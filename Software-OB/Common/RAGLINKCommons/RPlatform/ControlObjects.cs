using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace RAGLINKCommons.RPlatform
{
    public class ControlObjects
    {
        public const int controlObjectsCount = 15;
        public enum ControlObjectsList
        {
            MASTER_KEY = 0,
            REVERSER = 1,
            COMBINED_HANDLE = 2,
            HORN = 3,
            EMERGENCY = 4,
            DOOR = 5,
            ATC = 6,
            DEADMAN = 7,
            LIGHT = 8,
            AIR_CONDITION = 9,
            PANTO = 10,
            TRACTION = 11,
            CAB_SIGNAL = 12,
            SAND = 13,
            HMI = 14
        }
        public class ControlObjectsInfo
        {
            public string objectFilePath;
            //Area control_object
            public int objectID;
            public string objectName;
            public string objectDescribe;
            public bool objectSetEnable;
            public bool objectTimerAttached;
            //Area object_io
            public int objectIOCount;
            public List<DevicesManager.DevicesIOMode> objectIOMode;
            public List<string> objectIODescrible;
            //Area object_data_attach
            public int objectDataCount;
            public List<string> objectData;
            public ControlObjectsInfo()
            {
                objectSetEnable = false;
                objectTimerAttached = false;
                objectIOMode = new List<DevicesManager.DevicesIOMode>();
                objectIODescrible = new List<string>();
                objectData = new List<string>();
            }
        };
        public class ControlObjectsArrange
        {
            public int objectID;
            public int objectIOSerialID;
            public int objectIOSerialBaud;
            public bool objectEnable;
            public List<object> objectIO;
            public ControlObjectsArrange()
            {
                objectEnable = false;
                objectIOSerialBaud = 9600;
                objectIO = new List<object>();
            }
        };
        static public ControlObjectsInfo[] controlObjectsInfo = new ControlObjectsInfo[controlObjectsCount];
        static public ControlObjectsArrange[] controlObjectArrange = new ControlObjectsArrange[controlObjectsCount];
        public delegate void EventsDelegate();
        static public EventsDelegate[] eventsDelegate = new EventsDelegate[controlObjectsCount]
        {
            new EventsDelegate(MASTER_KEY_EVENT),
            new EventsDelegate(REVERSER_EVENT),
            new EventsDelegate(COMBINED_HANDLE_EVENT),
            new EventsDelegate(HORN_EVENT),
            new EventsDelegate(EMERGENCY_EVENT),
            new EventsDelegate(DOOR_EVENT),
            new EventsDelegate(ATC_EVENT),
            new EventsDelegate(DEADMAN_EVENT),
            new EventsDelegate(LIGHT_EVENT),
            new EventsDelegate(AIR_CONDITION_EVENT),
            new EventsDelegate(PANTO_EVENT),
            new EventsDelegate(TRACTION_EVENT),
            new EventsDelegate(CAB_SIGNAL_EVENT),
            new EventsDelegate(SAND_EVENT),
            new EventsDelegate(HMI_EVENT)
        };
        static public int controlObjectFilesCount = 0;
        static private string controlObjectSection = "control_object";
        static private string objectIOSection = "object_io";
        static private string objectDataSection = "object_data_attach";

        static private int triggerClientID = -1;
        static private RProxy.SimWorldTrigger.TriggerClient TriggerClient = new RProxy.SimWorldTrigger.TriggerClient();

        static public void AttachToTrigger()
        {
            RProxy.SimWorldTrigger.DeleteTriggerClienr(TriggerClient);
            TriggerClient = new RProxy.SimWorldTrigger.TriggerClient(50, TriggerEvent);
            triggerClientID = RProxy.SimWorldTrigger.AddTriggerClient(TriggerClient);
        }

        static public void EnableTriggerClient()
        {
            RProxy.SimWorldTrigger.SetClientState(TriggerClient, false);
        }

        static public void TriggerEvent(double currentTimesCount, double currentTrackDistance)
        {
            DataManager.UpdateData(DataManager.TrainDataClassify.POSTBACK_DATA);
            for (int i = 0; i < controlObjectsCount; i++)
            {
                if (controlObjectArrange[i].objectEnable && controlObjectsInfo[i].objectTimerAttached)
                {
                    ControlObjectDoSingleEvents((ControlObjectsList)i);
                }
            }
        }

        static public void ResetControlObjects()
        {
            RProxy.SimWorldTrigger.DeleteTriggerClienr(TriggerClient);
            AttachToTrigger();
            for (int i = 0; i < controlObjectsCount; i++)
            {
                controlObjectsInfo[i] = new ControlObjectsInfo();
                controlObjectArrange[i] = new ControlObjectsArrange();
            }
        }
        static public void UpdateControlObjectsItem()
        {
            try
            {
                controlObjectFilesCount = 0;
                SettingsContent.UpdateSettingsPath();
                DirectoryInfo objectPath = new DirectoryInfo(SettingsContent.controlObjectPath);
                FileInfo[] objectFiles = objectPath.GetFiles();
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                foreach (FileInfo fileName in objectFiles)
                {
                    if (fileName.Extension == SettingsContent.universalFileExtName)
                    {
                        try
                        {
                            settingsFileIO.SetSettingsFilePath(fileName.FullName);
                            if (settingsFileIO.GetFileType() == SettingsContent.FileType.OBJECT)
                            {
                                int objectID = Int32.Parse(settingsFileIO.ReadValue(controlObjectSection, "object_id"));
                                controlObjectFilesCount++;
                                if (objectID > controlObjectsCount)
                                {
                                    continue;
                                }

                                controlObjectsInfo[objectID].objectIOMode.Clear();
                                controlObjectsInfo[objectID].objectIODescrible.Clear();
                                controlObjectsInfo[objectID].objectData.Clear();
                                controlObjectsInfo[objectID].objectFilePath = fileName.FullName;
                                //Area control_object
                                controlObjectsInfo[objectID].objectID = objectID;
                                controlObjectsInfo[objectID].objectName = settingsFileIO.ReadValue(controlObjectSection, "object_name");
                                controlObjectsInfo[objectID].objectDescribe = settingsFileIO.ReadValue(controlObjectSection, "object_describle");
                                controlObjectsInfo[objectID].objectSetEnable = Convert.ToBoolean(Int32.Parse(settingsFileIO.ReadValue(controlObjectSection, "object_set_enable")));
                                controlObjectsInfo[objectID].objectTimerAttached = Convert.ToBoolean(Int32.Parse(settingsFileIO.ReadValue(controlObjectSection, "object_timer_attached")));
                                //Area object_io
                                controlObjectsInfo[objectID].objectIOCount = Int32.Parse(settingsFileIO.ReadValue(objectIOSection, "object_io_count"));
                                for (int i = 0; i < controlObjectsInfo[objectID].objectIOCount; i++)
                                {
                                    controlObjectsInfo[objectID].objectIOMode.Add(
                                        (DevicesManager.DevicesIOMode)Int32.Parse(settingsFileIO.ReadValue(objectIOSection, "object_io_" + i.ToString() + "_mode")));
                                    controlObjectsInfo[objectID].objectIODescrible.Add(settingsFileIO.ReadValue(objectIOSection, "object_io_" + i.ToString() + "_describe"));
                                }
                                //Area object_data_attach
                                controlObjectsInfo[objectID].objectDataCount = Int32.Parse(settingsFileIO.ReadValue(objectDataSection, "object_data_count"));
                                for (int i = 0; i < controlObjectsInfo[objectID].objectDataCount; i++)
                                {
                                    controlObjectsInfo[objectID].objectData.Add(settingsFileIO.ReadValue(objectDataSection, "object_data_" + i.ToString()));
                                }
                            }
                        }
                        catch (Exception) { };
                        settingsFileIO.Dispose();
                    }
                }
            }
            catch (Exception) { };
        }
        static public ControlObjectsInfo CreateNewControlObjectFile(string filePath, string fileName)
        {
            ControlObjectsInfo retValue = new ControlObjectsInfo();
            try
            {
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                if (settingsFileIO.CreateNewFile(SettingsContent.FileType.OBJECT, filePath + "\\" + fileName))
                {
                    retValue.objectFilePath = Path.GetFullPath(filePath + "\\" + fileName + SettingsContent.universalFileExtName);
                }
            }
            catch (Exception) { };
            return retValue;
        }
        static public bool SaveControlObjectFile(ControlObjectsInfo currentControlObjectInfo)
        {
            bool retValue = false;
            SettingsFileIO settingsFileIO = new SettingsFileIO();
            settingsFileIO.SetSettingsFilePath(currentControlObjectInfo.objectFilePath);
            try
            {
                //write file
                //Area control_object
                settingsFileIO.WriteValue(controlObjectSection, "object_id", currentControlObjectInfo.objectID.ToString());
                settingsFileIO.WriteValue(controlObjectSection, "object_name", currentControlObjectInfo.objectName.ToString());
                settingsFileIO.WriteValue(controlObjectSection, "object_describle", currentControlObjectInfo.objectDescribe.ToString());
                settingsFileIO.WriteValue(controlObjectSection, "object_set_enable", Convert.ToInt32(currentControlObjectInfo.objectSetEnable).ToString());
                settingsFileIO.WriteValue(controlObjectSection, "object_timer_attached", Convert.ToInt32(currentControlObjectInfo.objectTimerAttached).ToString());
                //Area object_io
                settingsFileIO.WriteValue(objectIOSection, "object_io_count", currentControlObjectInfo.objectIOCount.ToString());
                for (int i = 0; i < currentControlObjectInfo.objectIOCount; i++)
                {
                    settingsFileIO.WriteValue(objectIOSection, "object_io_" + i.ToString() + "_mode", Convert.ToInt32(currentControlObjectInfo.objectIOMode[i]).ToString());
                    settingsFileIO.WriteValue(objectIOSection, "object_io_" + i.ToString() + "_describe", currentControlObjectInfo.objectIODescrible[i]);
                }
                //Area object_data_attach
                settingsFileIO.WriteValue(objectDataSection, "object_data_count", currentControlObjectInfo.objectDataCount.ToString());
                for (int i = 0; i < currentControlObjectInfo.objectDataCount; i++)
                {
                    settingsFileIO.WriteValue(objectDataSection, "object_data_" + i.ToString(), currentControlObjectInfo.objectData[i]);
                }
                retValue = true;
            }
            catch (Exception) { };
            settingsFileIO.Dispose();
            return retValue;
        }
        static public void ArrangeControlObject(int objectID, List<object> objectIO)
        {
            if (controlObjectArrange[objectID].objectEnable)
            {
                return;
            }

            controlObjectArrange[objectID].objectID = objectID;
            controlObjectArrange[objectID].objectEnable = true;
            controlObjectArrange[objectID].objectIO = objectIO;
        }

        static public void ArrangeControlObject(int objectID, string objectIO, int baudRate)
        {
            if (controlObjectArrange[objectID].objectEnable)
            {
                return;
            }

            controlObjectArrange[objectID].objectID = objectID;
            controlObjectArrange[objectID].objectEnable = true;
            controlObjectArrange[objectID].objectIO.Clear();
            controlObjectArrange[objectID].objectIO.Add(objectIO);
            controlObjectArrange[objectID].objectIOSerialBaud = baudRate;
            controlObjectArrange[objectID].objectIOSerialID = CommunicationSerial.serialCount++;
            CommunicationSerial.SerialByControlObject serialInfo = new CommunicationSerial.SerialByControlObject
            {
                serialControlObject = objectID,
                controlObjectSerialPort = objectIO,
                controlObjectSerialBaud = baudRate
            };
            CommunicationSerial.serialByControlObject.Add(serialInfo);
        }
        static public DevicesManager.DevicesIOMode GetBoardDeviceIOMode(int deviceID)
        {
            DevicesManager.DevicesIOMode retValue = DevicesManager.DevicesIOMode.DEVICE_WITHOUT_INIT;
            try
            {
                foreach (ControlObjectsArrange arrangeInfo in controlObjectArrange)
                {
                    if (arrangeInfo.objectEnable)
                    {
                        for (int i = 0; i < controlObjectsInfo[arrangeInfo.objectID].objectIOCount; i++)
                        {
                            if ((int)arrangeInfo.objectIO[i] == deviceID)
                            {
                                retValue = controlObjectsInfo[arrangeInfo.objectID].objectIOMode[i];
                                return retValue;
                            }
                        }
                    }
                }
            }
            catch (Exception) { };
            return retValue;
        }
        static public void ControlObjectDoEvents(ControlObjects.ControlObjectsList requestObjectID)
        {
            ControlObjectDoSingleEvents(requestObjectID);
            ControlObjectDoEvents();
        }
        static public void ControlObjectDoSingleEvents(ControlObjects.ControlObjectsList objectID)
        {
            DataManager.UpdateData(DataManager.TrainDataClassify.POSTBACK_DATA);
            eventsDelegate[(int)objectID]();
        }
        static public void ControlObjectDoEvents()
        {
            DataManager.UpdateData(DataManager.TrainDataClassify.POSTBACK_DATA);
            foreach (EventsDelegate DoEvent in eventsDelegate)
            {
                try
                {
                    DoEvent();
                }
                catch (Exception) { };
            }
        }
        static public void MASTER_KEY_EVENT()
        {
            int objectID = (int)ControlObjectsList.MASTER_KEY;
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.MASTER_KEY, 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.MASTER_KEY, 1);
            }
        }
        static public void REVERSER_EVENT()
        {
            int objectID = (int)ControlObjectsList.REVERSER;
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.MASTER_KEY) == 0)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.REVERSER, 0);
                return;
            }
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.POWER) != 0)
            {
                return;
            }

            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.REVERSER, Int32.Parse(controlObjectsInfo[objectID].objectData[2]));
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[1]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.REVERSER, Int32.Parse(controlObjectsInfo[objectID].objectData[4]));
            }
            else
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.REVERSER, Int32.Parse(controlObjectsInfo[objectID].objectData[3]));
            }
        }
        static public void COMBINED_HANDLE_EVENT()
        {
            bool zeroPaused = false;
            int objectID = (int)ControlObjectsList.COMBINED_HANDLE;
            int tractionID = (int)ControlObjectsList.TRACTION;
        HandleCheck:
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.MASTER_KEY) == 0 ||
                (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.PANTO_UP) == 0 ||
                ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.TRACTION_ON) == 0 && controlObjectArrange[tractionID].objectEnable) ||
                (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.EMERGENCY) == 1
                || (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.REVERSER) == 0)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.POWER, 0);
                DataManager.SetTrainData((int)DataManager.TrainDataMap.BRAKE, 6);
                return;
            }
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.POWER, 1);
                DataManager.SetTrainData((int)DataManager.TrainDataMap.BRAKE, 0);
                return;
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[1]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.POWER, 2);
                DataManager.SetTrainData((int)DataManager.TrainDataMap.BRAKE, 0);
                return;
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[2]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.POWER, 3);
                DataManager.SetTrainData((int)DataManager.TrainDataMap.BRAKE, 0);
                return;
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[3]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.POWER, 0);
                DataManager.SetTrainData((int)DataManager.TrainDataMap.BRAKE, 2);
                return;
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[4]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.POWER, 0);
                DataManager.SetTrainData((int)DataManager.TrainDataMap.BRAKE, 4);
                return;
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[5]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.POWER, 0);
                DataManager.SetTrainData((int)DataManager.TrainDataMap.BRAKE, 6);
                return;
            }
            else
            {
                // Hack If Last Time Not P1 or B1
                if (((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.POWER) > 1 ||
                    (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.BRAKE) > 2) && !zeroPaused)
                {
                    Thread.Sleep(200);
                    zeroPaused = true;
                    goto HandleCheck;
                }
                DataManager.SetTrainData((int)DataManager.TrainDataMap.POWER, 0);
                DataManager.SetTrainData((int)DataManager.TrainDataMap.BRAKE, 0);
                zeroPaused = false;
                return;
            }
        }
        static public void HORN_EVENT()
        {
            int objectID = (int)ControlObjectsList.HORN;
            if (!controlObjectArrange[objectID].objectEnable)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.HORN, 0);
                return;
            }
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.HORN, 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.HORN, 1);
            }
        }
        static public void EMERGENCY_EVENT()
        {
            int objectID = (int)ControlObjectsList.EMERGENCY;
            if (!controlObjectArrange[objectID].objectEnable)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.EMERGENCY, 0);
                return;
            }
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.EMERGENCY, 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.EMERGENCY, 1);
            }
        }
        static public void DOOR_EVENT()
        {
            int objectID = (int)ControlObjectsList.DOOR;
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.MASTER_KEY) == 0)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.LDOOR_OPEN, 0);
                DataManager.SetTrainData((int)DataManager.TrainDataMap.RDOOR_OPEN, 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[2], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[3], 0);
                return;
            }
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.LDOOR_OPEN, 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[2], 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.LDOOR_OPEN, 1);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[2], 1);
            }
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[1]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.RDOOR_OPEN, 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[3], 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[1]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.RDOOR_OPEN, 1);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[3], 1);
            }
        }
        static public void ATC_EVENT()
        {
            int objectID = (int)ControlObjectsList.ATC;
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.MASTER_KEY) == 0 ||
                (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.PANTO_UP) == 0 ||
                (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.TRACTION_ON) == 0 ||
                (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.EMERGENCY) == 1 ||
                !controlObjectArrange[objectID].objectEnable)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.SPEED_CONST, 0);
                return;
            }
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.SPEED_CONST, 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.SPEED_CONST, 1);
            }
        }
        static public void DEADMAN_EVENT()
        {
            int objectID = (int)ControlObjectsList.DEADMAN;
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.MASTER_KEY) == 0 ||
                (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.PANTO_UP) == 0 ||
                (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.TRACTION_ON) == 0 ||
                (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.SPEED) == 0 ||
                (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.EMERGENCY) == 1 ||
                (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.DEADMAN_ENABLE_TRIG) == 0 ||
                !controlObjectArrange[objectID].objectEnable)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.DEADMAN_ENABLE, 0);
                DataManager.SetTrainData((int)DataManager.TrainDataMap.DEADMAN_PRESS, 1);
                return;
            }
            DataManager.SetTrainData((int)DataManager.TrainDataMap.DEADMAN_ENABLE, 1);
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.DEADMAN_PRESS, 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.DEADMAN_PRESS, 1);
            }
        }
        static public void LIGHT_EVENT()
        {
            int objectID = (int)ControlObjectsList.LIGHT;
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.MASTER_KEY) == 0 ||
                !controlObjectArrange[objectID].objectEnable)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.LIGHT_ON, 0);
                return;
            }
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.LIGHT_ON, 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.LIGHT_ON, 1);
            }
        }
        static public void AIR_CONDITION_EVENT()
        {
            int objectID = (int)ControlObjectsList.AIR_CONDITION;
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.MASTER_KEY) == 0 ||
                !controlObjectArrange[objectID].objectEnable)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.AIR_CONDITION_ON, 0);
                return;
            }
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.AIR_CONDITION_ON, 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.AIR_CONDITION_ON, 1);
            }
        }
        static public void PANTO_EVENT()
        {
            int objectID = (int)ControlObjectsList.PANTO;
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.MASTER_KEY) == 0)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.PANTO_UP, 0);
                return;
            }
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.PANTO_UP, 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.PANTO_UP, 1);
            }
        }
        static public void TRACTION_EVENT()
        {
            int objectID = (int)ControlObjectsList.TRACTION;
            // Hack: If Traction Switch Doesn't Enable
            if (!controlObjectArrange[objectID].objectEnable)
            {
                return;
            }

            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.MASTER_KEY) == 0 ||
                (int)DataManager.GetTrainData((int)DataManager.TrainDataMap.PANTO_UP) == 0)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.TRACTION_ON, 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[1], 0);
                return;
            }
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.TRACTION_ON, 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[1], 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.TRACTION_ON, 1);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[1], 1);
            }
        }
        static public void CAB_SIGNAL_EVENT()
        {
            int objectID = (int)ControlObjectsList.CAB_SIGNAL;
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.MASTER_KEY) == 0 ||
                !controlObjectArrange[objectID].objectEnable)
            {
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[0], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[1], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[2], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[3], 0);
                return;
            }
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.SIGNAL) > 2)
            {
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[0], 1);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[1], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[2], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[3], 0);
            }
            else if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.SIGNAL) == 2)
            {
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[0], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[1], 1);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[2], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[3], 0);
            }
            else if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.SIGNAL) == 1)
            {
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[0], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[1], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[2], 1);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[3], 0);
            }
            else if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.SIGNAL) == 0)
            {
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[0], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[1], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[2], 0);
                DevicesManager.SetDeviceValue((int)controlObjectArrange[objectID].objectIO[3], 1);
            }
        }
        static public void SAND_EVENT()
        {
            int objectID = (int)ControlObjectsList.SAND;
            if ((int)DataManager.GetTrainData((int)DataManager.TrainDataMap.MASTER_KEY) == 0)
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.SANDER_ON, 0);
                return;
            }
            if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[0]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.SANDER_ON, 0);
            }
            else if (DevicesManager.GetDeviceValue((int)controlObjectArrange[objectID].objectIO[0]) == Int32.Parse(controlObjectsInfo[objectID].objectData[1]))
            {
                DataManager.SetTrainData((int)DataManager.TrainDataMap.SANDER_ON, 1);
            }
        }
        static public void HMI_EVENT()
        {
            int objectID = (int)ControlObjectsList.HMI;
            if (!controlObjectArrange[objectID].objectEnable)
            {
                return;
            } ((DataManager.HMIInstructions)DataManager.processData.trainData[(int)DataManager.TrainDataMap.HMI_INSTRUCTIONS]).UpdateHMIInstructions();
            try
            {
                foreach (string HMIString in ((DataManager.HMIInstructions)DataManager.processData.trainData[(int)DataManager.TrainDataMap.HMI_INSTRUCTIONS]).HMIData)
                {
                    CommunicationSerial.SendDataViaSerial(controlObjectArrange[objectID].objectIOSerialID, HMIString);
                    CommunicationSerial.SendDataViaSerial(controlObjectArrange[objectID].objectIOSerialID, 0xff);
                    CommunicationSerial.SendDataViaSerial(controlObjectArrange[objectID].objectIOSerialID, 0xff);
                    CommunicationSerial.SendDataViaSerial(controlObjectArrange[objectID].objectIOSerialID, 0xff);
                }
            }
            catch (Exception) { };
        }
    }
}
