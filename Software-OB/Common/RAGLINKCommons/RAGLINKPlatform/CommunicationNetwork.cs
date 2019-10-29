using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;

namespace RAGLINKCommons.RAGLINKPlatform
{
    class CommunicationNetwork
    {
        public delegate List<string> StringSepratorDelegate(int maxSize);
        public delegate string StringBuilderDelegate();
        static public List<IWebSocketConnection> socketList = new List<IWebSocketConnection>();
        static public List<string> socketUrlList = new List<string>();
        static public Dictionary<IWebSocketConnection, string> socketConnectionInfoList = new Dictionary<IWebSocketConnection, string>();
        static public Dictionary<string, StringBuilderDelegate> stringBuilderDelegatesList = new Dictionary<string, StringBuilderDelegate>();
        static public Dictionary<string, StringSepratorDelegate> stringSepratorDelegatesList = new Dictionary<string, StringSepratorDelegate>();
        static public WebSocketServer webSocketServer = new WebSocketServer("ws://127.0.0.1:7187");

        static public void SetupWebSocketServer(string serverAddress)
        {
            try
            {
                socketList.Clear();
                socketUrlList.Clear();
                stringBuilderDelegatesList.Clear();
                stringSepratorDelegatesList.Clear();
                socketConnectionInfoList.Clear();
                webSocketServer.Dispose();
                webSocketServer = new WebSocketServer(serverAddress);
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
                    socketList.Add(socket);
                    socketUrlList.Add(clientUrl);
                    socketConnectionInfoList.Add(socket, clientUrl);
                };
                socket.OnClose = () =>
                {
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    int socketIndex = socketList.IndexOf(socket);
                    if (socketIndex != -1)
                    {
                        socketConnectionInfoList.Remove(socket);
                        socketList.RemoveAt(socketIndex);
                        socketUrlList.RemoveAt(socketIndex);
                        try
                        {
                            stringBuilderDelegatesList.Remove(clientUrl);
                        }
                        catch (Exception) { };
                        try
                        {
                            stringSepratorDelegatesList.Remove(clientUrl);
                        }
                        catch (Exception) { };
                    }
                }; 
                socket.OnMessage = message =>
                {
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    DealwithMessage(clientUrl, message);
                };
            });
        }

        static public bool SetSocketDataProvider(string clientUrl, StringBuilderDelegate stringBuilderDelegate, StringSepratorDelegate stringSepratorDelegate)
        {
            bool retValue = false;
            try
            {
                
            }
            catch (Exception) { };
            return retValue;
        }

        static public void DealwithMessage(string clientUrl, string streamData)
        {

        }
    }
}
