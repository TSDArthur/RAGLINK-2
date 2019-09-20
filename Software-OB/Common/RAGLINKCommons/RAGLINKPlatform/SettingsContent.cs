using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAGLINKCommons.RAGLINKPlatform
{
	public class SettingsContent
	{
        //File type
        public enum FileType
        {
            BOARD = 0,
            OBJECT = 1,
            PROJECT = 2,
            RECORDER = 3,
            RECORDS = 4,
            RESPACK = 5,
            SETTINGS = 6,
            IDEDESC = 7,
            UNKNOW = 8
        };
        //Paths
		static public string platformPath;
		static public string boardPath;
		static public string controlObjectPath;
		static public string projectLibPath;
		static public string settingsPath;
		static public string tempFilePath;
		static public string recorderPath;
        static public string packagePath;
        static public string simulatorDefaultSettingsPath;
        //File Ext-Names
		static public string projectFileExtName = ".prj";
		static public string universalFileExtName = ".re";
        //Simulator Args
        static public string simulatorUIMode = "-ui";
        static public string simualatorCompilerMode = "-o";
        //File encrypt key
		static public string encryptKey = @"WNyA6SmzQ7EL82UXxMK17CjVhhHSi8LMXno2h1CuZUSO22PRslxaKbgRiX2WIQb7";
		static public void UpdateSettingsPath()
		{
			SettingsFileIO settingsFileIO = new SettingsFileIO();
            platformPath = Application.StartupPath;
            settingsFileIO.SetSettingsFilePath(platformPath + "\\Path.ini");
            packagePath = Path.GetFullPath(platformPath + "\\" + settingsFileIO.ReadValue("path", "package_path"));
            boardPath = Path.GetFullPath(platformPath + "\\" + settingsFileIO.ReadValue("path", "board_path"));
			controlObjectPath = Path.GetFullPath(platformPath + "\\" + settingsFileIO.ReadValue("path", "control_object_path"));
			projectLibPath = Path.GetFullPath(platformPath + "\\" + settingsFileIO.ReadValue("path", "project_lib_path"));
			settingsPath = Path.GetFullPath(platformPath + "\\" + settingsFileIO.ReadValue("path", "settings_path"));
			tempFilePath = Path.GetFullPath(platformPath + "\\" + settingsFileIO.ReadValue("path", "temp_file_path"));
			recorderPath = Path.GetFullPath(platformPath + "\\" + settingsFileIO.ReadValue("path", "recorder_path"));
            simulatorDefaultSettingsPath = Path.GetFullPath(platformPath + "\\" + settingsFileIO.ReadValue("path", "settings_path"));
            settingsFileIO.Dispose();
			return;
		}
	}
}
