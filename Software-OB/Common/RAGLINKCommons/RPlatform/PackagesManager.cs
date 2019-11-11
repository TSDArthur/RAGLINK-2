using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RAGLINKCommons.RPlatform
{
    public class PackagesManager
    {
        public class PackageList
        {
            public int packageCount;
            public List<string> packageName;
            public List<string> packageGUID;
            public List<string> packageDefineFilePath;
            public PackageList()
            {
                packageCount = 0;
                packageName = new List<string>();
                packageGUID = new List<string>();
                packageDefineFilePath = new List<string>();
            }
        };
        public class PackageInfo
        {
            public bool packageFileLoaded;
            public bool routeDetailInfoLoaded;
            public bool trainDetailInfoLoaded;
            //Area package_info
            public string packageName;
            public string packageVersion;
            public string packageGUID;
            public string packageDescription;
            public string packagePlatform;
            //Area dir
            public string packageRouteName;
            public string packageRouteDesc;
            public string packageRouteDir;
            public string packageRouteRelateDir;
            public Image packageRouteImage;
            public string packageRouteImageRelateDir;
            public Image packageRouteMapImage;
            public Image packageRouteGradientImage;
            public string packageTrainName;
            public string packageTrainDesc;
            public string packageTrainDir;
            public string packageTrainRelateDir;
            public Image packageTrainImage;
            public string packageTrainImageRelateDir;
            public PackageInfo()
            {
                routeDetailInfoLoaded = false;
                trainDetailInfoLoaded = false;
                packageFileLoaded = false;
                packageRouteImage = null;
                packageTrainImage = null;
                packageRouteMapImage = null;
                packageRouteGradientImage = null;
            }
        }
        static public PackageList packageList = new PackageList();
        static public PackageInfo packageInfo = new PackageInfo();
        static private string packageInfoSection = "package_info";
        static private string packageDirSection = "dir";
        static public void ResetPackageManager()
        {
            packageList = new PackageList();
            packageInfo = new PackageInfo();
        }
        static public bool UpdatePackageList()
        {
            bool retValue = false;
            try
            {
                //Clear package list
                packageList.packageCount = 0;
                packageList.packageDefineFilePath.Clear();
                packageList.packageGUID.Clear();
                packageList.packageName.Clear();
                //Update
                SettingsContent.UpdateSettingsPath();
                DirectoryInfo boardPath = new DirectoryInfo(SettingsContent.packagePath);
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                foreach (DirectoryInfo subDirectory in boardPath.GetDirectories())
                {
                    FileInfo[] packageDefinieFiles = subDirectory.GetFiles();
                    foreach (FileInfo fileName in packageDefinieFiles)
                    {
                        if (fileName.Extension == SettingsContent.universalFileExtName)
                        {
                            string packageDefineFilePath = fileName.FullName;
                            settingsFileIO.SetSettingsFilePath(packageDefineFilePath);
                            if (settingsFileIO.GetFileType() == SettingsContent.FileType.RESPACK)
                            {
                                //MessageBox.Show(packageDefineFilePath);
                                packageList.packageCount++;
                                packageList.packageDefineFilePath.Add(packageDefineFilePath);
                                packageList.packageGUID.Add(settingsFileIO.ReadValue(packageInfoSection, "packageguid"));
                                packageList.packageName.Add(settingsFileIO.ReadValue(packageInfoSection, "name"));
                            }
                            settingsFileIO.Dispose();
                        }
                    }
                }
            }
            catch (Exception) { };
            return retValue;
        }
        static public PackageInfo ReadPackageInfo(string packageFilePath)
        {
            PackageInfo retValue = new PackageInfo();
            try
            {
                if (!File.Exists(packageFilePath))
                {
                    return retValue;
                }

                string packagePath = Path.GetDirectoryName(packageFilePath);
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                settingsFileIO.SetSettingsFilePath(packageFilePath);
                if (settingsFileIO.GetFileType() != SettingsContent.FileType.RESPACK)
                {
                    return retValue;
                }
                //Load package
                retValue.packageName = settingsFileIO.ReadValue(packageInfoSection, "name");
                retValue.packageVersion = settingsFileIO.ReadValue(packageInfoSection, "version");
                retValue.packageGUID = settingsFileIO.ReadValue(packageInfoSection, "packageguid");
                retValue.packageDescription = settingsFileIO.ReadValue(packageInfoSection, "description");
                //Load dir
                retValue.packageRouteRelateDir = settingsFileIO.ReadValue(packageDirSection, "route_file");
                retValue.packageRouteDir = Path.GetFullPath(packagePath + "\\" + retValue.packageRouteRelateDir);
                retValue.packageTrainRelateDir = settingsFileIO.ReadValue(packageDirSection, "train_file");
                retValue.packageTrainDir = Path.GetFullPath(packagePath + "\\" + retValue.packageTrainRelateDir);
                try
                {
                    retValue.packageRouteImageRelateDir = settingsFileIO.ReadValue(packageDirSection, "route_image");
                    retValue.packageRouteImage = Image.FromFile(Path.GetFullPath(packagePath) + "\\" + retValue.packageTrainImageRelateDir);
                    retValue.packageTrainRelateDir = settingsFileIO.ReadValue(packageDirSection, "train_image");
                    retValue.packageTrainImage = Image.FromFile(Path.GetFullPath(packagePath) + "\\" + retValue.packageTrainRelateDir);
                }
                catch (Exception) { };
                settingsFileIO.Dispose();
                retValue.packageFileLoaded = true;
                retValue.routeDetailInfoLoaded = false;
                retValue.trainDetailInfoLoaded = false;
            }
            catch (Exception) { };
            return retValue;
        }
        static public void SetRouteDetailData(Image mapImage, Image gradientImage, string routeName, string routeDesc)
        {
            if (!packageInfo.packageFileLoaded || mapImage == null || gradientImage == null)
            {
                return;
            }

            packageInfo.packageRouteMapImage = mapImage;
            packageInfo.packageRouteGradientImage = gradientImage;
            packageInfo.packageRouteName = routeName;
            packageInfo.packageRouteDesc = routeDesc;
            packageInfo.routeDetailInfoLoaded = true;
        }
        static public void SetTrainDetailData(string trainName, string trainDesc)
        {
            if (!packageInfo.packageFileLoaded)
            {
                return;
            }

            packageInfo.packageTrainName = trainName;
            packageInfo.packageTrainDesc = trainDesc;
            packageInfo.trainDetailInfoLoaded = true;
        }
        static public bool GetDetailDataLoadingState()
        {
            return packageInfo.packageFileLoaded & packageInfo.routeDetailInfoLoaded & packageInfo.trainDetailInfoLoaded;
        }
        static public bool LoadPackageFile(string packageGUID)
        {
            bool retValue = false;
            try
            {
                for (int i = 0; i < packageList.packageGUID.Count; i++)
                {
                    if (packageList.packageGUID[i] == packageGUID)
                    {
                        packageInfo.packageFileLoaded = false;
                        packageInfo = ReadPackageInfo(packageList.packageDefineFilePath[i]);
                        packageInfo.packageFileLoaded = true;
                        break;
                    }
                }
            }
            catch (Exception) { };
            return retValue;
        }
    }
}
