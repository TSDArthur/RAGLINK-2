using System;

namespace OpenBve.RPlatform
{
	public class DataManagerClient
	{
		static public void EventsRegister()
		{
			RAGLINKCommons.RPlatform.DataManager.UpdateDataEvent += UpdateData;
			RAGLINKCommons.RPlatform.DataManager.ApplyTrainDataEvent += ApplyTrainData;
			RAGLINKCommons.RPlatform.DataManager.ResetTrainData();
		}
		static public void UpdateData(RAGLINKCommons.RPlatform.DataManager.TrainDataClassify updateMode)
		{
			for (int i = 0; i < RAGLINKCommons.RPlatform.DataManager.processData.GetTrainDataCount(); i++)
			{
				if (updateMode != RAGLINKCommons.RPlatform.DataManager.TrainDataClassify.ALL_DATA && RAGLINKCommons.RPlatform.DataManager.processData.trainDataClassify[i] != updateMode)
				{
					continue;
				}

				switch (i)
				{
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.MASTER_KEY:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentMasterKeyState());
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.REVERSER:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentReverserState());
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.POWER:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentPowerHandle());
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.BRAKE:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentBrakeHandle());
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.SIGNAL:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetNextSectionSignalInfo());
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.SIGNAL_DIST:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetNextSectionSignalDistance());
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.SPEED:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToDouble(TrainMethods.GetCurrentTrainSpeedDouble());
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.SPEED_LIMIT:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentSectionSpeedLimit());
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.HORN:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentHornState());
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.SPEED_CONST:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentATCState());
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.EMERGENCY:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentEmergencyState());
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.LDOOR_OPEN:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetLeftDoorState() != -1 ? TrainMethods.GetLeftDoorState() : 0);
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.RDOOR_OPEN:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetRightDoorState() != -1 ? TrainMethods.GetRightDoorState() : 0);
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.CURRENT_STATION_INDEX:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentStationIndex();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.CURRENT_STATION_NAME:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentStationName();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.NEXT_STATION_INDEX:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetNextStationIndex();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.NEXT_STATION_NAME:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetNextStationName();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.CURRENT_STATION_DEPART_TIME:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentStationDepartureTime();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.NEXT_STATION_ARRIVAL_TIME:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetNextStationArrialTime();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.NEXT_STATION_DISTANCE:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetNextStationDistance();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.CYLINER_PRESSURE:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentCylinderPressure();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.PIPE_PRESSURE:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentPipePressure();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.DESTINATION_STATION_NAME:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetDestinationStationName();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.STATION_COUNT:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetStationCount();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.CURRENT_TIME:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentTime();
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.HMI_INSTRUCTIONS:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = RAGLINKCommons.RPlatform.DataManager.HMIData;
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.NEXT_STATION_STOP:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = ((int)(TrainMethods.GetNextStationStopMode()));
							break;
						}
					case (int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.TRACK_POSITION:
						{
							RAGLINKCommons.RPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentTrackPosition();
							break;
						}	
				}
			}
		}
		static public void ApplyTrainData()
		{
			TrainMethods.SetMasterKeyState((int)RAGLINKCommons.RPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.MASTER_KEY]);
			TrainMethods.SetTrainReverser((int)RAGLINKCommons.RPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.REVERSER]);
			TrainMethods.SetTrainPowerHandle((int)RAGLINKCommons.RPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.POWER]);
			TrainMethods.SetTrainBrakeHandle((int)RAGLINKCommons.RPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.BRAKE]);
			TrainMethods.SetHornState((int)RAGLINKCommons.RPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.HORN]);
			TrainMethods.SetEmergencyState((int)RAGLINKCommons.RPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.EMERGENCY]);
			TrainMethods.SetLeftDoorState((int)RAGLINKCommons.RPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.LDOOR_OPEN]);
			TrainMethods.SetRightDoorState((int)RAGLINKCommons.RPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.RDOOR_OPEN]);
			TrainMethods.SetATCSystemState(Convert.ToBoolean(RAGLINKCommons.RPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.SPEED_CONST]));
			TrainMethods.SetDeadmanSystemEnable(Convert.ToBoolean(RAGLINKCommons.RPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.DEADMAN_ENABLE]));
			TrainMethods.SetResetDeadmanSystem(Convert.ToBoolean(RAGLINKCommons.RPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RPlatform.DataManager.TrainDataMap.DEADMAN_PRESS]));
		}
	}
}
