using System;
using System.Threading;
using System.Windows.Forms;

namespace RAGLINKCommons.RProxy
{
    public partial class FormController : Form
    {
        public FormController()
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
            labelPlanName.Text = "已加载的行车计划：" + RPlatform.ProjectsManager.projectInfo.projectName;
            try
            {
                UserInterfaceSwap.formControllerLeft = this.Left;
                UserInterfaceSwap.formControllerTop = this.Top;
                UserInterfaceSwap.formControllerHeight = this.Height;
                UserInterfaceSwap.formControllerWidth = this.Width;
                textBoxDebugStream.Enabled = false;
                buttonSendDebugStream.Enabled = false;
                UpdateButtonEnableState();
                RProxy.SimWorldTrigger.InitTriggerTimer();
                RStatistics.SAGATManager.AttachToTrigger();
                RPlatform.ControlObjects.AttachToTrigger();
                SimCoreClient.AttachToTrigger();
                SimCoreClient.EnableTriggerClient();
                timerEvents.Enabled = true;
                timerStreamTextUpdater.Enabled = true;
            }
            catch (Exception) { };
        }

        private void ButtonMasterKeyOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.MASTER_KEY;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonMasterKeyOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.MASTER_KEY;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonPantoOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.PANTO;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonPantoOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.PANTO;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonTractionOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.TRACTION;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonTractionOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.TRACTION;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonReverserF_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.REVERSER;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonReverserIDLE_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.REVERSER;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonReverserB_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.REVERSER;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonATCOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.ATC;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonATCOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.ATC;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonLeftDoorOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.DOOR;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonLeftDoorOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.DOOR;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonRightDoorOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.DOOR;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonRightDoorOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.DOOR;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonEmergencyOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.EMERGENCY;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonEmergencyOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.EMERGENCY;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonAirconOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.AIR_CONDITION;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonAirconOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.AIR_CONDITION;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonSandOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.SAND;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonSandOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.SAND;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonHornOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.HORN;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonHornOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.HORN;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonHeadlightOn_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.LIGHT;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonHeadlightOff_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.LIGHT;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonDeadmanOn_Click(object sender, EventArgs e)
        {
            RPlatform.DataManager.SetTrainData((int)RPlatform.DataManager.TrainDataMap.DEADMAN_ENABLE_TRIG, 1);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonDeadmanPress_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.DEADMAN;
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
            Thread.Sleep(200);
            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 0);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonDeadmanOff_Click(object sender, EventArgs e)
        {
            RPlatform.DataManager.SetTrainData((int)RPlatform.DataManager.TrainDataMap.DEADMAN_ENABLE_TRIG, 0);
            RPlatform.DataManager.ApplyTrainData();
        }

        static private int findSimualtorWindowTick = 0;
        static private bool simulatorWindowHasOpened = false;
        static private bool applyDataInLoading = false;

