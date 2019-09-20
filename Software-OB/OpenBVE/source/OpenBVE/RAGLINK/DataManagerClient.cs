using System;

namespace OpenBve.RAGLINKPlatform
{
	public class DataManagerClient
	{
		static public void EventsRegister()
		{
			RAGLINKCommons.RAGLINKPlatform.DataManager.UpdateDataEvent += UpdateData;
			RAGLINKCommons.RAGLINKPlatform.DataManager.ApplyTrainDataEvent += ApplyTrainData;
			RAGLINKCommons.RAGLINKPlatform.DataManager.ResetTrainData();
		}
		static public void UpdateData(RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataClassify updateMode)
		{
			for (int i = 0; i < RAGLINKCommons.RAGLINKPlatform.DataManager.processData.GetTrainDataCount(); i++)
			{
				if (updateMode != RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataClassify.ALL_DATA && RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainDataClassify[i] != updateMode) continue;
				switch (i)
				{
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.MASTER_KEY:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentMasterKeyState());
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.REVERSER:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentReverserState());
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.POWER:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentPowerHandle());
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.BRAKE:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentBrakeHandle());
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.SIGNAL:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetNextSectionSignalInfo());
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.SIGNAL_DIST:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetNextSectionSignalDistance());
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.SPEED:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentTrainSpeed());
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.SPEED_HMI:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(((int)(TrainMethods.GetCurrentTrainSpeedDouble() * 10)));
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.SPEED_LIMIT:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentSectionSpeedLimit());
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.HORN:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentHornState());
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.SPEED_CONST:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentATCState());
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.EMERGENCY:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetCurrentEmergencyState());
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.LDOOR_OPEN:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetLeftDoorState() != -1 ? TrainMethods.GetLeftDoorState() : 0);
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.RDOOR_OPEN:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = Convert.ToInt32(TrainMethods.GetRightDoorState() != -1 ? TrainMethods.GetRightDoorState() : 0);
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.CURRENT_STATION_NAME:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentStationName();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.NEXT_STATION_NAME:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = TrainMethods.GetNextStationName();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.CURRENT_STATION_DEPART_TIME:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentStationDepartureTime();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.NEXT_STATION_ARRIVAL_TIME:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = TrainMethods.GetNextStationArrialTime();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.NEXT_STATION_DISTANCE:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = TrainMethods.GetNextStationDistance();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.CYLINER_PRESSURE:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentCylinderPressure();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.PIPE_PRESSURE:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentPipePressure();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.DESTINATION_STATION_NAME:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = TrainMethods.GetDestinationStationName();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.STATION_COUNT:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = TrainMethods.GetStationCount();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.CURRENT_TIME:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = TrainMethods.GetCurrentTime();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.HMI_INSTRUCTIONS:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = RAGLINKCommons.RAGLINKPlatform.DataManager.HMIData;
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.NEXT_STATION_DISTANCE_HMI:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = (TrainMethods.GetNextStationDistance() >= 1000 ?
							decimal.Round(decimal.Parse((TrainMethods.GetNextStationDistance() / 1000).ToString()), 2) :
							decimal.Round(decimal.Parse((TrainMethods.GetNextStationDistance()).ToString()), 1)) +
							(TrainMethods.GetNextStationDistance() >= 1000 ? " KM" : " M") +
							" / " + (TrainMethods.GetNextStationStopMode() == 1 ? "VIA" : "STOP");
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.CYLINER_PRESSURE_HMI:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = ((int)(TrainMethods.GetCurrentCylinderPressure() * 10)).ToString();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.PIPE_PRESSURE_HMI:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = ((int)(TrainMethods.GetCurrentPipePressure() * 10)).ToString();
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.NEXT_STATION_STOP:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = ((int)(TrainMethods.GetNextStationStopMode()));
							break;
						}
					case (int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.STATION_COUNT_HMI:
						{
							RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[i] = TrainMethods.GetNextStationIndex().ToString() + " / " + TrainMethods.GetStationCount().ToString();
							break;
						}
				}
			}
		}
		static public void ApplyTrainData()
		{
			TrainMethods.SetMasterKeyState((int)RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.MASTER_KEY]);
			TrainMethods.SetTrainReverser((int)RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.REVERSER]);
			TrainMethods.SetTrainPowerHandle((int)RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.POWER]);
			TrainMethods.SetTrainBrakeHandle((int)RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.BRAKE]);
			TrainMethods.SetHornState((int)RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.HORN]);
			TrainMethods.SetEmergencyState((int)RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.EMERGENCY]);
			TrainMethods.SetLeftDoorState((int)RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.LDOOR_OPEN]);
			TrainMethods.SetRightDoorState((int)RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.RDOOR_OPEN]);
			TrainMethods.SetATCSystemState(Convert.ToBoolean(RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.SPEED_CONST]));
			TrainMethods.SetDeadmanSystemEnable(Convert.ToBoolean(RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.DEADMAN_ENABLE]));
			TrainMethods.SetResetDeadmanSystem(Convert.ToBoolean(RAGLINKCommons.RAGLINKPlatform.DataManager.processData.trainData[(int)RAGLINKCommons.RAGLINKPlatform.DataManager.TrainDataMap.DEADMAN_PRESS]));
		}
	}
}
