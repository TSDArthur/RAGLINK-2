using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAGLINKCommons.RAGLINKPlatform
{
    public class ProjectLoader
    {
        public enum LoadingState
        {
            WAIT_FOR_START = 0,
            READY = 1,
            DATA_RESET = 2,
            DATA_UPDATED = 3,
            PROJECT_DEFINE_LOADED = 4,
            PACKAGE_DEFINE_LOADED = 5,
            TRAIN_LOADED = 6,
            ROUTE_LOADED = 7,
            SIM_OPTIONS_LOADED = 8,
            ERROR_CHECKED = 9,
            DONE = 10,
            ERROR = 11,
        };
        public delegate void LoaderProcessHandler(LoadingState processValue);
        static public event LoaderProcessHandler ProcessEvent;
        public delegate bool TrainDataLoaderHandler(string trainFile);
        static public event TrainDataLoaderHandler TrainDataLoaderEvent;
        public delegate bool RouteDataLoaderHandler(string routeFile);
        static public event RouteDataLoaderHandler RouteDataLoaderEvent;
        public delegate bool GraphicOptionsLoaderHandler(GraphicOptionsManager.GraphicOptionsValue optionsData);
        static public event GraphicOptionsLoaderHandler GraphicOptionsLoaderEvent;
        public delegate void StartSimulatorHandler();
        static public event StartSimulatorHandler StartSimulatorEvent;
        static public string projectGUIDValue = string.Empty;
        static private string projectFilePath = string.Empty;
        static private string projectDirPath = string.Empty;
        public static class routeLoadingStateMonitor
        {
            private static int inTimerMain = 0;
            private static System.Threading.Timer MainTimer;
            public static bool MainTimerEnable = false;
            private static readonly int mainTimerTick = 100;
            static public void AttachMainTimerInterrupt()
            {
                try
                {
                    inTimerMain = 0;
                    MainTimer = new System.Threading.Timer(new TimerCallback(MainTimerProcess), null, mainTimerTick, mainTimerTick);
                }
                catch (Exception) { };
            }
            static public void Dispose()
            {
                MainTimer.Dispose();
            }
            static private void MainTimerProcess(object state)
            {
                if (Interlocked.Exchange(ref inTimerMain, 1) == 0)
                {
                    if (MainTimerEnable)
                    {
                        try
                        {
                            if(PackagesManager.packageInfo.routeDetailInfoLoaded)
                            {
                                MainTimerEnable = false;
                                ProcessEvent(LoadingState.ROUTE_LOADED);
                                Dispose();
                            }
                        }
                        catch (Exception) { };
                    }
                    Interlocked.Exchange(ref inTimerMain, 0);
                }
            }
        };
        static public bool LoadTrainData(string trainFile)
        {
            bool retValue = false;
            retValue = TrainDataLoaderEvent(trainFile);
            return retValue;
        }
        static public bool LoadRouteData(string routeFile)
        {
            bool retValue = false;
            retValue = RouteDataLoaderEvent(routeFile);
            return retValue;
        }
        static public void ProjectLoaderSetup(string projectGUID)
        {
            try
            {
                routeLoadingStateMonitor.Dispose();
            }
            catch (Exception) { };
            projectGUIDValue = projectGUID;
            projectFilePath = string.Empty;
            projectDirPath = string.Empty;
        }
        static public void ProjectLoaderProcess(LoadingState processValue)
        {
            try
            {
                switch (processValue)
                {
                    case LoadingState.WAIT_FOR_START:
                        {
                            ProcessEvent(LoadingState.READY);
                            break;
                        }
                    case LoadingState.READY:
                        {
                            //reset data
                            BoardsManager.ResetBoardManager();
                            ControlObjects.ResetControlObjects();
                            Communication.ResetSerial();
                            DataManager.ResetTrainData();
                            PackagesManager.ResetPackageManager();
                            GraphicOptionsManager.ResetGraphicOptionsManager();
                            UserInterfaceSwap.errorContent.Clear();
                            ProcessEvent(LoadingState.DATA_RESET);
                            break;
                        }
                    case LoadingState.DATA_RESET:
                        {
                            //update list
                            SettingsContent.UpdateSettingsPath();
                            BoardsManager.UpdateBoardItems();
                            ControlObjects.UpdateControlObjectsItem();
                            ProjectsManager.UpdateProjectItem();
                            PackagesManager.UpdatePackageList();
                            ProcessEvent(LoadingState.DATA_UPDATED);
                            break;
                        }
                    case LoadingState.DATA_UPDATED:
                        {
                            //get project define file path
                            int fileIndex = ProjectsManager.projectList.projectID.IndexOf(projectGUIDValue);
                            projectFilePath = ProjectsManager.projectList.projectFilePath[fileIndex];
                            projectDirPath = Path.GetDirectoryName(projectFilePath);
                            //load project define file
                            ProjectsManager.LoadProjectFile(projectFilePath);
                            ProjectsManager.SetupBoard();
                            ProjectsManager.SetupControlObjects();
                            DevicesManager.UpdateDevicesInitMode();
                            Communication.InitializeSerial();
                            ProcessEvent(LoadingState.PROJECT_DEFINE_LOADED);
                            break;
                        }
                    case LoadingState.PROJECT_DEFINE_LOADED:
                        {
                            //load package
                            PackagesManager.LoadPackageFile(ProjectsManager.projectInfo.packageGUID);
                            ProcessEvent(LoadingState.PACKAGE_DEFINE_LOADED);
                            break;
                        }
                    case LoadingState.PACKAGE_DEFINE_LOADED:
                        {
                            //load train
                            PackagesManager.packageInfo.trainDetailInfoLoaded = false;
                            LoadTrainData(PackagesManager.packageInfo.packageTrainDir);
                            ProcessEvent(LoadingState.TRAIN_LOADED);
                            break;
                        }
                    case LoadingState.TRAIN_LOADED:
                        {
                            //load route
                            PackagesManager.packageInfo.routeDetailInfoLoaded = false;
                            routeLoadingStateMonitor.AttachMainTimerInterrupt();
                            routeLoadingStateMonitor.MainTimerEnable = true;
                            LoadRouteData(PackagesManager.packageInfo.packageRouteDir);
                            break;
                        }
                    case LoadingState.ROUTE_LOADED:
                        {
                            //load options
                            GraphicOptionsManager.LoadGraphicOptionsFile(ProjectsManager.projectInfo.simulatorOptionsFilePath);
                            GraphicOptionsLoaderEvent(GraphicOptionsManager.graphicOptionsValue);
                            ProcessEvent(LoadingState.SIM_OPTIONS_LOADED);
                            break;
                        }
                    case LoadingState.SIM_OPTIONS_LOADED:
                        {
                            //check error
                            UserInterfaceSwap.errorContent = ProjectsManager.CheckProjectContent();
                            ProcessEvent(LoadingState.DONE);
                            break;
                        }
                    case LoadingState.DONE:
                        {
                            StartSimulatorEvent();
                            break;
                        }
                }
            }
            catch (Exception)
            {
                ProcessEvent(LoadingState.ERROR);
            };
        }
        static public void ReviewProjectResources(string projectGUID)
        {
            SettingsContent.UpdateSettingsPath();
            ProjectsManager.ResetProjectManager();
            PackagesManager.ResetPackageManager();
            ProjectsManager.UpdateProjectItem();
            PackagesManager.UpdatePackageList();
            ProjectsManager.LoadProjectFile(ProjectsManager.projectList.projectFilePath[ProjectsManager.projectList.projectID.IndexOf(projectGUID)]);
            PackagesManager.LoadPackageFile(ProjectsManager.projectInfo.packageGUID);
            GraphicOptionsManager.LoadGraphicOptionsFile(ProjectsManager.projectInfo.simulatorOptionsFilePath);
            LoadTrainData(PackagesManager.packageInfo.packageTrainDir);
            PackagesManager.packageInfo.trainDetailInfoLoaded = false;
            LoadRouteData(PackagesManager.packageInfo.packageRouteDir);
            PackagesManager.packageInfo.routeDetailInfoLoaded = false;
        }
    }
}
