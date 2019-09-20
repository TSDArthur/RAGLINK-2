using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace RAGLINKCommons.RAGLINKPlatform
{
    public class Communication
	{
		public enum CommunicationState
		{
			STATE0 = 0,
			STATE1 = 1,
			STATE2 = 2,
			STATE3 = 3
		}
		public struct SerialByControlObject
		{
			public int serialControlObject;
			public string controlObjectSerialPort;
			public int controlObjectSerialBaud;
		};
		public const int serialBufferSize = 1024;
		static public int serialCount = 1;
		static public List<SerialByControlObject> serialByControlObject = new List<SerialByControlObject>();
		static public List<SerialPort> serialList = new List<SerialPort>();
		static public CommunicationState[] communicationStateList;
		public delegate void UpdateStreamEventHandler(string dataStream);
		static public UpdateStreamEventHandler updateText = new UpdateStreamEventHandler(BoardSerialEvent);
		static public string boardSerialStream = string.Empty;
		static public bool dealWithBoardSerialStream = false;
		static public string boardSentStream = string.Empty;
		static public List<string> diviedDataStream = new List<string> ();
		static public int divedDataStreamCount = 0;
		static public int divedDataStreamCurrentSendID = 0;
		static public class StreamRecord
		{
			public class StreamRecordCombined
			{
				public int recordType;
				public string dataStream;
				public StreamRecordCombined()
				{
					recordType = 0;
					dataStream = string.Empty;
				}
			};
			static public int recordLimit = 1000;
			static public bool inProcess = false;
			static public List<string> boardSerialStreamRecord = new List<string>();
			static public List<string> boardSentStreamRecord = new List<string>();
			static public List<StreamRecordCombined> boardStreamCombined = new List<StreamRecordCombined>(); 
			static public void Clear()
			{
				inProcess = false;
				boardSerialStreamRecord.Clear();
				boardSentStreamRecord.Clear();
				boardStreamCombined.Clear();
			}
			static public void InsertRecievedStream(string dataStream)
			{
				while (inProcess) { };
				inProcess = true;
				StreamRecordCombined streamRecordCombined = new StreamRecordCombined
				{
					dataStream = dataStream,
					recordType = 0
				};
				if (boardStreamCombined.Count == recordLimit) Clear();
				boardSerialStreamRecord.Add(dataStream);
				boardStreamCombined.Add(streamRecordCombined);
				inProcess = false;
			}
			static public void InsertSentStream(string dataStream)
			{
				while (inProcess) { };
				inProcess = true;
				StreamRecordCombined streamRecordCombined = new StreamRecordCombined
				{
					dataStream = dataStream,
					recordType = 1
				};
				if (boardSentStreamRecord.Count == recordLimit) Clear();
				boardSentStreamRecord.Add(dataStream);
				boardStreamCombined.Add(streamRecordCombined);
				inProcess = false;
			}
		};
		static public class BoardStreamCommonFunctions
		{
			static public bool devicesConfigFlashed = false;
			static public bool specialDataExpected = false;
			static public bool inProcessSendDiviedPack = false;
			static public int expectSpecialDataNum = 0;
			static public void SendSpecialData(int dataNum)
			{
				try
				{
					if (serialList[0] == null || !BoardsManager.boardInfo.boardFileLoaded) return;
					string sendData = string.Empty;
					sendData += (char)BoardsManager.boardInfo.COMM_START_SYMBOL;
					for (int i = 0; i < dataNum; i++) sendData += (char)BoardsManager.boardInfo.COMM_SPEC_SYMBOL;
					sendData += (char)BoardsManager.boardInfo.COMM_END_SYMBOL;
					SendDataViaSerial(0, sendData);
				}
				catch (Exception) { };
			}
			static public bool SpecialSymbolRepeatTime(string dataStream, int repeatTime)
			{
				bool retValue = false;
				int symbolCounter = 0;
				for (int i = 1; i < dataStream.Length - 1; i++)
				{
					if ((int)dataStream[i] == BoardsManager.boardInfo.COMM_SPEC_SYMBOL)
					{
						symbolCounter++;
					}
					else
					{
						retValue = false;
						return retValue;
					}
				}
				if (symbolCounter == repeatTime) retValue = true;
				return retValue;
			}
			static public void SetSpecialDataExpect(int expectDataNum)
			{
				specialDataExpected = true;
				expectSpecialDataNum = expectDataNum;
			}
			static public List<string> GetDiviedDataPack(string fullStream)
			{
				List<string> retValue = new List<string>();
				try
				{
					retValue.Clear();
					int singleDataSize = (int)fullStream[0] == BoardsManager.boardInfo.COMM_SET_SYMBOL ? 3 : 2;
					int dataCount = fullStream.Length / singleDataSize;
					int perDeviedPackDataCount = (BoardsManager.boardInfo.COMM_PACK_MAX_SIZE - 2) / singleDataSize;
					int startPos = 0;
					while (dataCount > 0)
					{
						if (dataCount - perDeviedPackDataCount >= 0)
						{
							string diviedData = string.Empty;
							diviedData += (char)BoardsManager.boardInfo.COMM_START_SYMBOL;
							diviedData += fullStream.Substring(startPos, perDeviedPackDataCount * singleDataSize);
							diviedData += (char)BoardsManager.boardInfo.COMM_END_SYMBOL;
							retValue.Add(diviedData);
							startPos += perDeviedPackDataCount * singleDataSize;
							dataCount -= perDeviedPackDataCount;
						}
						else
						{
							string diviedData = string.Empty;
							diviedData += (char)BoardsManager.boardInfo.COMM_START_SYMBOL;
							diviedData += fullStream.Substring(startPos, fullStream.Length - startPos);
							diviedData += (char)BoardsManager.boardInfo.COMM_END_SYMBOL;
							retValue.Add(diviedData);
							dataCount = 0;
						}
					}
				}
				catch (Exception) { };
				return retValue;
			}
		}
		//functions
		static public List<string> GetSerialPortNames()
		{
			List<string> retValue = new List<string>();
			try
			{
				retValue.AddRange(SerialPort.GetPortNames());
			}
			catch (Exception) { };
			return retValue;
		}
		public static string ByteToHexStr(List<byte>byteArray)
		{
			string retValue = string.Empty;
			if (byteArray.Count != 0)
			{
				for (int i = 0; i < byteArray.Count; i++)
				{
					retValue += byteArray[i].ToString("X2");
					retValue += " ";
				}
			}
			return retValue;
		}
		public static string ByteToHexStr(string dataStream)
		{
			string retValue = string.Empty;
			if (dataStream.Length != 0)
			{
				for (int i = 0; i < dataStream.Length; i++)
				{
					retValue += ((byte)dataStream[i]).ToString("X2");
					retValue += " ";
				}
			}
			return retValue;
		}
		static public bool ResetSerial()
		{
			bool retValue = false;
			try
			{
				serialByControlObject.Clear();
				foreach (SerialPort serialPort in serialList)
				{
					try
					{
						serialPort.Close();
					}
					catch (Exception) { };
				}
				StreamRecord.Clear();
				serialList.Clear();
				serialCount = 1;
				retValue = true;
			}
			catch (Exception) { };
			return retValue;
		}
		static public bool InitializeSerial()
		{
			bool retValue = false;
			try
			{
				if (!BoardsManager.boardInfo.boardFileLoaded || !ProjectsManager.projectInfo.fileLoaded) return retValue;
				communicationStateList = new CommunicationState[serialCount];
				//Init board serial
				serialList.Clear();
				for (int i = 0; i < serialCount; i++)
				{
					serialList.Add(new SerialPort());
					if (i > 0)
					{
						serialList[i].BaudRate = serialByControlObject[i - 1].controlObjectSerialBaud;
						serialList[i].PortName = serialByControlObject[i - 1].controlObjectSerialPort;
					}
					else
					{
						serialList[i].BaudRate = BoardsManager.boardInfo.boardBaud;
						serialList[i].PortName = ProjectsManager.projectInfo.boardPort;
					}
					serialList[i].ReadBufferSize = serialBufferSize;
					serialList[i].WriteBufferSize = serialBufferSize;
					communicationStateList[i] = CommunicationState.STATE0;
				}
				serialList[0].DataReceived += new SerialDataReceivedEventHandler(BoardSerialDataReceived);
				retValue = true;
			}
			catch (Exception) { };
			return retValue;
		}
		static public void SerialConnect()
		{
			try
			{
				if (!BoardsManager.boardInfo.boardFileLoaded || !ProjectsManager.projectInfo.fileLoaded) return;
				foreach (SerialPort serialPort in serialList)
				{
					try
					{
						if (!serialPort.IsOpen)
						{
							serialPort.Open();
						}
					}
					catch (Exception) { };
				}
			}
			catch (Exception) { };
		}
		static public void SerialConnect(int serialID)
		{
			try
			{
				if (!serialList[serialID].IsOpen)
				{
					serialList[serialID].Open();
				}
			}
			catch (Exception) { };
		}
		static private void BoardSerialDataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			if (dealWithBoardSerialStream) return;
			try
			{
				if (communicationStateList[0] == CommunicationState.STATE0)
				{
					serialList[0].DiscardInBuffer();
					serialList[0].DiscardOutBuffer();
					return;
				}
				string readStream = serialList[0].ReadExisting();
				BoardSerialEvent(readStream);
			}
			catch (Exception) { };
		}
		static public void BoardSerialEvent(string dataStream)
		{
			boardSerialStream += dataStream;
			if ((int)boardSerialStream[boardSerialStream.Length - 1] != BoardsManager.boardInfo.COMM_END_SYMBOL)
			{
				dealWithBoardSerialStream = false;
				return;
			}
			dealWithBoardSerialStream = true;
			int streamStartPoint = -1;
			int streamEndPoint = boardSerialStream.Length - 1;
			for (int i = streamEndPoint; i >= 0; i--)
			{
				if ((int)boardSerialStream[i] == BoardsManager.boardInfo.COMM_START_SYMBOL)
				{
					streamStartPoint = i;
					break;
				}
			}
			if (streamStartPoint == -1)
			{
				boardSerialStream = string.Empty;
				dealWithBoardSerialStream = false;
				return;
			}
			boardSerialStream = boardSerialStream.Substring(streamStartPoint, streamEndPoint - streamStartPoint + 1);
			StreamRecord.InsertRecievedStream(ByteToHexStr(boardSerialStream));
			BoardSerialStreamDoEvents(boardSerialStream);
			boardSerialStream = string.Empty;
			dealWithBoardSerialStream = false;
		}
		static public void BoardSerialStreamDoEvents(string dataStream)
		{
			bool getDataToConnect = BoardStreamCommonFunctions.SpecialSymbolRepeatTime(dataStream, BoardsManager.boardInfo.COMM_CONNECT_SPEC_NUM);
			if (communicationStateList[0] != CommunicationState.STATE1 && getDataToConnect)
			{
				BoardStreamCommonFunctions.specialDataExpected = false;
				communicationStateList[0] = CommunicationState.STATE1;
			}
			if (communicationStateList[0] == CommunicationState.STATE1 && !getDataToConnect)
			{
				BoardStreamCommonFunctions.SendSpecialData(BoardsManager.boardInfo.COMM_RESET_SPEC_NUM);
				BoardStreamCommonFunctions.specialDataExpected = false;
				communicationStateList[0] = CommunicationState.STATE1;
			}
			startPoint:
			if (communicationStateList[0] == CommunicationState.STATE1)
			{
				if (!BoardStreamCommonFunctions.specialDataExpected)
					BoardStreamCommonFunctions.SetSpecialDataExpect(BoardsManager.boardInfo.COMM_CONNECT_SPEC_NUM);
				if (BoardStreamCommonFunctions.SpecialSymbolRepeatTime(dataStream, BoardStreamCommonFunctions.expectSpecialDataNum))
				{
					BoardStreamCommonFunctions.devicesConfigFlashed = false;
					BoardStreamCommonFunctions.specialDataExpected = false;
					BoardStreamCommonFunctions.inProcessSendDiviedPack = false;
					BoardStreamCommonFunctions.SendSpecialData(BoardsManager.boardInfo.COMM_CONNECT_SPEC_NUM);
					Thread.Sleep(200);
					communicationStateList[0] = CommunicationState.STATE2;
					goto startPoint;
				}
				return;
			}
			else if (communicationStateList[0] == CommunicationState.STATE2)
			{
				//Send setup data
				if (!BoardStreamCommonFunctions.devicesConfigFlashed)
				{
					if (!BoardStreamCommonFunctions.inProcessSendDiviedPack)
					{
						BoardStreamCommonFunctions.SendSpecialData(BoardsManager.boardInfo.COMM_CLEAR_DEVICES_STATE_NUM);
						Thread.Sleep(200);
						diviedDataStream.Clear();
						diviedDataStream = BoardStreamCommonFunctions.GetDiviedDataPack(DevicesManager.GetDevicesSetupDataStream());
						divedDataStreamCount = diviedDataStream.Count;
						divedDataStreamCurrentSendID = 0;
						BoardStreamCommonFunctions.inProcessSendDiviedPack = true;
					}
					if (divedDataStreamCount > 0)
					{
						SendDataViaSerial(0, diviedDataStream[divedDataStreamCurrentSendID]);
						divedDataStreamCurrentSendID++;
						divedDataStreamCount--;
						communicationStateList[0] = CommunicationState.STATE3;
						BoardStreamCommonFunctions.SetSpecialDataExpect(BoardsManager.boardInfo.COMM_CONTINUE_SPEC_NUM);
						return;
					}
					BoardStreamCommonFunctions.SendSpecialData(BoardsManager.boardInfo.COMM_TRANSEND_SPEC_NUM);
					diviedDataStream.Clear();
					BoardStreamCommonFunctions.inProcessSendDiviedPack = false;
					BoardStreamCommonFunctions.specialDataExpected = false;
					BoardStreamCommonFunctions.devicesConfigFlashed = true;
					communicationStateList[0] = CommunicationState.STATE3;
					return;
				}
				//Send device data
				if (!BoardStreamCommonFunctions.inProcessSendDiviedPack)
				{
					diviedDataStream.Clear();
					diviedDataStream = BoardStreamCommonFunctions.GetDiviedDataPack(DevicesManager.GetDevicesDataStream());
					divedDataStreamCount = diviedDataStream.Count;
					divedDataStreamCurrentSendID = 0;
					BoardStreamCommonFunctions.inProcessSendDiviedPack = true;
				}
				if (divedDataStreamCount > 0)
				{
					SendDataViaSerial(0, diviedDataStream[divedDataStreamCurrentSendID]);
					divedDataStreamCurrentSendID++;
					divedDataStreamCount--;
					communicationStateList[0] = CommunicationState.STATE3;
					BoardStreamCommonFunctions.SetSpecialDataExpect(BoardsManager.boardInfo.COMM_CONTINUE_SPEC_NUM);
					return;
				}
				BoardStreamCommonFunctions.SendSpecialData(BoardsManager.boardInfo.COMM_TRANSEND_SPEC_NUM);
				diviedDataStream.Clear();
				BoardStreamCommonFunctions.inProcessSendDiviedPack = false;
				BoardStreamCommonFunctions.specialDataExpected = false;
				BoardStreamCommonFunctions.devicesConfigFlashed = true;
				communicationStateList[0] = CommunicationState.STATE3;
				return;
			}
			if (communicationStateList[0] == CommunicationState.STATE3)
			{
				if (BoardStreamCommonFunctions.specialDataExpected
				&& BoardStreamCommonFunctions.SpecialSymbolRepeatTime(dataStream, BoardStreamCommonFunctions.expectSpecialDataNum))
				{
					BoardStreamCommonFunctions.specialDataExpected = false;
					communicationStateList[0] = CommunicationState.STATE2;
					goto startPoint;
				}
				//Get board devices value
				if (BoardStreamCommonFunctions.SpecialSymbolRepeatTime(dataStream, BoardsManager.boardInfo.COMM_TRANSEND_SPEC_NUM))
				{
					DevicesManager.BoardStreamToDevicesApply();
					communicationStateList[0] = CommunicationState.STATE2;
					goto startPoint;
				}
				DevicesManager.BoardStreamToDevicesValue(dataStream);
				BoardStreamCommonFunctions.SendSpecialData(BoardsManager.boardInfo.COMM_CONTINUE_SPEC_NUM);
				return;
			}
		}
		static public bool SendDataViaSerial(int serialID, string dataValue)
		{
			bool retValue = false;
			try
			{
				if (serialList[serialID] == null || !serialList[serialID].IsOpen)
				{
					retValue = false;
					return retValue;
				}
				while (serialList[serialID].BytesToWrite > 0) { };
				serialList[serialID].Write(dataValue);
				if (serialID == 0)
				{
					boardSentStream = ByteToHexStr(dataValue);
					StreamRecord.InsertSentStream(boardSentStream);
				}
				while (serialList[serialID].BytesToWrite > 0) { };
				retValue = true;
			}
			catch (Exception) { };
			return retValue;
		}
		static public bool SendDataViaSerial(int serialID, byte dataValue)
		{
			bool retValue = false;
			try
			{
				if (serialList[serialID] == null || !serialList[serialID].IsOpen)
				{
					retValue = false;
					return retValue;
				}
				while (serialList[serialID].BytesToWrite > 0) { };
				List<byte> dataValueByteArray = new List<byte>();
				dataValueByteArray.Clear();
				dataValueByteArray.Add(dataValue);
				serialList[serialID].Write(dataValueByteArray.ToArray(), 0, 1);
				if (serialID == 0)
				{
					boardSentStream = ByteToHexStr(dataValueByteArray);
					StreamRecord.InsertSentStream(boardSentStream);
				}
				while (serialList[serialID].BytesToWrite > 0) { };
				retValue = true;
			}
			catch (Exception) { };
			return retValue;
		}
		static public bool SendDataViaSerial(int serialID, List<byte>dataValue)
		{
			bool retValue = false;
			try
			{
				if (serialList[serialID] == null || !serialList[serialID].IsOpen)
				{
					retValue = false;
					return retValue;
				}
				while (serialList[serialID].BytesToWrite > 0) { };
				serialList[serialID].Write(dataValue.ToArray(), 0, dataValue.Count);
				if (serialID == 0)
				{
					boardSentStream = ByteToHexStr(dataValue);
					StreamRecord.InsertSentStream(boardSentStream);
				}
				while (serialList[serialID].BytesToWrite > 0) { };
				retValue = true;
			}
			catch (Exception) { };
			return retValue;
		}
	}
}
