using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAGLINKCommons.RAGLINKProxy
{
    public class TrainMethodsClient
    {
        public delegate void TimerAttachHandler();
        static public event TimerAttachHandler TimerAttachEvent;
        public delegate bool GetSimulatorStateHandler();
        static public event GetSimulatorStateHandler GetSimulatorStateEvent;
        public delegate bool RestartSimulatorStateHandler(string Args);
        static public event RestartSimulatorStateHandler RestartSimulatorStateEvent;
        static public void AttachMainTimerInterrupt()
        {
            try
            {
                TimerAttachEvent();
            }
            catch (Exception) { };
        }
        static public bool GetSimulatorState()
        {
            bool retValue = false;
            try
            {
                retValue = GetSimulatorStateEvent();
            }
            catch (Exception) { };
            return retValue;
        }
        static public bool RestartSimulator(string Args)
        {
            bool retValue = false;
            retValue = RestartSimulatorStateEvent(Args);
            return retValue;
        }
    }
}
