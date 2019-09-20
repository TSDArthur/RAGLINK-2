using RAGLINKCommons.RAGLINKProxy;
using System;
using System.Media;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace OpenBve
{
	public partial class TrainMethods
	{
		static private TrainManager.Train currentTrainInControl;
		//for ATC
		static int mainTimerTick = 100;
		private static bool ATCEnable = false;
		//for master key
		private static bool masterKeyEnable = false;
		//for main timer
		private static int inTimerMain = 0;
		private static System.Threading.Timer MainTimer;
		//for emergenct
		private static bool emergencyEnable = false;
		//for horn
		private static bool hornEnable = false;
		//for signal
		private static bool redLightEnable = false;
		//for timetable
		static Timetable.Table Table;
		static bool timetableReady = false;
		static double currentTime = 0;
		//for deadman system
		static bool deadmanSystemTrig = false;
		private static int deadmanSystemState = 0;
		static int deadmanTimetick1 = 30;
		static int deadmanTimetick2 = 10;
		static int deadmanCounter = 0;
		//for warning sound
		static bool warningSoundPlaying = false;
		static public string namespaceName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
		static public Assembly assemblyPath = Assembly.GetExecutingAssembly();
		static public SoundPlayer soundPlayer = new SoundPlayer(assemblyPath.GetManifestResourceStream(namespaceName + ".Resources" + ".alarm.wav"));
		static public void EventsRegister()
		{
			TrainMethodsClient.TimerAttachEvent += AttachMainTimerInterrupt;
			TrainMethodsClient.GetSimulatorStateEvent += GetSimulatorState;
			TrainMethodsClient.RestartSimulatorStateEvent += RestartSimulator;
		}
		static public bool RestartSimulator(string Args)
		{
			bool retValue = false;
			//try
			{
				System.Diagnostics.Process processManager;
				processManager = new System.Diagnostics.Process();
				processManager.StartInfo.FileName = RAGLINKCommons.RAGLINKPlatform.SettingsContent.platformPath + "\\OpenBVE.exe";
				processManager.StartInfo.Arguments = Args;
				processManager.Start();
				System.Environment.Exit(0);
				retValue = true;
				return retValue;
			}
			//catch (Exception) { };
			return retValue;
		}
		static public void AttachMainTimerInterrupt()
		{
			try
			{
				inTimerMain = 0;
				MainTimer = new System.Threading.Timer(new TimerCallback(MainTimerProcess), null, mainTimerTick, mainTimerTick);
			}
			catch (Exception) { };
		}
		static private void MainTimerProcess(object state)
		{
			if (Interlocked.Exchange(ref inTimerMain, 1) == 0)
			{
				try
				{
					currentTrainInControl = TrainManager.PlayerTrain;
					//current time
					currentTime = Game.SecondsSinceMidnight;
					//speed const ISP
					if (GetCurrentATCState())
					{
						SpeedConstSystemEvent();
					}
					//Deadman system
					if (GetDeadmanSystemEnable())
					{
						if ((deadmanCounter >= deadmanTimetick1 * 1000 / mainTimerTick) && deadmanSystemState == 0 && !emergencyEnable)
						{
							deadmanSystemState = 1;
							PlayWarningSound(true);
							deadmanCounter++;
						}
						else if (deadmanCounter >= ((deadmanTimetick1 + deadmanTimetick2) * 1000 / mainTimerTick))
						{
							deadmanSystemState = 2;
							SetEmergencyState(1);
						}
						else if (deadmanSystemState == 1)
						{
							deadmanCounter++;
						}
						else if (deadmanSystemState == 0)
						{
							PlayWarningSound(false);
							deadmanSystemState = 0;
							deadmanCounter++;
						}
					}
					else
					{
						PlayWarningSound(false);
						deadmanSystemState = 0;
						deadmanCounter = 0;
					}
				}
				catch (Exception) { };
				Interlocked.Exchange(ref inTimerMain, 0);
			}
		}
		static public void SetTrainPowerHandleUp()
		{
			try
			{
				int currretPower = currentTrainInControl.Handles.Power.Driver;
				int currentBrake = currentTrainInControl.Handles.Brake.Driver;
				if (currentTrainInControl.Handles.EmergencyBrake.Actual) currentTrainInControl.UnapplyEmergencyBrake();
				if (currentBrake > 0) currentTrainInControl.ApplyNotch(0, true, -1, true);
				else currentTrainInControl.ApplyNotch(1, true, 0, true);
				return;
			}
			catch (Exception) { };
		}
		static public void SetTrainPowerHandleDown()
		{
			try
			{
				int currretPower = currentTrainInControl.Handles.Power.Driver;
				int currentBrake = currentTrainInControl.Handles.Brake.Driver;
				if (currretPower > 0) currentTrainInControl.ApplyNotch(-1, true, 0, true);
				else currentTrainInControl.ApplyNotch(0, true, 1, true);
				return;
			}
			catch (Exception) { };
		}
		static public void SetTrainPowerHandle(int dataValue)
		{
			try
			{
				currentTrainInControl.ApplyNotch(dataValue < 0 ? 0 : dataValue, false, 0, true);
				return;
			}
			catch (Exception) { };
		}
		static public void SetTrainBrakeHandle(int dataValue)
		{
			try
			{
				currentTrainInControl.ApplyNotch(0, true, dataValue < 0 ? 0 : dataValue, false);
				return;
			}
			catch (Exception) { };
		}
		static public void SetTrainReverserForward()
		{
			try
			{
				currentTrainInControl.ApplyReverser(1, true);
				currentTrainInControl.ApplyReverser(1, true);
				return;
			}
			catch (Exception) { };
		}
		static public void SetTrainReverserNeutral()
		{
			try
			{
				currentTrainInControl.ApplyReverser(1, true);
				currentTrainInControl.ApplyReverser(1, true);
				currentTrainInControl.ApplyReverser(-1, true);
				return;
			}
			catch (Exception) { };
		}
		static public void SetTrainReverserBackward()
		{
			try
			{
				currentTrainInControl.ApplyReverser(-1, true);
				currentTrainInControl.ApplyReverser(-1, true);
				return;
			}
			catch (Exception) { };
		}
		static public void SetTrainReverser(int dataValue)
		{
			try
			{
				currentTrainInControl.ApplyReverser(dataValue, false);
				return;
			}
			catch (Exception) { };
		}
		static public int GetCurrentTrainSpeed()
		{
			int retValue = 0;
			try
			{
				int currentDriveCar = currentTrainInControl.DriverCar;
				retValue = (int)(currentTrainInControl.Cars[currentDriveCar].Specs.CurrentSpeed * 3.6);
				return retValue;
			}
			catch (Exception) { };
			return retValue;
		}
		static public double GetCurrentTrainSpeedDouble()
		{
			double retValue = 0;
			try
			{
				int currentDriveCar = currentTrainInControl.DriverCar;
				retValue = currentTrainInControl.Cars[currentDriveCar].Specs.CurrentSpeed * 3.6;
				return retValue;
			}
			catch (Exception) { };;
			return retValue;
		}
		static public int GetCurrentReverserState()
		{
			int retValue = 0;
			try
			{
				TrainManager.ReverserPosition ReverserPos = currentTrainInControl.Handles.Reverser.Driver;
				TrainManager.ReverserPosition Reverse = TrainManager.ReverserPosition.Reverse;
				TrainManager.ReverserPosition Neutral = TrainManager.ReverserPosition.Neutral;
				retValue = ReverserPos == Reverse ? -1 : (ReverserPos == Neutral ? 0 : 1);
				return retValue;
			}
			catch (Exception) { };
			return retValue;
		}
		static public int GetCurrentPowerHandle()
		{
			int retValue = 0;
			try
			{
				int currretPower = currentTrainInControl.Handles.Power.Driver;
				retValue = currretPower;
				return retValue;
			}
			catch (Exception) { };
			return retValue;
		}
		static public int GetCurrentBrakeHandle()
		{
			int retValue = 0;
			try
			{
				int currentBrake = currentTrainInControl.Handles.Brake.Driver;
				retValue = currentBrake;
				return retValue;
			}
			catch (Exception) { };
			return retValue;
		}
		static public int GetCurrentSectionSpeedLimit()
		{
			int retValue = 30;
			try
			{
				retValue = (int)Math.Min(Math.Abs(currentTrainInControl.CurrentSectionLimit * 3.6),
					Math.Abs(currentTrainInControl.CurrentRouteLimit * 3.6));
				if (retValue > 350 || retValue < 0) return 30;
				return retValue;
			}
			catch (Exception) { };
			return retValue;
		}
		static public int GetNextSectionSignalInfo()
		{
			int retValue = 0;
			try
			{
				int currentSection = currentTrainInControl.CurrentSectionIndex;
				int nextSection = Game.Sections[currentSection].NextSection;
				retValue = Game.Sections[nextSection].CurrentAspect;
				return retValue;
			}
			catch (Exception) { };
			return retValue;
		}
		static public int GetNextSectionSignalDistance()
		{
			int retValue = 0;
			try
			{
				int currentSection = currentTrainInControl.CurrentSectionIndex;
				int nextSection = Game.Sections[currentSection].NextSection;
				double nextSectionPos = Game.Sections[nextSection].TrackPosition;
				double currentSectionPos = currentTrainInControl.Cars[currentTrainInControl.DriverCar].FrontAxle.Follower.TrackPosition;
				retValue = (int)((nextSectionPos - currentSectionPos >= 0 ? nextSectionPos - currentSectionPos : 0));
				return retValue;
			}
			catch (Exception) { };
			return retValue;
		}
		static public void SetHornStart()
		{
			try
			{
				currentTrainInControl.Cars[currentTrainInControl.DriverCar].Horns[0].Play();
				return;
			}
			catch (Exception) { };
		}
		static public void SetHornStop()
		{
			try
			{
				currentTrainInControl.Cars[currentTrainInControl.DriverCar].Horns[0].Stop();
				return;
			}
			catch (Exception) { };
		}
		static public void SetHornState(int dataValue)
		{
			try
			{
				if (dataValue == 0)
				{
					hornEnable = false;
					SetHornStop();
				}
				else
				{
					if (!hornEnable)
					{
						SetHornStart();
						hornEnable = true;
					}
				}
			}
			catch (Exception) { };
		}
		static public bool GetCurrentHornState()
		{
			bool retValue = hornEnable;
			return retValue;
		}
		static public void SetATCSystemState(bool appliedEnable)
		{
			try
			{
				ATCEnable = appliedEnable;
				return;
			}
			catch (Exception)
			{
				ATCEnable = false;
			}
		}
		static public bool GetCurrentATCState()
		{
			bool retValue;
			retValue = ATCEnable;
			return retValue;
		}
		static private void SpeedConstSystemEvent()
		{
			double constSpeed = GetATCCurrentSpeed();
			double destSpeed = constSpeed;
			ConstSpeedController(true, constSpeed);
		}
		static private void ConstSpeedController(bool ignoreReadLight, double constSpeed)
		{
			if (constSpeed >= 0)
			{
				try
				{
					//set destSpeed
					double destSpeed = Math.Min(GetCurrentSectionSpeedLimit() / 3.6, constSpeed);
					//get accurate speed
					int currentDriveCar = TrainManager.PlayerTrain.DriverCar;
					double currentSpeed = (TrainManager.PlayerTrain.Cars[currentDriveCar].Specs.CurrentSpeed);
					double accSpeed = TrainManager.PlayerTrain.Cars[currentDriveCar].Specs.CurrentAcceleration;
					//
					if (redLightEnable && !ignoreReadLight)
					{
						if (GetNextSectionSignalDistance() / currentSpeed <= 15 && Math.Abs(currentSpeed / (Math.Abs(accSpeed) + 1e-5)) <= 30) destSpeed = 0;
						else destSpeed = Math.Min(30 / 3.6, destSpeed);
					}
					//
					double relativeSpeed = destSpeed - currentSpeed;
					double pid_k = 0.6;
					//PID control
					if (relativeSpeed > 0)
					{
						SetTrainBrakeHandle(0);
						if (accSpeed < 0) SetTrainPowerHandleUp();
						else if (accSpeed > 0)
						{
							if (Math.Abs(relativeSpeed / accSpeed) <= pid_k) SetTrainPowerHandleDown();
							else SetTrainPowerHandleUp();
						}
					}
					else if (relativeSpeed < 0)
					{
						SetTrainPowerHandle(0);
						if (accSpeed > 0) SetTrainPowerHandleDown();
						else if(accSpeed < 0)
						{
							if (Math.Abs(relativeSpeed / accSpeed) <= pid_k) SetTrainPowerHandleUp();
							else SetTrainPowerHandleDown();
						}
					}
				}
				catch (Exception) { };
			}
		}
		static public double GetATCCurrentSpeed()
		{
			double retValue;
			retValue = FacileATC.GetControlSpeedValue(GetCurrentTime(), GetNextStationArrialTime(),
				GetCurrentSectionSpeedLimit() / 3.6, GetCurrentTrainSpeedDouble() / 3.6, GetNextStationDistance());
			return retValue;
		}
		static public void SetMasterKeyState(int dataValue)
		{
			try
			{
				masterKeyEnable = dataValue == 0 ? false : true;
				if (!masterKeyEnable)
				{
					SetATCSystemState(false);
					SetTrainBrakeHandle(0);
					SetTrainPowerHandle(0);
					SetTrainReverserNeutral();
				}
			}
			catch (Exception) { };
		}
		static public bool GetCurrentMasterKeyState()
		{
			bool retValue;
			retValue = masterKeyEnable;
			return retValue;
		}
		static public void SetEmergencyState(int dataValue)
		{
			try
			{
				emergencyEnable = dataValue == 0 ? false : true;
				TrainManager.Train currentTrainInControl = TrainManager.PlayerTrain;
				if (emergencyEnable)
				{
					currentTrainInControl.ApplyEmergencyBrake();
				}
				else
				{
					currentTrainInControl.UnapplyEmergencyBrake();
				}
			}
			catch (Exception) { };
		}
		static public bool GetCurrentEmergencyState()
		{
			bool retValue;
			retValue = emergencyEnable;
			return retValue;
		}
		static public void UpdateCurrentTimeTable()
		{
			try
			{
				Table = Timetable.ControllerGetTimetable();
				timetableReady = true;
			}
			catch (Exception)
			{
				timetableReady = false;
			}
		}
		static public int GetNextStationIndex()
		{
			int errState = -1;
			int stationIndex = -1;
			try
			{
				if (!timetableReady) UpdateCurrentTimeTable();
				if (Table.Stations.Length == 0) return errState;
				stationIndex = Table.Stations.Length;
				for (int i = 0; i < Table.Stations.Length; i++)
				{
					double currentSectionPos = currentTrainInControl.Cars[currentTrainInControl.DriverCar].FrontAxle.Follower.TrackPosition + 2.3;
					int stationStopIndex = Game.Stations[i].GetStopIndex(currentTrainInControl.Cars.Length);
					double nextStationPos = Game.Stations[i].Stops[stationStopIndex].TrackPosition;
					if (currentSectionPos <= nextStationPos)
					{
						stationIndex = i;
						break;
					}
				}
				if (stationIndex + 1 < Table.Stations.Length)
				{
					double currentSectionPos = currentTrainInControl.Cars[currentTrainInControl.DriverCar].FrontAxle.Follower.TrackPosition + 2.3;
					int currentStationStopIndex = Game.Stations[stationIndex].GetStopIndex(currentTrainInControl.Cars.Length);
					double currentStationPos = Game.Stations[stationIndex].Stops[currentStationStopIndex].TrackPosition;
					if (currentStationPos - currentSectionPos > 0 &&
						currentStationPos - currentSectionPos <= 50 &&
						(currentTrainInControl.StationState == TrainManager.TrainStopState.Boarding || currentTrainInControl.StationState == TrainManager.TrainStopState.Completed))
					{
						stationIndex++;
					}
				}
				return stationIndex;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public int GetDestinationStationID()
		{
			int errState = -1;
			int stationIndex = -1;
			try
			{
				if (!timetableReady) UpdateCurrentTimeTable();
				if (Table.Stations.Length == 0) return errState;
				stationIndex = Table.Stations.Length - 1;
				return stationIndex;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public string GetDestinationStationName()
		{
			string errState = "N/A";
			string stationName = String.Empty;
			try
			{
				if (!timetableReady) UpdateCurrentTimeTable();
				int destStationIndex = GetDestinationStationID();
				if (Table.Stations.Length == 0 || destStationIndex - 1 < 0) return errState;
				stationName = Table.Stations[destStationIndex].Name;
				return stationName;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public int GetStationCount()
		{
			int retValue = 0;
			try
			{
				retValue = Table.Stations.Length;
				return retValue < 0 ? 0 : retValue;
			}
			catch (Exception) { };
			return retValue;
		}
		static public string GetCurrentStationName()
		{
			string errState = "N/A";
			string stationName = String.Empty;
			try
			{
				if (!timetableReady) UpdateCurrentTimeTable();
				int nextStationIndex = GetNextStationIndex();
				if (Table.Stations.Length == 0 || nextStationIndex - 1 < 0) return errState;
				stationName = Table.Stations[nextStationIndex - 1].Name;
				return stationName;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public string GetNextStationName()
		{
			string errState = "N/A";
			string stationName = String.Empty;
			try
			{
				if (!timetableReady) UpdateCurrentTimeTable();
				int nextStationIndex = GetNextStationIndex();
				if (nextStationIndex == -1) return errState;
				if (Table.Stations.Length == 0 || nextStationIndex >= Table.Stations.Length) return errState;
				stationName = Table.Stations[nextStationIndex].Name;
				return stationName;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public double GetNextStationDistance()
		{
			double errState = 0;
			double stationDis = 0;
			try
			{
				if (!timetableReady) UpdateCurrentTimeTable();
				if (Table.Stations.Length == 0) return errState;
				//
				int nextStationIndex = GetNextStationIndex();
				if (nextStationIndex == -1) return errState;
				int currentSection = currentTrainInControl.CurrentSectionIndex;
				int nextSection = Game.Sections[currentSection].NextSection;
				double currentSectionPos = currentTrainInControl.Cars[currentTrainInControl.DriverCar].FrontAxle.Follower.TrackPosition + 2.3;
				int stationStopIndex = Game.Stations[nextStationIndex].GetStopIndex(currentTrainInControl.Cars.Length);
				double nextStationPos = Game.Stations[nextStationIndex].Stops[stationStopIndex].TrackPosition;
				stationDis = nextStationPos - currentSectionPos;
				return stationDis < 0 ? 0 : stationDis;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public double GetPreStationDistance()
		{
			double errState = 0;
			double stationDis;
			try
			{
				if (!timetableReady) UpdateCurrentTimeTable();
				if (Table.Stations.Length == 0) return errState;
				//
				int nextStationIndex = GetNextStationIndex();
				if (nextStationIndex == -1 || nextStationIndex - 1 < 0) return errState;
				int currentSection = currentTrainInControl.CurrentSectionIndex;
				int nextSection = Game.Sections[currentSection].NextSection;
				double currentSectionPos = currentTrainInControl.Cars[currentTrainInControl.DriverCar].FrontAxle.Follower.TrackPosition + 2.3;
				int stationStopIndex = Game.Stations[nextStationIndex - 1].GetStopIndex(currentTrainInControl.Cars.Length);
				double preStationPos = Game.Stations[nextStationIndex - 1].Stops[stationStopIndex].TrackPosition;
				stationDis = currentSectionPos - preStationPos;
				return stationDis;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public int GetNextStationStopMode()
		{
			int errState = -1;
			int stationStopMode = 0;
			try
			{
				if (!timetableReady) UpdateCurrentTimeTable();
				if (Table.Stations.Length == 0) return errState;
				int nextStationIndex = GetNextStationIndex();
				if (nextStationIndex == -1) return errState;
				if (Table.Stations[nextStationIndex].Pass) stationStopMode = 1;
				else stationStopMode = 2;
				return stationStopMode;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public string GetNextStationArrialTime()
		{
			string errState = "N/A";
			string arrivalTime = String.Empty;
			try
			{
				if (!timetableReady) UpdateCurrentTimeTable();
				int nextStationIndex = GetNextStationIndex();
				if (nextStationIndex == -1) return errState;
				if (Table.Stations.Length == 0 || nextStationIndex >= Table.Stations.Length) return errState;
				string Hour, Min, Sec;
				if (Table.Stations[nextStationIndex].Pass)
				{
					Hour = Table.Stations[nextStationIndex].Departure._Hour;
					Min = Table.Stations[nextStationIndex].Departure.Minute;
					Sec = Table.Stations[nextStationIndex].Departure.Second;
				}
				else
				{
					Hour = Table.Stations[nextStationIndex].Arrival._Hour;
					Min = Table.Stations[nextStationIndex].Arrival.Minute;
					Sec = Table.Stations[nextStationIndex].Arrival.Second;
				}
				if (Hour == string.Empty || Hour == " " || Hour == "  ") return errState;
				arrivalTime = Hour + ":" + Min + ":" + Sec;
				return arrivalTime;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public string GetCurrentStationDepartureTime()
		{
			string errState = "N/A";
			string departureTime = String.Empty;
			try
			{
				if (!timetableReady) UpdateCurrentTimeTable();
				int nextStationIndex = GetNextStationIndex();
				if (nextStationIndex == -1) return errState;
				if (Table.Stations.Length == 0 || nextStationIndex - 1 < 0) return errState;
				//
				string Hour, Min, Sec;
				Hour = Table.Stations[nextStationIndex - 1].Departure._Hour;
				Min = Table.Stations[nextStationIndex - 1].Departure.Minute;
				Sec = Table.Stations[nextStationIndex - 1].Departure.Second;
				if (Hour == string.Empty || Hour == " " || Hour == "  ") return errState;
				departureTime = Hour + ":" + Min + ":" + Sec;
				return departureTime;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public double GetStopPointDistance()
		{
			double retValue = double.NegativeInfinity;
			try
			{
				if (!timetableReady) UpdateCurrentTimeTable();
				if (Table.Stations.Length == 0) return retValue;
				retValue = currentTrainInControl.StationDistanceToStopPoint;
				if ((GetNextStationDistance() <= 180 || GetPreStationDistance() <= 180) && retValue >= -180) return retValue;
				return retValue;
			}
			catch (Exception){ };
			return retValue;
		}
		static public string GetCurrentRouteName()
		{
			string errState = "N/A";
			try
			{
				return Game.LogRouteName;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public string GetCurrentTrainName()
		{
			string errState = "N/A";
			try
			{
				return Game.LogTrainName;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public string GetCurrentTime()
		{
			string errState = "N/A";
			string _currentTime = String.Empty;
			try
			{
				int Hour, Min, Sec;
				double x;
				x = currentTime;
				x -= 86400.0 * Math.Floor(x / 86400.0);
				Hour = (int)Math.Floor(x / 3600.0);
				x -= 3600.0 * (double)Hour;
				Min = (int)Math.Floor(x / 60.0);
				x -= 60.0 * (double)Min;
				Sec = (int)Math.Floor(x);
				_currentTime = Hour.ToString("00", System.Globalization.CultureInfo.InvariantCulture) + ":" +
					Min.ToString("00", System.Globalization.CultureInfo.InvariantCulture) + ":" 
					+ Sec.ToString("00", System.Globalization.CultureInfo.InvariantCulture);
				return _currentTime;
			}
			catch (Exception)
			{
				return errState;
			}
		}
		static public int GetCurrentTimeHour()
		{
			int retValue = 0;
			try
			{
				double x;
				x = currentTime;
				x -= 86400.0 * Math.Floor(x / 86400.0);
				retValue = (int)Math.Floor(x / 3600.0);
				return retValue;
			}
			catch (Exception)
			{
				return retValue;
			}
		}
		static public int GetCurrentTimeMinutes()
		{
			int retValue = 0;
			try
			{
				double x;
				x = currentTime;
				x -= 86400.0 * Math.Floor(x / 86400.0);
				x -= 3600.0 * (double)GetCurrentTimeHour();
				retValue = (int)Math.Floor(x / 60.0);
				return retValue;
			}
			catch (Exception)
			{
				return retValue;
			}
		}
		static public int GetCurrentTimeSeconds()
		{
			int retValue = 0;
			try
			{
				double x;
				x = currentTime;
				x -= 86400.0 * Math.Floor(x / 86400.0);
				x -= 3600.0 * (double)GetCurrentTimeHour();
				x -= 60.0 * (double)GetCurrentTimeMinutes();
				retValue = (int)Math.Floor(x);
				return retValue;
			}
			catch (Exception)
			{
				return retValue;
			}
		}
		static public void SetLeftDoorOpen()
		{
			try
			{
				TrainManager.OpenTrainDoors(TrainManager.PlayerTrain, true, false);
			}
			catch (Exception) { };
		}
		static public void SetLeftDoorClose()
		{
			try
			{
				TrainManager.CloseTrainDoors(TrainManager.PlayerTrain, true, false);
			}
			catch (Exception) { };
		}
		static public void SetLeftDoorState(int dataValue)
		{
			try
			{
				if (dataValue == 1 && GetLeftDoorState() != 1)
				{
					SetLeftDoorOpen();
				}
				else if (dataValue == 0 && GetLeftDoorState() != 0)
				{
					SetLeftDoorClose();
				}
			}
			catch (Exception) { };
		}
		static public void SetRightDoorOpen()
		{
			try
			{
				TrainManager.OpenTrainDoors(TrainManager.PlayerTrain, false, true);
			}
			catch (Exception) { };
		}
		static public void SetRightDoorClose()
		{
			try
			{
				TrainManager.CloseTrainDoors(TrainManager.PlayerTrain, false, true);
			}
			catch (Exception) { };
		}
		static public void SetRightDoorState(int dataValue)
		{
			try
			{
				if (dataValue == 1 && GetRightDoorState() != 1)
				{
					SetRightDoorOpen();
				}
				else if (dataValue == 0 && GetRightDoorState() != 0)
				{
					SetRightDoorClose();
				}
			}
			catch (Exception) { };;
		}
		static public int GetLeftDoorState()
		{
			int retValue = -1;
			try
			{
				TrainManager.TrainDoorState dataValue;
				dataValue = TrainManager.GetDoorsState(TrainManager.PlayerTrain, true, false);
				if (dataValue == TrainManager.TrainDoorState.AllClosed) retValue = 0;
				else if (dataValue == TrainManager.TrainDoorState.AllOpened) retValue = 1;
				return retValue;
			}
			catch (Exception) { };
			return retValue;
		}
		static public int GetRightDoorState()
		{
			int retValue = -1;
			try
			{
				TrainManager.TrainDoorState dataValue;
				dataValue = TrainManager.GetDoorsState(TrainManager.PlayerTrain, false, true);
				if (dataValue == TrainManager.TrainDoorState.AllClosed) retValue = 0;
				else if (dataValue == TrainManager.TrainDoorState.AllOpened) retValue = 1;
				return retValue;
			}
			catch (Exception) { };
			return retValue;
		}
		static public bool GetSimulatorState()
		{
			bool retValue = false;
			try
			{
				string probeData = GetCurrentStationName();
				retValue = (probeData == "N/A" ? false : true);
			}
			catch (Exception)
			{
				retValue = false;
			}
			return retValue;
		}
		static public double GetCurrentTrainAcceleration()
		{
			double retValue = 0;
			try
			{
				retValue = currentTrainInControl.Cars[currentTrainInControl.DriverCar].Specs.CurrentAccelerationOutput;
			}catch(Exception) { };
			return retValue;
		}
		static public double GetCurrentPowerRate()
		{
			double retValue = 0;
			try
			{
				double currentSpeed = GetCurrentTrainSpeed() / 3.6;
				double currentAcceleration = GetCurrentTrainAcceleration();
				int carsNumber = currentTrainInControl.Cars.Length;
				double carWeight = 0;
				for (int i = 0; i < carsNumber; i++)
				{
					double length = currentTrainInControl.Cars[i].Length;
					double width = currentTrainInControl.Cars[i].Width;
					double height = currentTrainInControl.Cars[i].Height;
					double v = length * width * height;
					if (currentTrainInControl.Cars[i].Specs.IsMotorCar) carWeight += v * 250;
					else carWeight += v * 125;
				}
				double force = carWeight * currentAcceleration;
				retValue = force * currentSpeed;
			}
			catch (Exception) { };
			return retValue;
		}
		static public double GetCurrentCylinderPressure()
		{
			double retValue = 0;
			try
			{
				retValue = ((int)((currentTrainInControl.Cars[currentTrainInControl.DriverCar].CarBrake.brakeCylinder.CurrentPressure / 1000) * 10) / 10.0);
				retValue = retValue > 700 ? 700 : retValue;
				return retValue;
			}
			catch (Exception) { };
			return retValue;

		}
		static public double GetCurrentPipePressure()
		{
			double retValue = 0;
			try
			{
				retValue = ((int)((currentTrainInControl.Cars[currentTrainInControl.DriverCar].CarBrake.brakePipe.CurrentPressure / 1000) * 10) / 10.0);
				retValue = retValue > 700 ? 700 : retValue;
				return retValue;
			}
			catch (Exception) { };
			return retValue;
		}
		static public void PlayWarningSound(bool dataValue)
		{
			if (dataValue && !warningSoundPlaying)
			{
				soundPlayer.PlayLooping();
				warningSoundPlaying = true;
			}
			else if (!dataValue)
			{
				soundPlayer.Stop();
				warningSoundPlaying = false;
			}
		}
		static public void SetDeadmanSystemEnable(bool value)
		{
			deadmanSystemTrig = value;
		}
		static public bool GetDeadmanSystemEnable()
		{
			bool retValue;
			retValue = deadmanSystemTrig;
			return retValue;
		}
		static public void SetResetDeadmanSystem(bool dataValue)
		{
			if (dataValue)
			{
				deadmanCounter = 0;
				deadmanSystemState = 0;
			}
		}
		static public int GetDeadmanSystemState()
		{
			int retValue;
			retValue = deadmanSystemState;
			return retValue;
		}
	}
}
