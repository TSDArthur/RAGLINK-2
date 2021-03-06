﻿using RAGLINKCommons.RStatistics;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;

namespace RAGLINKCommons.RPlatform
{
    public class ProjectsManager
    {
        public class ProjectList
        {
            public int projectCount;
            public List<string> projectID;
            public List<string> projectName;
            public List<string> projectDescribe;
            public List<string> projectFilePath;
            public List<bool> projectDebug;

            public ProjectList()
            {
                projectCount = 0;
                projectID = new List<string>();
                projectName = new List<string>();
                projectDescribe = new List<string>();
                projectFilePath = new List<string>();
                projectDebug = new List<bool>();
            }
        };

        public class ProjectInfo
        {
            public bool fileLoaded;
            //Area project
            public string projectName;
            public string projectID;
            public string projectDescribe;
            public Version projectVersion;
            public Version projectSimulatorSupportVersion;
            public string projectAuthor;
            public string projectAuthorWebsite;
            public string projectPlatform;
            public bool projectDebug;
            public string projectDataServer;
            //Area board
            public int boardID;
            public string boardPort;
            public int deviceUsedCount;
            //Area package
            public string packageGUID;
            //Area simulator_options
            public string simulatorOptionsFilePath;
            //Area record
            public string recorderFilePath;
            public string recordDir;
            //Area Q&A
            public SAGATManager.QAManagerData QAData;
            //Area control_objects
            public int controlObjectsUsed;
            public List<int> controlObjectID;
            public List<object> controlObjectPara;
            public List<int> controlObjectSerialBaudRate;

            public ProjectInfo()
            {
                fileLoaded = false;
                controlObjectID = new List<int>();
                controlObjectPara = new List<object>();
                controlObjectSerialBaudRate = new List<int>();
                QAData = new SAGATManager.QAManagerData();
            }
        };

        public enum ErrorType
        {
            ERROR = 0,
            WARNING = 1,
            INFORMATION = 2
        };

        public class ProjectErrorProvider
        {
            public ErrorType errorType;
            public string errorTitle;
            public int errorCode;

            public ProjectErrorProvider()
            {
                errorType = ErrorType.ERROR;
                errorTitle = string.Empty;
                errorCode = 0;
            }
        };

        static public ProjectList projectList = new ProjectList();
        static public ProjectInfo projectInfo = new ProjectInfo();
        static private string projectSection = "project";
        static private string boardSection = "board";
        static private string packageSection = "package";
        static private string simulatorOptionsSection = "simulator_options";
        static private string recordSection = "record";
        static private string questionAnswerSection = "question_answer";
        static private string questionAnswerListSction = "question_answer_pause";
        static private string controlObjectsSection = "control_objects";
        static private string objectProtocolSection = "object";

        static public void ResetProjectManager()
        {
            projectInfo = new ProjectInfo();
        }

        static public bool UpdateProjectItem()
        {
            bool retValue = false;
            try
            {
                //Clear project list
                projectList.projectCount = 0;
                projectList.projectFilePath.Clear();
                projectList.projectID.Clear();
                projectList.projectName.Clear();
                projectList.projectDescribe.Clear();
                projectList.projectDebug.Clear();
                //Update
                SettingsContent.UpdateSettingsPath();
                DirectoryInfo projectPath = new DirectoryInfo(SettingsContent.projectLibPath);
                foreach (DirectoryInfo subDirectory in projectPath.GetDirectories())
                {
                    FileInfo[] projectFiles = subDirectory.GetFiles();
                    SettingsFileIO settingsFileIO = new SettingsFileIO();
                    foreach (FileInfo fileName in projectFiles)
                    {
                        if (fileName.Extension == SettingsContent.projectFileExtName)
                        {
                            settingsFileIO.SetSettingsFilePath(fileName.FullName);
                            projectList.projectCount++;
                            projectList.projectFilePath.Add(fileName.FullName);
                            projectList.projectID.Add(settingsFileIO.ReadValue(projectSection, "project_id"));
                            projectList.projectName.Add(settingsFileIO.ReadValue(projectSection, "project_name"));
                            projectList.projectDescribe.Add(settingsFileIO.ReadValue(projectSection, "project_describle"));
                            projectList.projectDebug.Add(Convert.ToBoolean(Int32.Parse(settingsFileIO.ReadValue(projectSection, "project_debug"))));
                            settingsFileIO.Dispose();
                        }
                    }
                }
                retValue = true;
            }
            catch (Exception) { };
            return retValue;
        }

