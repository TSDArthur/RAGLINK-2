using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace RAGLINKCommons.RAGLINKProxy
{
    public partial class formController : Form
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        public formController()
        {
            InitializeComponent();
        }
        private void FormController_Load(object sender, EventArgs e)
        {
            //Load theme
            themeApplier.SetStyle(menuStripMain, WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender.VsVersion.Vs2015, mainTheme);
            if (!UserInterfaceSwap.showFormController)
            {
                this.Left = 1;
                this.FormBorderStyle = FormBorderStyle.None;
                this.ShowInTaskbar = false;
                this.Opacity = 0;
                this.TopMost = false;
                this.Hide();
            }
            else
            {
                this.Left = 1;
                this.TopMost = true;
            }
            labelPlanName.Text = "已加载的行车计划：" + RAGLINKPlatform.ProjectsManager.projectInfo.projectName;
            try
            {
                UserInterfaceSwap.formControllerLeft = this.Left;
                UserInterfaceSwap.formControllerTop = this.Top;
                UserInterfaceSwap.formControllerHeight = this.Height;
                UserInterfaceSwap.formControllerWidth = this.Width;
                textBoxDebugStream.Enabled = false;
                buttonSendDebugStream.Enabled = false;
                UpdateButtonEnableState();
                TrainMethodsClient.AttachMainTimerInterrupt();
                RAGLINKPlatform.ControlObjects.MainTimerFunctions.AttachMainTimerInterrupt();
                RAGLINKPlatform.ControlObjects.MainTimerFunctions.MainTimerEnable = true;
                timerEvents.Enabled = true;
                timerStreamTextUpdater.Enabled = true;
            }
            catch (Exception) { };
        }
        private void ButtonMasterKeyOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.MASTER_KEY;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonMasterKeyOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.MASTER_KEY;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonPantoOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.PANTO;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonPantoOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.PANTO;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonTractionOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.TRACTION;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonTractionOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.TRACTION;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonReverserF_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.REVERSER;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonReverserIDLE_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.REVERSER;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonReverserB_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.REVERSER;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonATCOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.ATC;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonATCOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.ATC;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonLeftDoorOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.DOOR;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonLeftDoorOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.DOOR;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonRightDoorOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.DOOR;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonRightDoorOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.DOOR;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonEmergencyOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.EMERGENCY;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonEmergencyOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.EMERGENCY;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonAirconOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.AIR_CONDITION;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonAirconOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.AIR_CONDITION;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonSandOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.SAND;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonSandOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.SAND;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonHornOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.HORN;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonHornOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.HORN;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonHeadlightOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.LIGHT;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonHeadlightOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.LIGHT;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonDeadmanOn_Click(object sender, EventArgs e)
        {
            RAGLINKPlatform.DataManager.SetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.DEADMAN_ENABLE_TRIG, 1);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonDeadmanPress_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.DEADMAN;
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
            Thread.Sleep(200);
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonDeadmanOff_Click(object sender, EventArgs e)
        {
            RAGLINKPlatform.DataManager.SetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.DEADMAN_ENABLE_TRIG, 0);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }

        private int findSimualtorWindowTick = 0;
        private void TimerEvents_Tick(object sender, EventArgs e)
        {
            timerEvents.Enabled = false;
            try
            {
                if (boardConnection)
                {
                    buttonBoardConnection.Text = "主板强制断开";
                }
                else
                {
                    buttonBoardConnection.Text = "主板连接保持";
                }
                try
                {
                    if (!RAGLINKPlatform.Communication.serialList[0].IsOpen)
                    {
                        checkBoxStreamDebugEnable.Enabled = false;
                        checkBoxStreamDebugEnable.Checked = false;
                        textBoxDebugStream.Text = string.Empty;
                        textBoxDebugStream.Enabled = false;
                        buttonSendDebugStream.Enabled = false;
                    }
                    else
                    {
                        checkBoxStreamDebugEnable.Enabled = true;
                    }
                }
                catch (Exception) { };
                if (!TrainMethodsClient.GetSimulatorState())
                {
                    buttonDataMonitor.Enabled = false;
                    menItemDataMonitor.Enabled = false;
                    toolStripLabelMain.Text = "模拟器载入中...";
                    timerEvents.Enabled = true;
                    return;
                }
                buttonDataMonitor.Enabled = true;
                menItemDataMonitor.Enabled = true;
                toolStripLabelMain.Text = "就绪.";
                if (!applyDataInLoading)
                {
                    RAGLINKPlatform.ControlObjects.ControlObjectDoEvents();
                    RAGLINKPlatform.DataManager.ApplyTrainData();
                    applyDataInLoading = true;
                }
                UpdateLabels();
                //Try to re-connect if connection failed
                if (boardConnection && !RAGLINKPlatform.Communication.serialList[0].IsOpen)
                {
                    RAGLINKPlatform.Communication.SerialConnect(0);
                    if (RAGLINKPlatform.Communication.serialList[0].IsOpen) RAGLINKPlatform.Communication.communicationStateList[0] = RAGLINKPlatform.Communication.CommunicationState.STATE1;
                }
                else if (!boardConnection)
                {
                    try
                    {
                        RAGLINKPlatform.Communication.serialList[0].Dispose();
                        RAGLINKPlatform.Communication.communicationStateList[0] = RAGLINKPlatform.Communication.CommunicationState.STATE0;
                        RAGLINKPlatform.Communication.dealWithBoardSerialStream = false;
                    }
                    catch (Exception) { };
                }
                for (int i = 1; i < RAGLINKPlatform.Communication.serialList.Count; i++) RAGLINKPlatform.Communication.SerialConnect(i);
            }
            catch (Exception) { };
            //Main-form Monitor Inv:3s
            findSimualtorWindowTick++;
            if (findSimualtorWindowTick > 20)
            {
                findSimualtorWindowTick = 0;
                IntPtr ParenthWnd = new IntPtr(0);
                ParenthWnd = FindWindow(null, "RAGLINK+");
                if (ParenthWnd == IntPtr.Zero) this.Close();
            }
            timerEvents.Enabled = true;
        }
        private void ButtonHandleUp_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.POWER) != 0)
            {
                handleValue = (int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.POWER);
            }
            else
            {
                handleValue = (-1) * (int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.BRAKE);
            }
            if (handleValue >= 3) return;
            for (int i = 0; i < RAGLINKPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
                RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            switch (handleValue)
            {
                case 0:
                    {
                        RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
                        handleValue++;
                        break;
                    }
                case 1:
                    {
                        RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 1);
                        handleValue++;
                        break;
                    }
                case 2:
                    {
                        RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[2], 1);
                        handleValue++;
                        break;
                    }
                case -2:
                    {
                        handleValue += 2;
                        break;
                    }
                case -4:
                    {
                        RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[3], 1);
                        handleValue += 2;
                        break;
                    }
                case -6:
                    {
                        RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[4], 1);
                        handleValue += 2;
                        break;
                    }
            }
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void ButtonHandleDown_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.POWER) != 0)
            {
                handleValue = (int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.POWER);
            }
            else
            {
                handleValue = (-1) * (int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.BRAKE);
            }
            if (handleValue <= -6) return;
            for (int i = 0; i < RAGLINKPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
                RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            switch (handleValue)
            {
                case 0:
                    {
                        RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[3], 1);
                        handleValue -= 2;
                        break;
                    }
                case 1:
                    {
                        handleValue--;
                        break;
                    }
                case 2:
                    {
                        RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
                        handleValue--;
                        break;
                    }
                case 3:
                    {
                        RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 1);
                        handleValue--;
                        break;
                    }
                case -2:
                    {
                        RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[4], 1);
                        handleValue -= 2;
                        break;
                    }
                case -4:
                    {
                        RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[5], 1);
                        handleValue -= 2;
                        break;
                    }
            }
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void MenItemHandleP3_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RAGLINKPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
                RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[2], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void MenItemHandleP2_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RAGLINKPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
                RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void MenItemHandleP1_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RAGLINKPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
                RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void MenItemHandleP0_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RAGLINKPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
                RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void MenItemHandleB1_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RAGLINKPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
                RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[3], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void MenItemHandleB2_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RAGLINKPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
                RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[4], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void MenItemHandleB3_Click(object sender, EventArgs e)
        {
            int objectID = (int)RAGLINKPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RAGLINKPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
                RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            RAGLINKPlatform.DevicesManager.SetDeviceValue((int)RAGLINKPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[5], 1);
            RAGLINKPlatform.ControlObjects.ControlObjectDoEvents((RAGLINKPlatform.ControlObjects.ControlObjectsList)objectID);
            RAGLINKPlatform.DataManager.ApplyTrainData();
        }
        private void TimerStreamTextUpdater_Tick(object sender, EventArgs e)
        {
            try
            {
                int recievedIndex = RAGLINKPlatform.Communication.StreamRecord.boardSerialStreamRecord.Count - 1;
                int sentIndex = RAGLINKPlatform.Communication.StreamRecord.boardSentStreamRecord.Count - 1;
                if (recievedIndex >= 0)
                {
                    if (textBoxRecievedStream.Text != RAGLINKPlatform.Communication.StreamRecord.boardSerialStreamRecord[recievedIndex])
                        textBoxRecievedStream.Text = RAGLINKPlatform.Communication.StreamRecord.boardSerialStreamRecord[recievedIndex];
                }
                if (sentIndex >= 0)
                {
                    if (textBoxSentStream.Text != RAGLINKPlatform.Communication.StreamRecord.boardSentStreamRecord[sentIndex])
                        textBoxSentStream.Text = RAGLINKPlatform.Communication.StreamRecord.boardSentStreamRecord[sentIndex];
                }
            }
            catch (Exception) { };
        }

        private void ButtonBoardConnection_Click(object sender, EventArgs e)
        {
            if (boardConnection)
            {
                try
                {
                    boardConnection = false;
                    RAGLINKPlatform.Communication.communicationStateList[0] = RAGLINKPlatform.Communication.CommunicationState.STATE0;
                    buttonBoardConnection.Text = "主板连接保持";
                }
                catch (Exception) { };
            }
            else
            {
                try
                {
                    boardConnection = true;
                    buttonBoardConnection.Text = "主板强制断开";
                }
                catch (Exception)
                {
                    MessageBox.Show("连接主控板失败!", "连接主控板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void ButtonStreamRecord_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread((System.Threading.ThreadStart)delegate
            {
                Application.Run(new formStreamRecord());
            }).Start();
        }

        private void ButtonDataMonitor_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread((System.Threading.ThreadStart)delegate
            {
                Application.Run(new formDataMonitor());
            }).Start();
        }

        private void FormController_Move(object sender, EventArgs e)
        {
            UserInterfaceSwap.formControllerLeft = this.Left;
            UserInterfaceSwap.formControllerTop = this.Top;
            UserInterfaceSwap.formControllerHeight = this.Height;
            UserInterfaceSwap.formControllerWidth = this.Width;
        }

        private void CheckBoxStreamDebugEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxStreamDebugEnable.Checked)
            {
                textBoxDebugStream.Text = string.Empty;
                textBoxDebugStream.Enabled = true;
                buttonSendDebugStream.Enabled = true;
            }
            else
            {
                textBoxDebugStream.Text = string.Empty;
                textBoxDebugStream.Enabled = false;
                buttonSendDebugStream.Enabled = false;
            }
        }

        private void ButtonSendDebugStream_Click(object sender, EventArgs e)
        {
            string sendStream = string.Empty;
            try
            {
                for (int i = 0; i < textBoxDebugStream.Text.Length; i += 2)
                {
                    int tempValue = Convert.ToInt32(textBoxDebugStream.Text.Substring(i, 2), 16);
                    sendStream += (char)tempValue;
                }
                if (sendStream.Length > 0) RAGLINKPlatform.Communication.SendDataViaSerial(0, sendStream);
            }
            catch (Exception) { };

        }

        private void TextBoxDebugStream_TextChanged(object sender, EventArgs e)
        {
            if (textBoxDebugStream.Text.Length == 0 || textBoxDebugStream.Text.Length % 2 != 0)
            {
                buttonSendDebugStream.Enabled = false;
            }
            else
            {
                buttonSendDebugStream.Enabled = true;
            }
        }

        private void FormController_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void MenItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RAGLINK Simulator 调试控制面板。\n©2019 TSDWorks\nRAGLINK Simulator 调试控制面板是平台 RAGLINK Studio 的组成部分。", "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenItemRestart_Click(object sender, EventArgs e)
        {
            TrainMethodsClient.RestartSimulator("-ui");
        }
    }
}
