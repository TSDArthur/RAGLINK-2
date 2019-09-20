using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAGLINK_Version_Selector.FileIO
{
    public class VersionManager
    {
        public class PlatformList
        {
            public int platformCount;
            public string platformDefault;
            public List<string> platformVerification;
            public List<string> platformNameDisplay;
            public List<string> platformVersion;
            public List<string> platformPath;
            public List<string> platformStudioExecute;
            public List<string> platformImage;
            public PlatformList()
            {
                platformDefault = string.Empty;
                platformVerification = new List<string>();
                platformNameDisplay = new List<string>();
                platformVersion = new List<string>();
                platformPath = new List<string>();
                platformStudioExecute = new List<string>();
                platformImage = new List<string>();
            }
        };
        static public PlatformList platformList = new PlatformList();
        static public int platformSelected = 0;
        static public void UpdatePlatformInfo()
        {
            try
            {
                platformList = new PlatformList();
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                settingsFileIO.SetSettingsFilePath(Path.GetFullPath(Application.StartupPath + "\\Platform.ini"));
                platformList.platformCount = Convert.ToInt32(settingsFileIO.ReadValue("summary", "platform_count"));
                platformList.platformDefault = settingsFileIO.ReadValue("summary", "platform_default");
                for (int i = 0; i < platformList.platformCount; i++)
                {
                    string sectionValue = "platform" + i.ToString();
                    platformList.platformVerification.Add(settingsFileIO.ReadValue(sectionValue, "platform_verification"));
                    platformList.platformNameDisplay.Add(settingsFileIO.ReadValue(sectionValue, "platform_name_display"));
                    platformList.platformVersion.Add(settingsFileIO.ReadValue(sectionValue, "platform_version"));
                    platformList.platformPath.Add(settingsFileIO.ReadValue(sectionValue, "platform_path"));
                    platformList.platformStudioExecute.Add(settingsFileIO.ReadValue(sectionValue, "platform_studio_execute"));
                    platformList.platformImage.Add(settingsFileIO.ReadValue(sectionValue, "platform_image"));
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