        static public ProjectInfo ReadProjectInfo(string projectFilePath)
        {
            ProjectInfo retValue = new ProjectInfo();
            SettingsFileIO settingsFileIO = new SettingsFileIO();
            try
            {
                retValue.controlObjectID.Clear();
                retValue.controlObjectPara.Clear();
                retValue.controlObjectSerialBaudRate.Clear();
                settingsFileIO.SetSettingsFilePath(projectFilePath);
                //Load project
                retValue.projectName = settingsFileIO.ReadValue(projectSection, "project_name");
                retValue.projectID = settingsFileIO.ReadValue(projectSection, "project_id");
                retValue.projectDescribe = settingsFileIO.ReadValue(projectSection, "project_describle");
                retValue.projectVersion = new Version(settingsFileIO.ReadValue(projectSection, "project_version"));
                retValue.projectSimulatorSupportVersion = new Version(settingsFileIO.ReadValue(projectSection, "project_simulator_support_version"));
                retValue.projectAuthor = settingsFileIO.ReadValue(projectSection, "project_author");
                retValue.projectAuthorWebsite = settingsFileIO.ReadValue(projectSection, "project_author_website");
                retValue.projectPlatform = settingsFileIO.ReadValue(projectSection, "project_platform");
                retValue.projectDebug = Convert.ToBoolean(Int32.Parse(settingsFileIO.ReadValue(projectSection, "project_debug")));
                retValue.projectDataServer = settingsFileIO.ReadValue(projectSection, "project_data_server");
                //Load board
                retValue.boardID = Int32.Parse(settingsFileIO.ReadValue(boardSection, "board_id"));
                retValue.boardPort = settingsFileIO.ReadValue(boardSection, "board_port");
                retValue.deviceUsedCount = Int32.Parse(settingsFileIO.ReadValue(boardSection, "devices_used"));
                //Load package
                retValue.packageGUID = settingsFileIO.ReadValue(packageSection, "package_id");
                //Load simulator_options
                string projectDir = Path.GetDirectoryName(projectFilePath);
                retValue.simulatorOptionsFilePath = Path.GetFullPath(projectDir + "\\" + settingsFileIO.ReadValue(simulatorOptionsSection, "option_file"));
                //Load record
                retValue.recorderFilePath = Path.GetFullPath(projectDir + "\\" + settingsFileIO.ReadValue(recordSection, "recorder"));
                retValue.recordDir = Path.GetFullPath(projectDir + "\\" + settingsFileIO.ReadValue(recordSection, "record_dir"));
                try
                {
                    //Load Q&A
                    retValue.QAData = new SAGATManager.QAManagerData();
                    retValue.QAData.Clear();
                    retValue.QAData.questionAnswerEnable = Convert.ToBoolean(Int32.Parse(settingsFileIO.ReadValue(questionAnswerSection, "question_answer_enable")));
                    if (retValue.QAData.questionAnswerEnable)
                    {
                        retValue.QAData.questionAnswerCount = Int32.Parse(settingsFileIO.ReadValue(questionAnswerSection, "question_answer_count"));
                        retValue.QAData.questionAnswerSavePath = Path.GetFullPath(projectDir + "\\" + settingsFileIO.ReadValue(questionAnswerSection, "question_answer_save_path"));
                        retValue.QAData.questionAnswers.Clear();
                        for (int i = 0; i < retValue.QAData.questionAnswerCount; i++)
                        {
                            string sectionName = questionAnswerListSction + i.ToString();
                            SAGATManager.QAManagerData.QuestionAnswer questionAnswer = new SAGATManager.QAManagerData.QuestionAnswer();
                            questionAnswer.pauseDistance = Double.Parse(settingsFileIO.ReadValue(sectionName, "pause_distance"));
                            questionAnswer.questionCount = Int32.Parse(settingsFileIO.ReadValue(sectionName, "question_count"));
                            questionAnswer.questionAnswerItems.Clear();
                            for (int j = 0; j < questionAnswer.questionCount; j++)
                            {
                                SAGATManager.QAManagerData.QuestionAnswer.QuestionAnswerItem questionAnswerItem = new SAGATManager.QAManagerData.QuestionAnswer.QuestionAnswerItem();
                                questionAnswerItem.questionTimer = Int32.Parse(settingsFileIO.ReadValue(sectionName, "question" + j.ToString() + "_timer"));
                                questionAnswerItem.questionTitle = settingsFileIO.ReadValue(sectionName, "question" + j.ToString() + "_title");
                                questionAnswerItem.questionDesc = settingsFileIO.ReadValue(sectionName, "question" + j.ToString() + "_describle");
                                questionAnswerItem.questionMode = (SAGATManager.QAManagerData.QuestionMode)Int32.Parse(settingsFileIO.ReadValue(sectionName, "answer" + j.ToString() + "_mode"));
                                questionAnswerItem.selectionText.Clear();
                                if (questionAnswerItem.questionMode == SAGATManager.QAManagerData.QuestionMode.TEXT_INPUT)
                                {
                                    questionAnswerItem.textboxType = (SAGATManager.QAManagerData.TextboxType)Int32.Parse(settingsFileIO.ReadValue(sectionName, "answer" + j.ToString() + "_textbox_type"));
                                }
                                else if (questionAnswerItem.questionMode == SAGATManager.QAManagerData.QuestionMode.SELECT)
                                {
                                    questionAnswerItem.selectionCount = Int32.Parse(settingsFileIO.ReadValue(sectionName, "answer" + j.ToString() + "_selection_count"));
                                    for (int k = 0; k < questionAnswerItem.selectionCount; k++)
                                    {
                                        questionAnswerItem.selectionText.Add(settingsFileIO.ReadValue(sectionName, "answer" + j.ToString() + "_selection" + k.ToString() + "_text"));
                                    }
                                }
                                questionAnswerItem.submitButtonText = settingsFileIO.ReadValue(sectionName, "answer" + j.ToString() + "_submit_button_text");
                                questionAnswerItem.correctValue = settingsFileIO.ReadValue(sectionName, "answer" + j.ToString() + "_correct_value");
                                questionAnswer.questionAnswerItems.Add(questionAnswerItem);
                            }
                            retValue.QAData.questionAnswers.Add(questionAnswer);
                        }
                    }
                }
                catch (Exception) { };
                //Load control_objects
                retValue.controlObjectsUsed = Int32.Parse(settingsFileIO.ReadValue(controlObjectsSection, "control_objects_used"));
                retValue.controlObjectID.Clear();
                retValue.controlObjectPara.Clear();
                retValue.controlObjectSerialBaudRate.Clear();
                //Load object setup
                for (int i = 0; i < retValue.controlObjectsUsed; i++)
                {
                    retValue.controlObjectID.Add(Int32.Parse(settingsFileIO.ReadValue(objectProtocolSection + i.ToString(), "object_id")));
                    if (ControlObjects.controlObjectsInfo[retValue.controlObjectID[i]].objectIOMode[0] == DevicesManager.DevicesIOMode.DEVICE_SERIAL)
                    {
                        retValue.controlObjectPara.Add(settingsFileIO.ReadValue(objectProtocolSection + i.ToString(), "object_device_para"));
                        retValue.controlObjectSerialBaudRate.Add(Int32.Parse(settingsFileIO.ReadValue(objectProtocolSection + i.ToString(), "object_baudrate")));
                    }
                    else
                    {
                        List<object> tempPara = new List<object>();
                        tempPara = settingsFileIO.ReadVectorValue(objectProtocolSection + i.ToString(), "object_device_para").ConvertAll<object>(x => (object)x);
                        retValue.controlObjectPara.Add(tempPara);
                        retValue.controlObjectSerialBaudRate.Add(-1);
                    }
                }
                settingsFileIO.Dispose();
            }
            catch (Exception) { };
            settingsFileIO.Dispose();
            return retValue;
        }