        private void TimerEvents_Tick(object sender, EventArgs e)
        {
            timerEvents.Enabled = false;
            try
            {
                labelServerAddress.Text = "嵌入式数据服务器地址：" + RPlatform.CommunicationNetwork.dataServerAddress + " 已连接客户端数量：" +
                    RPlatform.CommunicationNetwork.GetClientSocketCount().ToString();

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
                    if (!RPlatform.CommunicationSerial.serialList[0].IsOpen)
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

                //Main-form Monitor 240ms
                try
                {
                    findSimualtorWindowTick++;
                    if (findSimualtorWindowTick > 2)
                    {
                        findSimualtorWindowTick = 0;
                        bool simulatorWindowState = SimCoreClient.IsSimulatorInRunning("RAGLINK+");
                        if (!simulatorWindowState && simulatorWindowHasOpened)
                        {
                            this.Close();
                        }
                        else if (simulatorWindowState)
                        {
                            simulatorWindowHasOpened = true;
                        }
                    }
                }
                catch (Exception) { };

                timerEvents.Enabled = true;

                if (!SimCoreClient.GetSimulatorState())
                {
                    buttonDataMonitor.Enabled = false;
                    menItemDataMonitor.Enabled = false;
                    toolStripLabelMain.Text = "模拟器载入中...";
                    labelSAGATState.Text = "内部SAGAT系统状态：正在载入中...\n若加载外部SAGAT，此处将显示内部SAGAT系统未配置。";
                    timerEvents.Enabled = true;
                    return;
                }

                buttonDataMonitor.Enabled = true;
                menItemDataMonitor.Enabled = true;
                toolStripLabelMain.Text = "就绪。";

                if (RPlatform.ProjectsManager.projectInfo.QAData.questionAnswerEnable)
                {
                    if (RStatistics.SAGATManager.GetCurrentTimerMode() == RStatistics.SAGATManager.TimerMode.WAIT_PAUSE)
                    {
                        labelSAGATState.Text = "内部SAGAT系统状态：共 " + RStatistics.SAGATManager.GetQACount().ToString() + " 个问询，已完成 " +
                            RStatistics.SAGATManager.GetAnsweredCount().ToString() + " 个问询，\n当前轨道位置：" + RStatistics.SAGATManager.currentTrackDistanceFromStart.ToString("f1") +
                            "m，距下一次问询轨道距离：" + ((RStatistics.SAGATManager.GetAnsweredCount() < RStatistics.SAGATManager.GetQACount()) ? ((RStatistics.SAGATManager.GetNextQuestion().pauseDistance - RStatistics.SAGATManager.currentTrackDistanceFromStart).ToString("f1") +
                            "m。") : "无。");
                    }
                    else if (RStatistics.SAGATManager.GetCurrentTimerMode() == RStatistics.SAGATManager.TimerMode.WAIT_ANSWER)
                    {
                        labelSAGATState.Text = "内部SAGAT系统状态：共 " + RStatistics.SAGATManager.GetQACount().ToString() + " 个问询，已完成 " +
                            RStatistics.SAGATManager.GetAnsweredCount().ToString() + " 个问询，\n当前进行的问询项：" + RStatistics.SAGATManager.GetCurrentQuestionItem().questionTitle +
                            "，问询项倒计时：" + (RStatistics.SAGATManager.GetCurrentQuestionItem().questionTimer - RStatistics.SAGATManager.GetCurrentWaitAnswerTime()).ToString() +
                            "s。";
                    }
                }
                else
                {
                    labelSAGATState.Text = "内部SAGAT系统状态：未启用。\n未配置内部SAGAT或已加载外部SAGAT系统。";
                }

                if (applyDataInLoading == false)
                {
                    applyDataInLoading = true;
                    RPlatform.ControlObjects.ControlObjectDoEvents();
                    RPlatform.DataManager.ApplyTrainData();
                    RStatistics.SAGATManager.EnableTriggerClient();
                    RPlatform.ControlObjects.EnableTriggerClient();
                    SimCoreClient.SetupSimulatorLaunchTime();
                }

                UpdateLabels();

                //Try to re-connect if connection failed
                if (boardConnection && !RPlatform.CommunicationSerial.serialList[0].IsOpen)
                {
                    RPlatform.CommunicationSerial.SerialConnect(0);
                    if (RPlatform.CommunicationSerial.serialList[0].IsOpen)
                    {
                        RPlatform.CommunicationSerial.communicationStateList[0] = RPlatform.CommunicationSerial.CommunicationState.STATE1;
                    }
                }
                else if (!boardConnection)
                {
                    try
                    {
                        RPlatform.CommunicationSerial.serialList[0].Dispose();
                        RPlatform.CommunicationSerial.communicationStateList[0] = RPlatform.CommunicationSerial.CommunicationState.STATE0;
                        RPlatform.CommunicationSerial.dealWithBoardSerialStream = false;
                    }
                    catch (Exception) { };
                }
                for (int i = 1; i < RPlatform.CommunicationSerial.serialList.Count; i++)
                {
                    RPlatform.CommunicationSerial.SerialConnect(i);
                }
            }
            catch (Exception) { };
        }

        private void ButtonHandleUp_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            if ((int)RPlatform.DataManager.GetTrainData((int)RPlatform.DataManager.TrainDataMap.POWER) != 0)
            {
                handleValue = (int)RPlatform.DataManager.GetTrainData((int)RPlatform.DataManager.TrainDataMap.POWER);
            }
            else
            {
                handleValue = (-1) * (int)RPlatform.DataManager.GetTrainData((int)RPlatform.DataManager.TrainDataMap.BRAKE);
            }
            if (handleValue >= 3)
            {
                return;
            }

