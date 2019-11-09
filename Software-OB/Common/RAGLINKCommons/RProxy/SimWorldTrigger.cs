using System;
using System.Collections.Generic;
using System.Threading;

namespace RAGLINKCommons.RProxy
{
    public class SimWorldTrigger
    {
        public class TriggerClient
        {
            public bool isPaused;
            public int clientID;
            public int timerCounter;
            public int timerTimes;
            double timesCount;
            public delegate void TriggerHandler(double currentTimesCount, double currentTrackDistance);
            public event TriggerHandler TriggerEvent;

            public TriggerClient()
            {
                isPaused = true;
                timerCounter = 0;
                timerTimes = 1000 / mainTimerTick;
                timesCount = 0;
                clientID = 0;
            }

            public TriggerClient(int timerInv, TriggerHandler triggerHandler)
            {
                isPaused = true;
                timerCounter = 0;
                timerTimes = timerInv / mainTimerTick;
                timesCount = 0;
                clientID = 0;
                TriggerEvent = new TriggerHandler(triggerHandler);
            }

            public void DoEvent(double currentTrackDistance)
            {
                if (isPaused)
                {
                    return;
                }

                timerCounter++;
                timesCount += mainTimerTick;
                if (timerCounter >= timerTimes)
                {
                    timerCounter = 0;
                    try
                    {
                        TriggerEvent(timesCount, currentTrackDistance);
                    }
                    catch (Exception) { };
                }
            }
        };

        private static readonly int mainTimerTick = 25;
        static public List<TriggerClient> triggerClients = new List<TriggerClient>();

        static public void ResetTrigger()
        {
            MainTimerFunctions.MainTimerEnable = false;
            triggerClients.Clear();
        }

        static public int AddTriggerClient(TriggerClient triggerClient)
        {
            int retValue = -1;
            int currentClientIndex = triggerClients.IndexOf(triggerClient);
            if (currentClientIndex == -1)
            {
                triggerClient.clientID = triggerClients.Count + 1;
                triggerClients.Add(triggerClient);
                retValue = triggerClient.clientID;
            }
            else
            {
                retValue = currentClientIndex;
                triggerClients[currentClientIndex].isPaused = true;
            }
            return retValue;
        }

        static public void DeleteTriggerClienr(TriggerClient triggerClient)
        {
            int currentClientIndex = triggerClients.IndexOf(triggerClient);
            try
            {
                if (currentClientIndex == -1)
                {
                    return;
                }

                triggerClients.RemoveAt(currentClientIndex);
            }
            catch (Exception) { };
        }

        static public void SetClientState(TriggerClient triggerClient, bool isPaused)
        {
            int currentClientIndex = triggerClients.IndexOf(triggerClient);
            try
            {
                if (currentClientIndex == -1)
                {
                    return;
                }

                triggerClients[currentClientIndex].isPaused = isPaused;
            }
            catch (Exception) { };
        }

        static public void InitTriggerTimer()
        {
            MainTimerFunctions.AttachMainTimerInterrupt();
            MainTimerFunctions.MainTimerEnable = true;
        }

        public static class MainTimerFunctions
        {
            private static int inTimerMain = 0;
            private static System.Threading.Timer MainTimer;
            public static bool MainTimerEnable = false;
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
                    if (MainTimerEnable)
                    {
                        foreach (TriggerClient triggerClient in triggerClients)
                        {
                            triggerClient.DoEvent((double)RPlatform.DataManager.GetTrainData((int)RPlatform.DataManager.TrainDataMap.TRACK_POSITION));
                        }
                    }
                    Interlocked.Exchange(ref inTimerMain, 0);
                }
            }
        };

    }
}