        static public bool LoadProjectFile(string projectFilePath)
        {
            bool retValue = false;
            try
            {
                projectInfo.fileLoaded = false;
                projectInfo = ReadProjectInfo(projectFilePath);
                projectInfo.fileLoaded = true;
                retValue = true;
            }
            catch (Exception) { };
            return retValue;
        }

        static public bool SetupBoard()
        {
            bool retValue = false;
            try
            {
                BoardsManager.UpdateBoardItems();
                if (!projectInfo.fileLoaded || BoardsManager.boardList.boardID.IndexOf(projectInfo.boardID) == -1)
                {
                    retValue = false;
                    return retValue;
                }
                BoardsManager.LoadBoardFile(BoardsManager.boardList.boardFilePath[BoardsManager.boardList.boardID.IndexOf(projectInfo.boardID)]);
                retValue = true;
            }
            catch (Exception) { };
            return retValue;
        }

        static public bool SetupControlObjects()
        {
            bool retValue = false;
            try
            {
                if (!projectInfo.fileLoaded)
                {
                    retValue = false;
                    return retValue;
                }
                for (int i = 0; i < projectInfo.deviceUsedCount; i++)
                {
                    int objectID = projectInfo.controlObjectID[i];
                    if (projectInfo.controlObjectPara[i] is string)
                    {
                        ControlObjects.ArrangeControlObject(objectID, projectInfo.controlObjectPara[i].ToString(), projectInfo.controlObjectSerialBaudRate[i]);
                    }
                    else
                    {
                        ControlObjects.ArrangeControlObject(objectID, (List<object>)projectInfo.controlObjectPara[i]);
                    }
                }
            }
            catch (Exception) { };
            return retValue;
        }

