using System;

namespace RAGLINKCommons.RPlatform
{
    public class DataManager
    {
        //Data update event
        public delegate void UpdateDataHandler(TrainDataClassify updateMode);
        static public event UpdateDataHandler UpdateDataEvent;
        //Data apply event
        public delegate void ApplyTrainDataHandler();
        static public event ApplyTrainDataHandler ApplyTrainDataEvent;
        public enum TrainDataMap
        {
            //Normal datas
            MASTER_KEY = 0,
            REVERSER = 1,
            POWER = 2,
            BRAKE = 3,
            SIGNAL = 4,
            SIGNAL_DIST = 5,
            SPEED = 6,
            SPEED_HMI = 7,
            SPEED_LIMIT = 8,
            HORN = 9,
            SPEED_CONST = 10,
            EMERGENCY = 11,
            LDOOR_OPEN = 12,
            RDOOR_OPEN = 13,
            LIGHT_ON = 14,
            PANTO_UP = 15,
            SANDER_ON = 16,
            CURRENT_STATION_NAME = 17,
            NEXT_STATION_NAME = 18,
            CURRENT_STATION_DEPART_TIME = 19,
            NEXT_STATION_ARRIVAL_TIME = 20,
            NEXT_STATION_DISTANCE = 21,
            DEADMAN_PRESS = 22,
            TRACTION_ON = 23,
            AIR_CONDITION_ON = 24,
            CYLINER_PRESSURE = 25,
            PIPE_PRESSURE = 26,
            DESTINATION_STATION_NAME = 27,
            STATION_COUNT = 28,
            CURRENT_TIME = 29,
            NEXT_STATION_DISTANCE_HMI = 30,
            CYLINER_PRESSURE_HMI = 31,
            PIPE_PRESSURE_HMI = 32,
            STATION_COUNT_HMI = 33,
            DEADMAN_ENABLE = 34,
            NEXT_STATION_STOP = 35,
            DEADMAN_ENABLE_TRIG = 36,
            TRACK_POSITION = 37,
            //HMI instruction datas
            HMI_INSTRUCTIONS = 38
        }
        public enum TrainDataType
        {
            INT = 0,
            DOUBLE = 1,
            STRING = 2,
            HMI = 3
        }
        public enum TrainDataClassify
        {
            CONTROL_DATA = 0,
            POSTBACK_DATA = 1,
            ALL_DATA = 2
        }
        public class HMIInstructions
        {
            public const int HMIDataCount = 24;
            public string[] HMIScrips;
            public string[] HMIData;
            public TrainDataMap[] HMIDataMap;
            public HMIInstructions()
            {
                HMIScrips = new string[HMIDataCount];
                HMIData = new string[HMIDataCount];
                HMIDataMap = new TrainDataMap[HMIDataCount]
                {
                    TrainDataMap.SPEED_HMI,
                    TrainDataMap.REVERSER,
                    TrainDataMap.NEXT_STATION_DISTANCE_HMI,
                    TrainDataMap.POWER,
                    TrainDataMap.BRAKE,
                    TrainDataMap.SIGNAL,
                    TrainDataMap.CURRENT_TIME,
                    TrainDataMap.SIGNAL_DIST,
                    TrainDataMap.SPEED_LIMIT,
                    TrainDataMap.HORN,
                    TrainDataMap.MASTER_KEY,
                    TrainDataMap.LDOOR_OPEN,
                    TrainDataMap.RDOOR_OPEN,
                    TrainDataMap.SANDER_ON,
                    TrainDataMap.LIGHT_ON,
                    TrainDataMap.PANTO_UP,
                    TrainDataMap.CURRENT_STATION_NAME,
                    TrainDataMap.CURRENT_STATION_DEPART_TIME,
                    TrainDataMap.NEXT_STATION_NAME,
                    TrainDataMap.NEXT_STATION_ARRIVAL_TIME,
                    TrainDataMap.CYLINER_PRESSURE_HMI,
                    TrainDataMap.PIPE_PRESSURE_HMI,
                    TrainDataMap.DESTINATION_STATION_NAME,
                    TrainDataMap.STATION_COUNT_HMI
                };
            }
            public void UpdateHMIInstructions()
            {
                try
                {
                    HMIScrips = ControlObjects.controlObjectsInfo[(int)ControlObjects.ControlObjectsList.HMI].objectData.ToArray();
                    for (int i = 0; i < HMIDataCount; i++)
                    {
                        if (HMIScrips[i].Substring(HMIScrips[i].Length - 3, 3) == "txt")
                        {
                            string HMIString = HMIScrips[i] + "=\"" + processData.trainData[(int)HMIDataMap[i]].ToString() + "\"";
                            HMIData[i] = HMIString;
                        }
                        else if (HMIScrips[i].Substring(HMIScrips[i].Length - 3, 3) == "val")
                        {
                            string HMIString = HMIScrips[i] + "=" + processData.trainData[(int)HMIDataMap[i]].ToString();
                            HMIData[i] = HMIString;
                        }
                    }
                }
                catch (Exception) { };
            }
            public int GetDataCount()
            {
                return HMIDataCount;
            }
        };
        static public HMIInstructions HMIData = new HMIInstructions();
        public class TrainDataManager
        {
            public const int trainDataCount = 39;
            public object[] trainData;
            public TrainDataType[] trainDataType;
            public TrainDataClassify[] trainDataClassify;
            public TrainDataManager()
            {
                trainData = new object[trainDataCount];
                trainDataType = new TrainDataType[trainDataCount]
                {
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.STRING,
                    TrainDataType.STRING,
                    TrainDataType.STRING,
                    TrainDataType.STRING,
                    TrainDataType.DOUBLE,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.DOUBLE,
                    TrainDataType.DOUBLE,
                    TrainDataType.STRING,
                    TrainDataType.INT,
                    TrainDataType.STRING,
                    TrainDataType.STRING,
                    TrainDataType.STRING,
                    TrainDataType.STRING,
                    TrainDataType.STRING,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.INT,
                    TrainDataType.DOUBLE,
                    TrainDataType.HMI
                };
                trainDataClassify = new TrainDataClassify[trainDataCount]
                {
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.CONTROL_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                    TrainDataClassify.POSTBACK_DATA,
                };
                ResetData();
            }
            public void ResetData()
            {
                int intDefaultValue = 0;
                double doubleDefaultValue = 0;
                string stringDefaultValue = string.Empty;
                HMIInstructions HMIDefaultValue = HMIData;
                for (int i = 0; i < trainDataCount; i++)
                {
                    switch (trainDataType[i])
                    {
                        case TrainDataType.INT:
                            {
                                trainData[i] = intDefaultValue;
                                break;
                            }
                        case TrainDataType.DOUBLE:
                            {
                                trainData[i] = doubleDefaultValue;
                                break;
                            }
                        case TrainDataType.STRING:
                            {
                                trainData[i] = stringDefaultValue;
                                break;
                            }
                        case TrainDataType.HMI:
                            {
                                trainData[i] = HMIDefaultValue;
                                break;
                            }
                    }
                }
            }
            public void SetData(int dataID, object dataValue)
            {
                trainData[dataID] = dataValue;
            }
            public int GetTrainDataCount()
            {
                return trainDataCount;
            }
        };
        static public TrainDataManager processData = new TrainDataManager();
        static public void ResetTrainData()
        {
            processData.ResetData();
        }
        static public void UpdateData(DataManager.TrainDataClassify updateMode)
        {
            UpdateDataEvent(updateMode);
        }
        static public void ApplyTrainData()
        {
            ApplyTrainDataEvent();
        }
        static public object GetTrainData(int dataID)
        {
            object retValue;
            retValue = processData.trainData[dataID];
            return retValue;
        }
        static public void SetTrainData(int dataID, object dataValue)
        {
            processData.SetData(dataID, dataValue);
        }
    }
}
