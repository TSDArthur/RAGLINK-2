using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fleck;

namespace RAGLINKCommons.RAGLINKPlatform
{
    class CommunicationNetwork
    {
        public enum ClientType
        {
            UNKNOW = 0,
            TRAIN_DATA_HANDLER = 1
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
        static public string transRequestData = "rt";
        static public string transSetData = "st";
        static public string transSucc = "sc";
        static public int dataPackMaxsize = 64;
        static public string dataServerAddress = string.Empty;

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
                        socketReadBuffer[socketIndex] += message;
                        if(!MessageDealerInBusy())
                        {
                            int endSymbolPos = socketReadBuffer[socketIndex].LastIndexOf(endSymbol);
                            int startSymbolPos = socketReadBuffer[socketIndex].LastIndexOf(startSymbol);
                            if(endSymbolPos > startSymbolPos)
                            {
                                DealwithMessage(clientUrl, socketReadBuffer[socketIndex].Substring(startSymbolPos + 1, endSymbolPos - startSymbolPos - 1));
                                socketReadBuffer[socketIndex] = socketReadBuffer[socketIndex].Substring(endSymbolPos + 1);
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
                if (socketIndex != -1) socketClientTypeList[socketIndex] = clientType;
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

        static public void DealwithMessage(string clientUrl, string streamData)
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
                        int dataValue = (int)socketClientTypeList[socketIndex];
                        if (endPos >= startPos) dataValue = Int32.Parse(streamData.Substring(startPos + 1, endPos - startPos - 1));
                        SetSocketDataProvider(clientUrl, (ClientType)dataValue);
                        string sendStream = startSymbol.ToString() + transSucc + endSymbol.ToString();
                        socketList[socketIndex].Send(sendStream);
                    }
                    else if(socketClientTypeList[socketIndex] == ClientType.TRAIN_DATA_HANDLER)
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
                                    if (indexEndPos - indexStartPos > 0) dataIndex = Int32.Parse(streamData.Substring(indexStartPos + 1, indexEndPos - indexStartPos - 1));
                                    switch (DataManager.processData.trainDataType[dataIndex])
                                    {
                                        case DataManager.TrainDataType.STRING:
                                            {
                                                string sendStream = startSymbol.ToString() + transSetData + transLeftIndexBracket.ToString() + dataIndex.ToString() +
                                                    transRightIndexBracket.ToString() + transLeftValueBracket.ToString() + DataManager.processData.trainData[dataIndex].ToString() +
                                                    transRightValueBracket.ToString() + endSymbol.ToString();
                                                socketList[socketIndex].Send(sendStream);
                                                break;
                                            }
                                        case DataManager.TrainDataType.INT:
                                            {
                                                string sendStream = startSymbol.ToString() + transSetData + transLeftIndexBracket.ToString() + dataIndex.ToString() +
                                                    transRightIndexBracket.ToString() + transLeftValueBracket.ToString() + ((int)DataManager.processData.trainData[dataIndex]).ToString() +
                                                    transRightValueBracket.ToString() + endSymbol.ToString();
                                                    socketList[socketIndex].Send(sendStream);
                                                break;
                                            }
                                        case DataManager.TrainDataType.DOUBLE:
                                            {
                                                string sendStream = startSymbol.ToString() + transSetData + transLeftIndexBracket.ToString() + dataIndex.ToString() +
                                                    transRightIndexBracket.ToString() + transLeftValueBracket.ToString() + Math.Round((double)DataManager.processData.trainData[dataIndex], 2).ToString() +
                                                    transRightValueBracket.ToString() + endSymbol.ToString();
                                                    socketList[socketIndex].Send(sendStream);
                                                break;
                                            }
                                    }
                                    socketState[socketIndex] = StateManager.WAITTING_SUCC;
                                }
                                catch (Exception)
                                {
                                    socketState[socketIndex] = StateManager.READY;
                                }
                            }
                        }
                        else if(streamData.IndexOf(transSetData + transLeftIndexBracket.ToString()) != -1)
                        {
                            if (socketState[socketIndex] == StateManager.READY || socketState[socketIndex] == StateManager.WAITTING_DATA)
                            {
                                int indexStartPos = streamData.IndexOf(transLeftIndexBracket);
                                int indexEndPos = streamData.IndexOf(transRightIndexBracket);
                                int valueStartPos = streamData.IndexOf(transLeftValueBracket);
                                int valueEndPos = streamData.LastIndexOf(transRightValueBracket);
                                int dataIndex = 0;
                                string dataValue = string.Empty;
                                if(indexEndPos > indexStartPos) dataIndex = Int32.Parse(streamData.Substring(indexStartPos + 1, indexEndPos - indexStartPos - 1));
                                if (valueEndPos > valueStartPos) dataValue = streamData.Substring(valueStartPos + 1, valueEndPos - valueStartPos - 1);
                                try
                                {
                                    switch (DataManager.processData.trainDataType[dataIndex])
                                    {
                                        case DataManager.TrainDataType.STRING:
                                            {
                                                DataManager.SetTrainData(dataIndex, dataValue);
                                                DataManager.ApplyTrainData();
                                                break;
                                            }
                                        case DataManager.TrainDataType.INT:
                                            {
                                                DataManager.SetTrainData(dataIndex, Int32.Parse(dataValue));
                                                DataManager.ApplyTrainData();
                                                break;
                                            }
                                        case DataManager.TrainDataType.DOUBLE:
                                            {
                                                DataManager.SetTrainData(dataIndex, Double.Parse(dataValue));
                                                DataManager.ApplyTrainData();
                                                break;
                                            }
                                    }
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
                        else if(streamData.IndexOf(transSucc) != -1)
                        {
                            socketState[socketIndex] = StateManager.READY;
                        }
                    }
                }
            }
            catch (Exception) { };
            dealerInBusy = false;
        }
    }
}