            for (int i = 0; i < RPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
            {
                RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            }

            switch (handleValue)
            {
                case 0:
                    {
                        RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
                        break;
                    }
                case 1:
                    {
                        RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 1);
                        break;
                    }
                case 2:
                    {
                        RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[2], 1);
                        break;
                    }
                case -2:
                    {
                        break;
                    }
                case -4:
                    {
                        RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[3], 1);
                        break;
                    }
                case -6:
                    {
                        RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[4], 1);
                        break;
                    }
            }
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void ButtonHandleDown_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            if ((int)RPlatform.DataManager.GetTrainData((int)RPlatform.DataManager.TrainDataMap.POWER) != 0)
            {
                handleValue = (int)RPlatform.DataManager.GetTrainData((int)RPlatform.DataManager.TrainDataMap.POWER);
            }
            else
            {
                handleValue = (-1) * (int)RPlatform.DataManager.GetTrainData((int)RPlatform.DataManager.TrainDataMap.BRAKE);
            }
            if (handleValue <= -6)
            {
                return;
            }

            for (int i = 0; i < RPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
            {
                RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            }

            switch (handleValue)
            {
                case 0:
                    {
                        RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[3], 1);
                        break;
                    }
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
                        break;
                    }
                case 3:
                    {
                        RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 1);
                        break;
                    }
                case -2:
                    {
                        RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[4], 1);
                        break;
                    }
                case -4:
                    {
                        RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[5], 1);
                        break;
                    }
            }
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void MenItemHandleP3_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
            {
                RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            }

            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[2], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void MenItemHandleP2_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
            {
                RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            }

            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[1], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void MenItemHandleP1_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
            {
                RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            }

            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[0], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void MenItemHandleP0_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
            {
                RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            }

            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void MenItemHandleB1_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
            {
                RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            }

            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[3], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void MenItemHandleB2_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
            {
                RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            }

            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[4], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void MenItemHandleB3_Click(object sender, EventArgs e)
        {
            int objectID = (int)RPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE;
            for (int i = 0; i < RPlatform.ControlObjects.controlObjectsInfo[objectID].objectIOCount; i++)
            {
                RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[i], 0);
            }

            RPlatform.DevicesManager.SetDeviceValue((int)RPlatform.ControlObjects.controlObjectArrange[objectID].objectIO[5], 1);
            RPlatform.ControlObjects.ControlObjectDoEvents((RPlatform.ControlObjects.ControlObjectsList)objectID);
            RPlatform.DataManager.ApplyTrainData();
        }

        private void TimerStreamTextUpdater_Tick(object sender, EventArgs e)
        {
            try
            {
                int recievedIndex = RPlatform.CommunicationSerial.StreamRecord.boardSerialStreamRecord.Count - 1;
                int sentIndex = RPlatform.CommunicationSerial.StreamRecord.boardSentStreamRecord.Count - 1;
                if (recievedIndex >= 0)
                {
                    if (textBoxRecievedStream.Text != RPlatform.CommunicationSerial.StreamRecord.boardSerialStreamRecord[recievedIndex])
                    {
                        textBoxRecievedStream.Text = RPlatform.CommunicationSerial.StreamRecord.boardSerialStreamRecord[recievedIndex];
                    }
                }
                if (sentIndex >= 0)
                {
                    if (textBoxSentStream.Text != RPlatform.CommunicationSerial.StreamRecord.boardSentStreamRecord[sentIndex])
                    {
                        textBoxSentStream.Text = RPlatform.CommunicationSerial.StreamRecord.boardSentStreamRecord[sentIndex];
                    }
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
                    RPlatform.CommunicationSerial.communicationStateList[0] = RPlatform.CommunicationSerial.CommunicationState.STATE0;
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
                Application.Run(new FormStreamRecord());
            }).Start();
        }

        private void ButtonDataMonitor_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread((System.Threading.ThreadStart)delegate
            {
                Application.Run(new FormDataMonitor());
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
                if (sendStream.Length > 0)
                {
                    RPlatform.CommunicationSerial.SendDataViaSerial(0, sendStream);
                }
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
            SimCoreClient.RestartSimulator(RPlatform.SettingsContent.simulatorExcuteFileName, RPlatform.SettingsContent.simulatorUIMode);
        }

        private void menItemPause_Click(object sender, EventArgs e)
        {
            SimCoreClient.PauseSimulator(true);
        }

        private void menItemContinue_Click(object sender, EventArgs e)
        {
            SimCoreClient.PauseSimulator(false);
        }
    }
}