        static public bool SetupDataServer()
        {
            bool retValue = false;
            try
            {
                if (!projectInfo.fileLoaded)
                {
                    retValue = false;
                    return retValue;
                }
                CommunicationNetwork.SetupWebSocketServer(projectInfo.projectDataServer);
                retValue = true;
            }
            catch (Exception) { };
            return retValue;
        }

        static public List<ProjectErrorProvider> CheckProjectContent()
        {
            List<ProjectErrorProvider> retValue = new List<ProjectErrorProvider>();
            try
            {
                retValue.Clear();
                //Check board
                BoardsManager.UpdateBoardItems();
                if (BoardsManager.boardList.boardID.IndexOf(projectInfo.boardID) == -1)
                {
                    ProjectErrorProvider tempError = new ProjectErrorProvider
                    {
                        errorType = ErrorType.ERROR,
                        errorTitle = "当前本地主控板库无项目所使用主控板定义。",
                        errorCode = 100
                    };
                    retValue.Add(tempError);
                }
                //Check board version
                if (BoardsManager.boardInfo.boardSupprtVersion > SettingsContent.simulatorVersion)
                {
                    ProjectErrorProvider tempError = new ProjectErrorProvider
                    {
                        errorType = ErrorType.WARNING,
                        errorTitle = "当前主控板支持模拟器版本（" +
                            BoardsManager.boardInfo.boardSupprtVersion.ToString() +
                            "）,可能不兼容当前模拟器版本（" + SettingsContent.simulatorVersion.ToString() + "）。",
                        errorCode = 101
                    };
                    retValue.Add(tempError);
                }
                //Check project support version
                if (ProjectsManager.projectInfo.projectSimulatorSupportVersion != SettingsContent.simulatorVersion)
                {
                    ProjectErrorProvider tempError = new ProjectErrorProvider
                    {
                        errorType = ErrorType.WARNING,
                        errorTitle = "当前行车计划支持模拟器版本（" +
                            ProjectsManager.projectInfo.projectSimulatorSupportVersion.ToString() +
                            "）,可能不兼容当前模拟器版本（" + SettingsContent.simulatorVersion.ToString() + "）。",
                        errorCode = 102
                    };
                    retValue.Add(tempError);
                }
                //Check platform
                if (ProjectsManager.projectInfo.projectPlatform.ToUpper() != SettingsContent.simulatorVerification.ToUpper())
                {
                    ProjectErrorProvider tempError = new ProjectErrorProvider
                    {
                        errorType = ErrorType.ERROR,
                        errorTitle = "当前行车计划模拟器平台（" +
                            ProjectsManager.projectInfo.projectPlatform +
                            "）,不兼容当前模拟器平台（" + SettingsContent.simulatorVerification + "）。",
                        errorCode = 103
                    };
                    retValue.Add(tempError);
                }
                //Check serial
                foreach (SerialPort serialPort in CommunicationSerial.serialList)
                {
                    if (CommunicationSerial.GetSerialPortNames().IndexOf(serialPort.PortName) == -1)
                    {
                        ProjectErrorProvider tempError = new ProjectErrorProvider
                        {
                            errorType = ErrorType.ERROR,
                            errorTitle = "当前系统未与端口为“" + serialPort.PortName + "”的设备建立硬件连接。",
                            errorCode = 104
                        };
                        retValue.Add(tempError);
                    }
                }
                //Check control objects step
                for (int i = 0; i < ControlObjects.controlObjectsCount; i++)
                {
                    if (!ControlObjects.controlObjectsInfo[i].objectSetEnable && !ControlObjects.controlObjectArrange[i].objectEnable)
                    {
                        ProjectErrorProvider tempError = new ProjectErrorProvider
                        {
                            errorType = ErrorType.ERROR,
                            errorTitle = "必要操作对象“" + ControlObjects.controlObjectsInfo[i].objectName + "”未在项目中定义。",
                            errorCode = 105
                        };
                        retValue.Add(tempError);
                    }
                    if (ControlObjects.controlObjectArrange[i].objectEnable)
                    {
                        if (ControlObjects.controlObjectArrange[i].objectIO.Count != ControlObjects.controlObjectsInfo[i].objectIOCount)
                        {
                            ProjectErrorProvider tempError = new ProjectErrorProvider
                            {
                                errorType = ErrorType.ERROR,
                                errorTitle = "操作对象“" + ControlObjects.controlObjectsInfo[i].objectName + "”未完全配置。",
                                errorCode = 106
                            };
                            retValue.Add(tempError);
                        }
                        if (ControlObjects.controlObjectsInfo[i].objectIOMode[0] == DevicesManager.DevicesIOMode.DEVICE_SERIAL)
                        {
                            if (!(ControlObjects.controlObjectArrange[i].objectIO[0] is string)
                                || ControlObjects.controlObjectArrange[i].objectIOSerialBaud == 0
                                || ControlObjects.controlObjectArrange[i].objectIOSerialID == 0)
                            {
                                ProjectErrorProvider tempError = new ProjectErrorProvider
                                {
                                    errorType = ErrorType.ERROR,
                                    errorTitle = "操作对象“" + ControlObjects.controlObjectsInfo[i].objectName + "”存在配置错误。",
                                    errorCode = 107
                                };
                                retValue.Add(tempError);
                            }
                        }
                    }
                }
            }
            catch (Exception) { };
            return retValue;
        }
    }
}
