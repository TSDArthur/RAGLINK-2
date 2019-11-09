using System;
using System.IO;

namespace RAGLINKCommons.RPlatform
{
    public class GraphicOptionsManager
    {
        public class GraphicOptionsValue
        {
            public bool optionsFileLoaded;
            public string optionsFilePath;
            public bool enableVSync;
            public bool enableFullScreen;
            public bool enableMotionBlur;
            public int simulatorWidth;
            public int simulatorHeight;
            public int antiAliasingLevel;
            public int transparencyLevel;
            public int viewingDistance;
            public int maxSimulatorWidth;
            public int maxSimulatorHeight;
            public int minSimulatorWidth;
            public int minSimulatorHeight;
            public int maxAntiAliasingLevel;
            public int maxTransparencyLevel;
            public int maxViewingDistance;
            public GraphicOptionsValue()
            {
                optionsFilePath = string.Empty;
                optionsFileLoaded = false;
                enableVSync = false;
                enableFullScreen = false;
                enableMotionBlur = false;
                maxSimulatorWidth = 5760;
                maxSimulatorHeight = 1440;
                minSimulatorWidth = 1024;
                minSimulatorHeight = 600;
                maxAntiAliasingLevel = 4;
                maxTransparencyLevel = 3;
                maxViewingDistance = 800;
            }
            public object Clone()
            {
                return this.MemberwiseClone();
            }
        };
        static public GraphicOptionsValue graphicOptionsValue = new GraphicOptionsValue();
        static private string settingsSection = "settings";
        static public void ResetGraphicOptionsManager()
        {
            graphicOptionsValue = new GraphicOptionsValue();
        }
        static public bool LoadGraphicOptionsFile(string optionsFilePath)
        {
            bool retValue = false;
            try
            {
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                settingsFileIO.SetSettingsFilePath(optionsFilePath);
                if (settingsFileIO.GetFileType() != SettingsContent.FileType.SETTINGS)
                {
                    return retValue;
                }

                graphicOptionsValue.optionsFilePath = optionsFilePath;
                graphicOptionsValue.enableVSync = Convert.ToBoolean(Int32.Parse(settingsFileIO.ReadValue(settingsSection, "vsync")));
                graphicOptionsValue.enableFullScreen = Convert.ToBoolean(Int32.Parse(settingsFileIO.ReadValue(settingsSection, "fullscreen")));
                graphicOptionsValue.enableMotionBlur = Convert.ToBoolean(Int32.Parse(settingsFileIO.ReadValue(settingsSection, "motionblur")));
                graphicOptionsValue.simulatorWidth = Int32.Parse(settingsFileIO.ReadValue(settingsSection, "simulatorwidth"));
                graphicOptionsValue.simulatorHeight = Int32.Parse(settingsFileIO.ReadValue(settingsSection, "simulatorheight"));
                graphicOptionsValue.antiAliasingLevel = Int32.Parse(settingsFileIO.ReadValue(settingsSection, "antialiasinglevel"));
                graphicOptionsValue.transparencyLevel = Int32.Parse(settingsFileIO.ReadValue(settingsSection, "transparencylevel"));
                graphicOptionsValue.viewingDistance = Int32.Parse(settingsFileIO.ReadValue(settingsSection, "viewingdistance"));
                graphicOptionsValue.optionsFileLoaded = true;
                settingsFileIO.Dispose();
                retValue = true;
            }
            catch (Exception) { };
            return retValue;
        }
        static public GraphicOptionsValue CreateNewGraphicOptionsFile(string filePath, string fileName)
        {
            GraphicOptionsValue retValue = new GraphicOptionsValue();
            try
            {
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                if (settingsFileIO.CreateNewFile(SettingsContent.FileType.SETTINGS, filePath + "\\" + fileName))
                {
                    retValue.optionsFileLoaded = true;
                    retValue.optionsFilePath = Path.GetFullPath(filePath + "\\" + fileName + SettingsContent.universalFileExtName);
                    settingsFileIO.Dispose();
                }
            }
            catch (Exception) { };
            return retValue;
        }
        static public bool SaveGraphicOptionsFile(GraphicOptionsValue currentGraphicOptionsFile)
        {
            bool retValue = false;
            try
            {
                if (!currentGraphicOptionsFile.optionsFileLoaded)
                {
                    return retValue;
                }

                SettingsFileIO settingsFileIO = new SettingsFileIO();
                settingsFileIO.SetSettingsFilePath(currentGraphicOptionsFile.optionsFilePath);
                settingsFileIO.WriteValue(settingsSection, "fullscreen", currentGraphicOptionsFile.enableFullScreen == true ? "1" : "0");
                settingsFileIO.WriteValue(settingsSection, "vsync", currentGraphicOptionsFile.enableVSync == true ? "1" : "0");
                settingsFileIO.WriteValue(settingsSection, "motionblur", currentGraphicOptionsFile.enableMotionBlur == true ? "1" : "0");
                settingsFileIO.WriteValue(settingsSection, "simulatorwidth", currentGraphicOptionsFile.simulatorWidth.ToString());
                settingsFileIO.WriteValue(settingsSection, "simulatorheight", currentGraphicOptionsFile.simulatorHeight.ToString());
                settingsFileIO.WriteValue(settingsSection, "antialiasinglevel", currentGraphicOptionsFile.antiAliasingLevel.ToString());
                settingsFileIO.WriteValue(settingsSection, "transparencylevel", currentGraphicOptionsFile.transparencyLevel.ToString());
                settingsFileIO.WriteValue(settingsSection, "viewingdistance", currentGraphicOptionsFile.viewingDistance.ToString());
                settingsFileIO.Dispose();
                retValue = true;
            }
            catch (Exception) { };
            return retValue;
        }
    }
}
