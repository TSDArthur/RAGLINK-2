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
            SPEED_LIMIT = 7,
            HORN = 8,
            SPEED_CONST = 9,
            EMERGENCY = 10,
            LDOOR_OPEN = 11,
            RDOOR_OPEN = 12,
            LIGHT_ON = 13,
            PANTO_UP = 14,
            SANDER_ON = 15,
            CURRENT_STATION_INDEX = 16,
            CURRENT_STATION_NAME = 17,
            NEXT_STATION_INDEX = 18,
            NEXT_STATION_NAME = 19,
            CURRENT_STATION_DEPART_TIME = 20,
            NEXT_STATION_ARRIVAL_TIME = 21,
            NEXT_STATION_DISTANCE = 22,
            DEADMAN_PRESS = 23,
            TRACTION_ON = 24,
            AIR_CONDITION_ON = 25,
            CYLINER_PRESSURE = 26,
            PIPE_PRESSURE = 27,
            DESTINATION_STATION_NAME = 28,
            STATION_COUNT = 29,
            CURRENT_TIME = 30,
            DEADMAN_ENABLE = 31,
            NEXT_STATION_STOP = 32,
            DEADMAN_ENABLE_TRIG = 33,
            TRACK_POSITION = 34,
            //HMI instruction datas
            HMI_INSTRUCTIONS = 35
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
            public TrainDataMap[] HMIDataBinding;
            public bool[] HMIStringSpecialDeal;
            public HMIInstructions()
            {
                HMIDataBinding = new TrainDataMap[HMIDataCount]
                {
                    TrainDataMap.SPEED,
                    TrainDataMap.REVERSER,
                    TrainDataMap.NEXT_STATION_DISTANCE,
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
                    TrainDataMap.CYLINER_PRESSURE,
                    TrainDataMap.PIPE_PRESSURE,
                    TrainDataMap.DESTINATION_STATION_NAME,
                    TrainDataMap.STATION_COUNT
                };
                HMIStringSpecialDeal = new bool[HMIDataCount]
                {
                    true,
                    false,
                    true,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    true,
                    true,
                    false,
                    true
                };
                HMIScrips = new string[HMIDataCount];
                HMIData = new string[HMIDataCount];
            }
            public void SetHMIData(int dataIndex, string dataStream)
            {
                if (HMIScrips[dataIndex].Substring(HMIScrips[dataIndex].Length - 3, 3) == "txt")
                {
                    string HMIString = HMIScrips[dataIndex] + "=\"" + dataStream + "\"";
                    HMIData[dataIndex] = HMIString;
                }
                else if (HMIScrips[dataIndex].Substring(HMIScrips[dataIndex].Length - 3, 3) == "val")
                {
                    string HMIString = HMIScrips[dataIndex] + "=" + dataStream;
                    HMIData[dataIndex] = HMIString;
                }
            }
            public int GetDataCount()
            {
                return HMIDataCount;
            }
        };
        static public HMIInstructions HMIData = new HMIInstructions();
        public class TrainDataManager
        {
            public const int trainDataCount = 36;
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
                    TrainDataType.DOUBLE,
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
                    TrainDataType.INT,
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
