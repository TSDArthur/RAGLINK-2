using System;
using System.Collections.Generic;

namespace RAGLINKCommons.RAGLINKPlatform
{
    public class DevicesManager
	{
		public enum DevicesIOMode
		{
			DEVICE_WITHOUT_INIT = -1,
			DEVICE_BUTTON_AUTOLOCK = 0,
			DEVICE_BUTTON_AUTORESET = 1,
			DEVICE_DIGITAL_INPUT = 2,
			DEVICE_DIGITAL_OUTPUT = 3,
			DEVICE_ANALOG_INPUT = 4,
			DEVICE_ANALOG_OUTPUT = 5,
			DEVICE_SERIAL = 6
		}
		static public Dictionary<int, DevicesIOMode> devicesInitMode = new Dictionary<int, DevicesIOMode>();
		static public Dictionary<int, int> deviceValue = new Dictionary<int, int>();
		static public int GetDeviceValue(int deviceID)
		{
			int retValue = 0;
			retValue = deviceValue[deviceID];
			return retValue;
		}
		static public void SetDeviceValue(int deviceID, int dataValue)
		{
			deviceValue[deviceID] = dataValue;
		}
		static public void UpdateDevicesInitMode()
		{
			try
			{
				for (int i = 0; i < BoardsManager.boardInfo.boardIOCount; i++)
				{
					devicesInitMode[i] = ControlObjects.GetBoardDeviceIOMode(i);
					deviceValue[i] = 0;
				}
			}
			catch (Exception) { };
		}
		static public string GetDevicesSetupDataStream()
		{
			string retValue = string.Empty;
			try
			{
				if (!BoardsManager.boardInfo.boardFileLoaded) return retValue;
				UpdateDevicesInitMode();
				for (int i = 0; i < BoardsManager.boardInfo.boardIOCount; i++)
				{
					DevicesIOMode initMode = devicesInitMode[i];
					if (initMode == DevicesIOMode.DEVICE_WITHOUT_INIT || initMode == DevicesIOMode.DEVICE_SERIAL) continue;
					retValue += (char)BoardsManager.boardInfo.COMM_SET_SYMBOL;
					retValue += (char)i;
					switch (initMode)
					{
						case DevicesIOMode.DEVICE_BUTTON_AUTOLOCK:
							{
								retValue += (char)BoardsManager.boardInfo.DEVICE_BUTTON_AUTOLOCK;
								break;
							}
						case DevicesIOMode.DEVICE_BUTTON_AUTORESET:
							{
								retValue += (char)BoardsManager.boardInfo.DEVICE_BUTTON_AUTORESET;
								break;
							}
						case DevicesIOMode.DEVICE_DIGITAL_INPUT:
							{
								retValue += (char)BoardsManager.boardInfo.DEVICE_DIGITAL_INPUT;
								break;
							}
						case DevicesIOMode.DEVICE_DIGITAL_OUTPUT:
							{
								retValue += (char)BoardsManager.boardInfo.DEVICE_DIGITAL_OUTPUT;
								break;
							}
						case DevicesIOMode.DEVICE_ANALOG_INPUT:
							{
								retValue += (char)BoardsManager.boardInfo.DEVICE_ANALOG_INPUT;
								break;
							}
						case DevicesIOMode.DEVICE_ANALOG_OUTPUT:
							{
								retValue += (char)BoardsManager.boardInfo.DEVICE_ANALOG_OUTPUT;
								break;
							}
					}
				}
			}
			catch (Exception) { };
			return retValue;
		}
		static public string GetDevicesDataStream()
		{
			string retValue = string.Empty;
			try
			{
				ControlObjects.ControlObjectDoEvents();
				for (int i = 0; i < BoardsManager.boardInfo.boardIOCount; i++)
				{
					DevicesIOMode initMode = devicesInitMode[i];
					if (initMode == DevicesIOMode.DEVICE_ANALOG_OUTPUT || initMode == DevicesIOMode.DEVICE_DIGITAL_OUTPUT)
					{
						retValue += (char)i;
						retValue += (char)deviceValue[i];
					}
				}
			}
			catch (Exception) { };
			return retValue;
		}
		static public bool BoardStreamToDevicesValue(string dataStream)
		{
			bool retValue = false;
			try
			{
				if (!BoardsManager.boardInfo.boardFileLoaded)
				{
					retValue = false;
					return retValue;
				}
				for (int i = 1; i < dataStream.Length - 1;)
				{
					SetDeviceValue(dataStream[i], dataStream[i + 1]);
					i += 2;
				}
				retValue = true;
			}
			catch (Exception) { };
			return retValue;
		}
		static public void BoardStreamToDevicesApply()
		{
			try
			{
				ControlObjects.ControlObjectDoEvents();
				DataManager.ApplyTrainData();
			}
			catch (Exception) { };
		}
	}
}
