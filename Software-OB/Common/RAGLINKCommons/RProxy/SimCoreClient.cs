using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace RAGLINKCommons.RProxy
{
    public class SimCoreClient
    {
        public delegate void TriggerAttachHandler();
        static public event TriggerAttachHandler TriggerAttachEvent;
        public delegate void EnableTriggerClientHandler();
        static public event EnableTriggerClientHandler EnableTriggerClientEvent;
        public delegate bool GetSimulatorStateHandler();
        static public event GetSimulatorStateHandler GetSimulatorStateEvent;
        public delegate bool RestartSimulatorStateHandler(string excuteFileName, string Args);
        static public event RestartSimulatorStateHandler RestartSimulatorStateEvent;
        public delegate void PauseSimulatorStateHandler(bool dataValue);
        static public event PauseSimulatorStateHandler PauseSimulatorStateEvent;

        static public DateTime simulatorLaunchTime = DateTime.MinValue;
        static public bool isLaunchTimeInit = false;

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        static public void AttachToTrigger()
        {
            try
            {
                TriggerAttachEvent();
            }
            catch (Exception) { };
        }

        static public void EnableTriggerClient()
        {
            try
            {
                EnableTriggerClientEvent();
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
        static public bool RestartSimulator(string excuteFileName, string Args)
        {
            bool retValue = false;
            retValue = RestartSimulatorStateEvent(excuteFileName, Args);
            return retValue;
        }

        static public void PauseSimulator(bool dataValue)
        {
            PauseSimulatorStateEvent(dataValue);
        }

        static public bool IsSimulatorInRunning(string windowTitle)
        {
            bool retValue = false;
            try
            {
                IntPtr ParenthWnd = new IntPtr(0);
                ParenthWnd = FindWindow(null, windowTitle);
                retValue = ParenthWnd != IntPtr.Zero;
            }
            catch (Exception) { };
            return retValue;
        }

        static public Point GetSimulatorWindowPosition(string windowTitle)
        {
            Point windowPosition = new Point(0, 0);
            IntPtr ParenthWnd = new IntPtr(0);
            ParenthWnd = FindWindow(null, windowTitle);
            if (ParenthWnd != IntPtr.Zero)
            {
                RECT fx = new RECT();
                GetWindowRect(ParenthWnd, ref fx);
                windowPosition.X = fx.Left;
                windowPosition.Y = fx.Top;
            }
            return windowPosition;
        }

        static public void SetupSimulatorLaunchTime()
        {
            simulatorLaunchTime = DateTime.Now;
            return;
        }

        static public string GetSimulatorLaunchTimeString(char filterChar)
        {
            string retValue = string.Empty;
            retValue = simulatorLaunchTime.Year.ToString("0000") +
                filterChar + simulatorLaunchTime.Month.ToString("00") +
                filterChar + simulatorLaunchTime.Day.ToString("00") +
                filterChar + simulatorLaunchTime.Hour.ToString("00") +
                filterChar + simulatorLaunchTime.Minute.ToString("00") +
                filterChar + simulatorLaunchTime.Second.ToString("00");
            return retValue;
        }
    }
}
