using Orts.Simulation.RollingStocks;
using ORTS.Common;
using ORTS.Scripting.Api;
using System;
using System.Media;
using System.Reflection;

namespace Orts.RAGLINKProxy
{
    class TrainMethods
    {
        //Proxy variables
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
        //Proxy Core
        static public TrainCar playerLocomotive;
        static public void UpdatePlayerLocomotive(TrainCar currentData)
        {
            try
            {
                playerLocomotive = currentData;
            }
            catch (Exception) { };
        }
        //Train Methods

        static public void SetTrainPowerHandleUp(int levelCount)
        {
            int valuePerLevel = 100 / levelCount;
            try
            {
                int currentPowerLevel = Convert.ToInt32(playerLocomotive.ThrottlePercent);
                if (currentPowerLevel + valuePerLevel <= 100)
                    playerLocomotive.ThrottlePercent = currentPowerLevel + valuePerLevel;
            }
            catch (Exception) { };
        }
        static public void SetTrainPowerHandleDown(int levelCount)
        {
            int valuePerLevel = 100 / levelCount;
            try
            {
                int currentPowerLevel = Convert.ToInt32(playerLocomotive.ThrottlePercent);
                if (currentPowerLevel - valuePerLevel >= 0)
                    playerLocomotive.ThrottlePercent = currentPowerLevel - valuePerLevel;
            }
            catch (Exception) { };
        }
        static public void SetTrainPowerHandle(int dataValue)
        {
            try
            {
                if (dataValue < 0 || dataValue > 100) return;
                playerLocomotive.ThrottlePercent = dataValue;
            }
            catch (Exception) { };
        }
        static public void SetTrainPowerHandle(int levelCount, int dataValue)
        {
            int valuePerLevel = 100 / levelCount;
            try
            {
                if (dataValue * valuePerLevel < 0 || dataValue * valuePerLevel > 100) return;
                playerLocomotive.ThrottlePercent = dataValue * valuePerLevel;
            }
            catch (Exception) { };
        }
        static public void SetTrainBrakeHandle(int dataValue)
        {
            try
            {
                if (dataValue < 0 || dataValue > 100) return;
                ((MSTSLocomotive)playerLocomotive).TrainBrakeChangeTo(false, dataValue);
            }
            catch (Exception) { };
        }
        static public void SetTrainBrakeHandle(int levelCount, int dataValue)
        {
            int valuePerLevel = 100 / levelCount;
            try
            {
                if (dataValue * valuePerLevel < 0 || dataValue * valuePerLevel > 100) return;
                ((MSTSLocomotive)playerLocomotive).TrainBrakeChangeTo(false, dataValue * valuePerLevel);
            }
            catch (Exception) { };
        }
        static public void SetDynamicBrakeHandle(int dataValue)
        {
            try
            {
                if (dataValue < 0 || dataValue > 100) return;
                ((MSTSLocomotive)playerLocomotive).DynamicBrakeChangeTo(false, dataValue);
            }
            catch (Exception) { };
        }
        static public void SetDynamicBrakeHandle(int levelCount, int dataValue)
        {
            int valuePerLevel = 100 / levelCount;
            try
            {
                if (dataValue * valuePerLevel < 0 || dataValue * valuePerLevel > 100) return;
                ((MSTSLocomotive)playerLocomotive).DynamicBrakeChangeTo(false, dataValue * valuePerLevel);
            }
            catch (Exception) { };
        }
        static public void SetEngineBrakeHandle(int dataValue)
        {
            try
            {
                if (dataValue < 0 || dataValue > 100) return;
                ((MSTSLocomotive)playerLocomotive).EngineBrakeChangeTo(false, dataValue);
            }
            catch (Exception) { };
        }
        static public void SetEngineBrakeHandle(int levelCount, int dataValue)
        {
            int valuePerLevel = 100 / levelCount;
            try
            {
                if (dataValue * valuePerLevel < 0 || dataValue * valuePerLevel > 100) return;
                ((MSTSLocomotive)playerLocomotive).EngineBrakeChangeTo(false, dataValue * valuePerLevel);
            }
            catch (Exception) { };
        }
        static public void SetHandBrakeHandle(bool dataValue)
        {
            try
            {
                ((MSTSLocomotive)playerLocomotive).HandBrakePresent = dataValue;
            }
            catch (Exception) { };
        }
        static public void SetTrainReverser(int dataValue)
        {
            try
            {
                switch (dataValue)
                {
                    case -1:
                        {
                            ((MSTSLocomotive)playerLocomotive).SetDirection(Direction.Reverse);
                            break;
                        }
                    case 0:
                        {
                            ((MSTSLocomotive)playerLocomotive).SetDirection(Direction.N);
                            break;
                        }
                    case 1:
                        {
                            ((MSTSLocomotive)playerLocomotive).SetDirection(Direction.Forward);
                            break;
                        }
                }
                return;
            }
            catch (Exception) { };
        }
        //unit: m/s
        static public double GetCurrentTrainSpeed()
        {
            double retValue = 0;
            try
            {
                retValue = playerLocomotive.SpeedMpS;
            }
            catch (Exception) { };
            return retValue;
        }
        static public int GetCurrentReverserState()
        {
            int retValue = 0;
            try
            {
                switch (playerLocomotive.Direction)
                {
                    case Direction.Reverse:
                        {
                            retValue = -1;
                            break;
                        }
                    case Direction.N:
                        {
                            retValue = 0;
                            break;
                        }
                    case Direction.Forward:
                        {
                            retValue = 1;
                            break;
                        }
                }
            }
            catch (Exception) { };
            return retValue;
        }
        static public int GetCurrentPowerHandle(int levelCount)
        {
            int retValue = 0;
            try
            {
                retValue = Convert.ToInt32(playerLocomotive.ThrottlePercent / levelCount);
            }
            catch (Exception) { };
            return retValue;
        }
        static public int GetCurrentPowerHandle()
        {
            int retValue = 0;
            try
            {
                retValue = Convert.ToInt32(playerLocomotive.ThrottlePercent);
            }
            catch (Exception) { };
            return retValue;
        }
        static public int GetCurrentTrainBrakeHandle(int levelCount)
        {
            int retValue = 0;
            try
            {
                retValue = Convert.ToInt32(((MSTSLocomotive)playerLocomotive).TrainBrakeController.CurrentValue / levelCount);
            }
            catch (Exception) { };
            return retValue;
        }
        static public int GetCurrentTrainBrakeHandle()
        {
            int retValue = 0;
            try
            {
                retValue = Convert.ToInt32(((MSTSLocomotive)playerLocomotive).TrainBrakeController.CurrentValue);
            }
            catch (Exception) { };
            return retValue;
        }
        static public int GetCurrentDynamicBrakeHandle(int levelCount)
        {
            int retValue = 0;
            try
            {
                retValue = Convert.ToInt32(((MSTSLocomotive)playerLocomotive).DynamicBrakeController.CurrentValue / levelCount);
            }
            catch (Exception) { };
            return retValue;
        }
        static public int GetCurrentDynamicBrakeHandle()
        {
            int retValue = 0;
            try
            {
                retValue = Convert.ToInt32(((MSTSLocomotive)playerLocomotive).DynamicBrakeController.CurrentValue);
            }
            catch (Exception) { };
            return retValue;
        }
        static public int GetCurrentEngineBrakeHandle(int levelCount)
        {
            int retValue = 0;
            try
            {
                retValue = Convert.ToInt32(((MSTSLocomotive)playerLocomotive).EngineBrakeController.CurrentValue / levelCount);
            }
            catch (Exception) { };
            return retValue;
        }
        static public int GetCurrentEngineBrakeHandle()
        {
            int retValue = 0;
            try
            {
                retValue = Convert.ToInt32(((MSTSLocomotive)playerLocomotive).EngineBrakeController.CurrentValue);
            }
            catch (Exception) { };
            return retValue;
        }
        static public bool GetCurrentHandBrakeHandle()
        {
            bool retValue = false;
            try
            {
                retValue = Convert.ToBoolean(((MSTSLocomotive)playerLocomotive).HandBrakePresent);
            }
            catch (Exception) { };
            return retValue;
        }
        static public double GetCurrentSectionSpeedLimit()
        {
            double retValue = 30 / 3.6;
            try
            {
                retValue = playerLocomotive.Train.GetTrainInfo().allowedSpeedMpS;
                if (retValue > 500 / 3.6 || retValue < 0) retValue = 30;
            }
            catch (Exception) { };
            return retValue;
        }
        static public int GetNextSectionSignalInfo()
        {
            int retValue = 0;
            try
            {
                var trainParameters = playerLocomotive.Train;
                retValue = Convert.ToInt32(trainParameters.GetNextSignalAspect(trainParameters.GetTrainInfo().direction));
            }
            catch (Exception) { };
            return retValue;
        }
        static public double GetCurrentRouteGradient()
        {
            double retValue = 0;
            try
            {
                var trainParameters = playerLocomotive.Train;
                retValue = -1 * trainParameters.GetTrainInfo().currentElevationPercent;
            }
            catch (Exception) { };
            return retValue;
        }
        static public double GetNextSectionSignalDistance()
        {
            double retValue = 0;
            try
            {
                retValue = Convert.ToDouble(playerLocomotive.Train.DistanceToSignal);
            }
            catch (Exception) { };
            return retValue;
        }
        static public void SetHornState(bool dataValue)
        {
            try
            {
                ((MSTSLocomotive)playerLocomotive).ManualHorn = dataValue;
                if (dataValue)
                {
                    ((MSTSLocomotive)playerLocomotive).AlerterReset(TCSEvent.HornActivated);
                    ((MSTSLocomotive)playerLocomotive).Simulator.HazzardManager.Horn();
                }
            }
            catch (Exception) { };
        }
        static public bool GetCurrentHornState()
        {
            bool retValue = false;
            try
            {
                retValue = ((MSTSLocomotive)playerLocomotive).ManualHorn;
            }
            catch (Exception) { };
            return retValue;
        }
        static public void SetMasterKeyState(bool dataValue)
        {
            try
            {
                masterKeyEnable = dataValue;
                if (!masterKeyEnable)
                {
                    SetTrainBrakeHandle(0);
                    SetTrainPowerHandle(0);
                    SetTrainReverser(0);
                }
            }
            catch (Exception) { };
        }
        static public bool GetCurrentMasterKeyState()
        {
            bool retValue = false;
            retValue = masterKeyEnable;
            return retValue;
        }
        static public void SetEmergencyState(bool dataValue)
        {

        }
    }
}
