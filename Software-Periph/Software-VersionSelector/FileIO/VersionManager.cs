using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RStarter.FileIO
{
    public class VersionManager
    {
        public enum PlatformType
        {
            Community = 0,
            Laboratory = 1
        };

        public class PlatformList
        {
            public PlatformType studioType;
            public Version studioVersion;
            public int platformCount;
            public string platformDefault;
            public List<string> platformVerification;
            public List<string> platformNameDisplay;
            public List<string> platformVersion;
            public List<string> platformPath;
            public List<string> platformStudioExecute;
            public List<string> platformImage;
            public List<string> platformUIMode;
            public List<string> platformCompilerMode;
            public PlatformList()
            {
                platformDefault = string.Empty;
                platformVerification = new List<string>();
                platformNameDisplay = new List<string>();
                platformVersion = new List<string>();
                platformPath = new List<string>();
                platformStudioExecute = new List<string>();
                platformUIMode = new List<string>();
                platformCompilerMode = new List<string>();
            }
        };
        static public PlatformList platformList = new PlatformList();
        static public int platformSelected = 0;
        static public string[] startArgs;
        static public string startArgsStr = string.Empty;
        static public void UpdatePlatformInfo()
        {
            try
            {
                platformList = new PlatformList();
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                DirectoryInfo[] dirArray = new DirectoryInfo(Application.StartupPath).GetDirectories();
                foreach (DirectoryInfo directoryInfo in dirArray)
                {
                    settingsFileIO.SetSettingsFilePath(Application.StartupPath + "\\Platform.ini");
                    platformList.studioType = (PlatformType)Enum.Parse(typeof(PlatformType), settingsFileIO.ReadValue("summary", "studio_type"), true);
                    platformList.studioVersion = Version.Parse(settingsFileIO.ReadValue("summary", "studio_version"));
                    platformList.platformDefault = settingsFileIO.ReadValue("summary", "platform_default");
                    string fileFullName = Path.GetFullPath(directoryInfo.FullName + "\\Platform.ini");
                    if (File.Exists(fileFullName))
                    {
                        settingsFileIO.SetSettingsFilePath(fileFullName);
                        platformList.platformVerification.Add(settingsFileIO.ReadValue("platform_define", "platform_verification"));
                        platformList.platformNameDisplay.Add(settingsFileIO.ReadValue("platform_define", "platform_name"));
                        platformList.platformVersion.Add(settingsFileIO.ReadValue("platform_define", "platform_version"));
                        platformList.platformPath.Add(directoryInfo.Name);
                        platformList.platformStudioExecute.Add(settingsFileIO.ReadValue("platform_define", "complier_excute"));
                        platformList.platformUIMode.Add(settingsFileIO.ReadValue("platform_define", "ui_mode_args"));
                        platformList.platformCompilerMode.Add(settingsFileIO.ReadValue("platform_define", "compile_mode_args"));
                        platformList.platformCount++;
                    }
                }
            }
            catch (Exception) { };
        }
        static public void StartPlatformExecute(string platformVerification, string Args)
        {
            platformVerification = platformVerification.ToLower();
            try
            {
                int platformIndex = platformList.platformVerification.ConvertAll(c => c.ToLower()).IndexOf(platformVerification);
                string executeFileName = platformList.platformStudioExecute[platformIndex];
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = Path.GetFullPath(Application.StartupPath + "\\"
                    + platformList.platformPath[platformIndex] + "\\" + platformList.platformStudioExecute[platformIndex]);
                processInfo.Arguments = Args;
                Process processExcute = Process.Start(processInfo);
            }
            catch (Exception)
            {
                MessageBox.Show("平台启动失败，请检查文件完整性。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        static public void SetDefaultPlatfrom(string platformVerification)
        {
            try
            {
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                settingsFileIO.SetSettingsFilePath(Path.GetFullPath(Application.StartupPath + "\\Platform.ini"));
                settingsFileIO.WriteValue("summary", "platform_default", platformVerification);
            }
            catch (Exception) { };
        }
    }
}
