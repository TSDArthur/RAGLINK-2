﻿using Fleck;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RAGLINKCommons.RPlatform
{
    class CommunicationNetwork
    {
        public enum ClientType
        {
            UNKNOW = 0,
            TRAIN_DATA_HANDLER_STR = 1,
            TRAIN_DATA_HANDLER_JSON = 2
        };

        public enum StateManager
        {
            READY = 0,
            WAITTING_DATA = 1,
            WAITTING_SUCC = 2
        };

        static public List<IWebSocketConnection> socketList = new List<IWebSocketConnection>();
        static public List<string> socketUrlList = new List<string>();
        static public List<ClientType> socketClientTypeList = new List<ClientType>();
        static public List<string> socketReadBuffer = new List<string>();
        static public List<StateManager> socketState = new List<StateManager>();
        static public WebSocketServer webSocketServer = new WebSocketServer("ws://127.0.0.1:7187");
        static public bool dealerInBusy = false;
        static public char startSymbol = '#';
        static public char endSymbol = '@';
        static public char transLeftIndexBracket = '[';
        static public char transRightIndexBracket = ']';
        static public char transLeftValueBracket = '(';
        static public char transRightValueBracket = ')';
        static public string transSetMode = "s";
        static public string transPauseSimulator = "p";
        static public string transRequestData = "rt";
        static public string transSetData = "st";
        static public string transSucc = "sc";
        static public int dataPackMaxsize = 64;
        static public string dataServerAddress = string.Empty;

        static public class JsonDataExchanger
        {
            public enum Request
            {
                SET_MODE = 1,
                REQUEST_DATA_LIST = 2,
                REQUEST_ALL_DATA = 3,
                REQUEST_DATA = 4,
                SET_DATA = 5,
                RESPONSE = 6,
                PAUSE_SIMULATOR = 7,
                CONTINUE_SIMULATOR = 8
            };

            public struct TrainData
            {
                public string trainDataType;
                public string trainDataClassify;
                public int trainDataID;
                public string trainData;
            }

            public class TrainDataJson
            {
                public bool deserialized;
                public bool operateReport;
                public string requestMode;
                public string requestClientType;
                public List<int> dataRequestList;
                public List<TrainData> dataResponseList;
                public TrainDataJson()
                {
                    deserialized = false;
                    operateReport = false;
                    requestMode = string.Empty;
                    requestClientType = string.Empty;
                    dataRequestList = new List<int>();
                    dataResponseList = new List<TrainData>();
                }
            };

            static public string Serialize(TrainDataJson trainDataJson)
            {
                string retValue = string.Empty;
                try
                {
                    trainDataJson.deserialized = false;
                    retValue = JsonConvert.SerializeObject(trainDataJson);
                }
                catch (Exception) { };
                return retValue;
            }

            static public TrainDataJson Deserialize(string trainDataJsonStr)
            {
                TrainDataJson trainDataJSON = new TrainDataJson();
                try
                {
                    trainDataJSON = JsonConvert.DeserializeObject<TrainDataJson>(trainDataJsonStr);
                    trainDataJSON.deserialized = true;
                }
                catch (Exception)
                {
                    trainDataJSON.deserialized = false;
                }
                return trainDataJSON;
            }

            static public bool DealWithJson(string clientUrl, string trainDataJsonStr)
            {
                bool retValue = false;
                TrainDataJson trainDataJSON;
                int socketIndex = socketUrlList.IndexOf(clientUrl);
                try
                {
                    trainDataJSON = Deserialize(trainDataJsonStr);
                    trainDataJSON.operateReport = false;
                    Request currentRequest = (Request)Enum.Parse(typeof(Request), trainDataJSON.requestMode, true);
                    trainDataJSON.requestMode = Request.RESPONSE.ToString();
                    if (trainDataJSON.deserialized)
                    {
                        if (currentRequest == Request.RESPONSE)
                        {
                            socketState[socketIndex] = StateManager.READY;
                        }
                        if (socketState[socketIndex] == StateManager.READY)
                        {
                            try
                            {
                                switch (currentRequest)
                                {
                                    case Request.SET_MODE:
                                        {
                                            trainDataJSON.requestMode = Request.RESPONSE.ToString();
                                            ClientType clientType = (ClientType)Enum.Parse(typeof(ClientType), trainDataJSON.requestClientType, true);
                                            SetSocketDataProvider(clientUrl, clientType);
                                            socketReadBuffer[socketIndex] = string.Empty;
                                            socketState[socketIndex] = StateManager.READY;
                                            trainDataJSON.requestClientType = string.Empty;
                                            trainDataJSON.dataRequestList.Clear();
                                            trainDataJSON.dataResponseList.Clear();
                                            break;
                                        }
                                    case Request.REQUEST_DATA_LIST:
                                        {
                                            trainDataJSON.requestMode = Request.SET_DATA.ToString();
                                            trainDataJSON.requestClientType = string.Empty;
                                            trainDataJSON.dataResponseList.Clear();
                                            trainDataJSON.dataRequestList.Clear();
                                            for (int i = 0; i < DataManager.processData.GetTrainDataCount(); i++)
                                            {
                                                TrainData trainData = new TrainData();
                                                trainData.trainData = string.Empty;
                                                trainData.trainDataID = i;
                                                trainData.trainDataType = DataManager.processData.trainDataType[i].ToString();
                                                trainData.trainDataClassify = DataManager.processData.trainDataClassify[i].ToString();
                                                trainDataJSON.dataRequestList.Add(i);
                                                trainDataJSON.dataResponseList.Add(trainData);
                                            }
                                            socketState[socketIndex] = StateManager.WAITTING_SUCC;
                                            break;
                                        }
                                    case Request.REQUEST_DATA:
                                        {
                                            List<int> currentDataRequest = new List<int>();
                                            trainDataJSON.requestMode = Request.SET_DATA.ToString();
                                            trainDataJSON.requestClientType = string.Empty;
                                            trainDataJSON.dataResponseList.Clear();
                                            currentDataRequest.Clear();
                                            foreach (int dataIndex in trainDataJSON.dataRequestList)
                                            {
                                                TrainData trainData = new TrainData();
                                                trainData.trainDataID = dataIndex;
                                                trainData.trainDataType = DataManager.processData.trainDataType[dataIndex].ToString();
                                                trainData.trainDataClassify = DataManager.processData.trainDataClassify[dataIndex].ToString();
                                                trainData.trainData = TrainData2Str(dataIndex);
                                                currentDataRequest.Add(trainDataJSON.dataResponseList.Count);
                                                trainDataJSON.dataResponseList.Add(trainData);
                                            }
                                            trainDataJSON.dataRequestList.Clear();
                                            trainDataJSON.dataRequestList = currentDataRequest;
                                            socketState[socketIndex] = StateManager.WAITTING_SUCC;
                                            break;
                                        }
                                    case Request.REQUEST_ALL_DATA:
                                        {
                                            trainDataJSON.requestMode = Request.SET_DATA.ToString();
                                            trainDataJSON.requestClientType = string.Empty;
                                            trainDataJSON.dataResponseList.Clear();
                                            trainDataJSON.dataRequestList.Clear();
                                            for (int dataIndex = 0; dataIndex < DataManager.processData.GetTrainDataCount(); dataIndex++)
                                            {
                                                TrainData trainData = new TrainData();
                                                trainData.trainDataID = dataIndex;
                                                trainData.trainDataType = DataManager.processData.trainDataType[dataIndex].ToString();
                                                trainData.trainDataClassify = DataManager.processData.trainDataClassify[dataIndex].ToString();
                                                trainData.trainData = TrainData2Str(dataIndex);
                                                trainDataJSON.dataRequestList.Add(dataIndex);
                                                trainDataJSON.dataResponseList.Add(trainData);
                                            }
                                            socketState[socketIndex] = StateManager.WAITTING_SUCC;
                                            break;
                                        }
                                    case Request.SET_DATA:
                                        {
                                            trainDataJSON.requestMode = Request.RESPONSE.ToString();
                                            try
                                            {
                                                foreach (int dataListIndex in trainDataJSON.dataRequestList)
                                                {
                                                    int dataIndex = trainDataJSON.dataResponseList[dataListIndex].trainDataID;
                                                    ApplyTrainData(dataIndex, trainDataJSON.dataResponseList[dataListIndex].trainData);
                                                }
                                            }
                                            catch (Exception) { };
                                            trainDataJSON.requestClientType = string.Empty;
                                            trainDataJSON.dataResponseList.Clear();
                                            trainDataJSON.dataRequestList.Clear();
                                            socketState[socketIndex] = StateManager.READY;
                                            break;
                                        }
                                    case Request.PAUSE_SIMULATOR:
                                        {
                                            trainDataJSON.requestMode = Request.RESPONSE.ToString();
                                            RProxy.SimCoreClient.PauseSimulator(true);
                                            trainDataJSON.requestClientType = string.Empty;
                                            trainDataJSON.dataResponseList.Clear();
                                            trainDataJSON.dataRequestList.Clear();
                                            socketState[socketIndex] = StateManager.READY;
                                            break;
                                        }
                                    case Request.CONTINUE_SIMULATOR:
                                        {
                                            trainDataJSON.requestMode = Request.RESPONSE.ToString();
                                            RProxy.SimCoreClient.PauseSimulator(false);
                                            trainDataJSON.requestClientType = string.Empty;
                                            trainDataJSON.dataResponseList.Clear();
                                            trainDataJSON.dataRequestList.Clear();
                                            socketState[socketIndex] = StateManager.READY;
                                            break;
                                        }
                                }
                                trainDataJSON.operateReport = true;
                            }
                            catch (Exception) { };
                            socketList[socketIndex].Send(Serialize(trainDataJSON));
                            retValue = true;
                        }
                    }
                }
                catch (Exception) { };
                return retValue;
            }
        };

        static public void ApplyTrainData(int dataIndex, string trainDataValue)
        {
            switch (DataManager.processData.trainDataType[dataIndex])
            {
                case DataManager.TrainDataType.STRING:
                    {
                        DataManager.SetTrainData(dataIndex, trainDataValue);
                        DataManager.ApplyTrainData();
                        break;
                    }
                case DataManager.TrainDataType.INT:
                    {
                        DataManager.SetTrainData(dataIndex, Int32.Parse(trainDataValue));
                        DataManager.ApplyTrainData();
                        break;
                    }
                case DataManager.TrainDataType.DOUBLE:
                    {
                        DataManager.SetTrainData(dataIndex, Double.Parse(trainDataValue));
                        DataManager.ApplyTrainData();
                        break;
                    }
            }
        }

        static public string TrainData2Str(int dataIndex)
        {
            string retValue = string.Empty;
            try
            {
                object trainDataValue = DataManager.processData.trainData[dataIndex];
                switch (DataManager.processData.trainDataType[dataIndex])
                {
                    case DataManager.TrainDataType.STRING:
                        {
                            retValue = trainDataValue.ToString();
                            break;
                        }
                    case DataManager.TrainDataType.INT:
                        {
                            retValue = ((int)trainDataValue).ToString();
                            break;
                        }
                    case DataManager.TrainDataType.DOUBLE:
                        {
                            retValue = Math.Round((double)trainDataValue, 2).ToString();
                            break;
                        }
                }
            }
            catch (Exception) { };
            return retValue;
        }

        static public int GetClientSocketCount()
        {
            int retValue;
            retValue = socketList.Count;
            return retValue;
        }

        static public void SetupWebSocketServer(string serverAddress)
        {
            try
            {
                socketList.Clear();
                socketUrlList.Clear();
                socketClientTypeList.Clear();
                socketReadBuffer.Clear();
                socketState.Clear();
                webSocketServer.Dispose();
                webSocketServer = new WebSocketServer(serverAddress);
                dataServerAddress = serverAddress;
                webSocketServer.RestartAfterListenError = true;
            }
            catch (Exception) { };
        }

        static public void StartWebSocketServer()
        {
            webSocketServer.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    int socketIndex = socketUrlList.IndexOf(clientUrl);
                    if (socketIndex == -1)
                    {
                        socketList.Add(socket);
                        socketUrlList.Add(clientUrl);
                        socketClientTypeList.Add(ClientType.UNKNOW);
                        socketReadBuffer.Add(string.Empty);
                        socketState.Add(StateManager.READY);
                    }
                };
                socket.OnClose = () =>
                {
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    int socketIndex = socketUrlList.IndexOf(clientUrl);
                    if (socketIndex != -1)
                    {
                        socketClientTypeList.RemoveAt(socketIndex);
                        socketList.RemoveAt(socketIndex);
                        socketUrlList.RemoveAt(socketIndex);
                        socketReadBuffer.RemoveAt(socketIndex);
                        socketState.RemoveAt(socketIndex);
                    }
                };
                socket.OnMessage = message =>
                {
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    int socketIndex = socketUrlList.IndexOf(clientUrl);
                    if (socketIndex != -1)
                    {
                        if (JsonDataExchanger.Deserialize(message).deserialized ||
                        (socketClientTypeList[socketIndex] == ClientType.TRAIN_DATA_HANDLER_JSON &&
                        JsonDataExchanger.Deserialize(message).deserialized))
                        {
                            JsonDataExchanger.DealWithJson(clientUrl, message);
                        }
                        else
                        {
                            socketReadBuffer[socketIndex] += message;
                            if (!MessageDealerInBusy())
                            {
                                int endSymbolPos = socketReadBuffer[socketIndex].LastIndexOf(endSymbol);
                                int startSymbolPos = socketReadBuffer[socketIndex].LastIndexOf(startSymbol);
                                if (endSymbolPos > startSymbolPos)
                                {
                                    try
                                    {
                                        DealwithMessageStrMode(clientUrl, socketReadBuffer[socketIndex].Substring(startSymbolPos + 1, endSymbolPos - startSymbolPos - 1));
                                        socketReadBuffer[socketIndex] = socketReadBuffer[socketIndex].Substring(endSymbolPos + 1);
                                    }
                                    catch (Exception) { };
                                }
                            }
                        }
                    }
                };
            });
        }

        static public bool SetSocketDataProvider(string clientUrl, ClientType clientType)
        {
            bool retValue = false;
            try
            {
                int socketIndex = socketUrlList.IndexOf(clientUrl);
                if (socketIndex != -1)
                {
                    socketClientTypeList[socketIndex] = clientType;
                }

                retValue = true;
            }
            catch (Exception) { };
            return retValue;
        }

        static public bool MessageDealerInBusy()
        {
            bool retValue = false;
            retValue = dealerInBusy;
            return retValue;
        }

        static public void DealwithMessageStrMode(string clientUrl, string streamData)
        {
            dealerInBusy = true;
            try
            {
                int socketIndex = socketUrlList.IndexOf(clientUrl);
                if (socketIndex != -1)
                {
                    if (streamData.IndexOf(transSetMode + transLeftValueBracket.ToString()) != -1)
                    {
                        int startPos = streamData.IndexOf(transLeftValueBracket);
                        int endPos = streamData.LastIndexOf(transRightValueBracket);
                        ClientType dataValue = socketClientTypeList[socketIndex];
                        if (endPos >= startPos)
                        {
                            dataValue = (ClientType)Enum.Parse(typeof(ClientType), streamData.Substring(startPos + 1, endPos - startPos - 1), true);
                        }

                        SetSocketDataProvider(clientUrl, dataValue);
                        string sendStream = startSymbol.ToString() + transSucc + endSymbol.ToString();
                        socketList[socketIndex].Send(sendStream);
                        socketReadBuffer[socketIndex] = string.Empty;
                        socketState[socketIndex] = StateManager.READY;
                    }
                    else if (socketClientTypeList[socketIndex] == ClientType.TRAIN_DATA_HANDLER_STR)
                    {
                        if (streamData.IndexOf(transRequestData + transLeftIndexBracket.ToString()) != -1)
                        {
                            if (socketState[socketIndex] == StateManager.READY)
                            {
                                int indexStartPos = streamData.IndexOf(transLeftIndexBracket);
                                int indexEndPos = streamData.LastIndexOf(transRightIndexBracket);
                                int dataIndex = 0;
                                try
                                {
                                    if (indexEndPos - indexStartPos > 0)
                                    {
                                        dataIndex = Int32.Parse(streamData.Substring(indexStartPos + 1, indexEndPos - indexStartPos - 1));
                                    }

                                    string sendStream = startSymbol.ToString() + transSetData + transLeftIndexBracket.ToString() + dataIndex.ToString() +
                                                    transRightIndexBracket.ToString() + transLeftValueBracket.ToString() + TrainData2Str(dataIndex) +
                                                    transRightValueBracket.ToString() + endSymbol.ToString();
                                    socketList[socketIndex].Send(sendStream);
                                    socketState[socketIndex] = StateManager.WAITTING_SUCC;
                                }
                                catch (Exception)
                                {
                                    socketState[socketIndex] = StateManager.READY;
                                }
                            }
                        }
                        else if (streamData.IndexOf(transSetData + transLeftIndexBracket.ToString()) != -1)
                        {
                            if (socketState[socketIndex] == StateManager.READY || socketState[socketIndex] == StateManager.WAITTING_DATA)
                            {
                                int indexStartPos = streamData.IndexOf(transLeftIndexBracket);
                                int indexEndPos = streamData.IndexOf(transRightIndexBracket);
                                int valueStartPos = streamData.IndexOf(transLeftValueBracket);
                                int valueEndPos = streamData.LastIndexOf(transRightValueBracket);
                                int dataIndex = 0;
                                string dataValue = string.Empty;
                                if (indexEndPos > indexStartPos)
                                {
                                    dataIndex = Int32.Parse(streamData.Substring(indexStartPos + 1, indexEndPos - indexStartPos - 1));
                                }

                                if (valueEndPos > valueStartPos)
                                {
                                    dataValue = streamData.Substring(valueStartPos + 1, valueEndPos - valueStartPos - 1);
                                }

                                try
                                {
                                    ApplyTrainData(dataIndex, dataValue);
                                    string sendStream = startSymbol.ToString() + transSucc + endSymbol.ToString();
                                    socketList[socketIndex].Send(sendStream);
                                    socketState[socketIndex] = StateManager.READY;
                                }
                                catch (Exception)
                                {
                                    socketState[socketIndex] = StateManager.READY;
                                };
                            }
                        }
                        else if (streamData.IndexOf(transSucc) != -1)
                        {
                            socketState[socketIndex] = StateManager.READY;
                        }
                        else if (streamData.IndexOf(transPauseSimulator + transLeftValueBracket) != -1)
                        {
                            int valueStartPos = streamData.IndexOf(transLeftValueBracket);
                            int valueEndPos = streamData.LastIndexOf(transRightValueBracket);
                            string dataValue = string.Empty;
                            if (valueEndPos > valueStartPos)
                            {
                                dataValue = streamData.Substring(valueStartPos + 1, valueEndPos - valueStartPos - 1);
                            }

                            try
                            {
                                RProxy.SimCoreClient.PauseSimulator(Convert.ToBoolean(Int32.Parse(dataValue)));
                                string sendStream = startSymbol.ToString() + transSucc + endSymbol.ToString();
                                socketList[socketIndex].Send(sendStream);
                                socketState[socketIndex] = StateManager.READY;
                            }
                            catch (Exception)
                            {
                                socketState[socketIndex] = StateManager.READY;
                            };
                        }
                    }
                }
            }
            catch (Exception) { };
            dealerInBusy = false;
        }
    }
}
