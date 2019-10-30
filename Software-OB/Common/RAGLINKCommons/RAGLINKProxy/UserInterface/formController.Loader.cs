using System;
using System.Windows.Forms;

namespace RAGLINKCommons.RAGLINKProxy
{
	public partial class formController : Form
	{
		private int handleValue = 0;
		private bool applyDataInLoading = false;
		private bool boardConnection = true;
		private void UpdateButtonEnableState()
		{
			for (int i = 0; i < RAGLINKPlatform.ControlObjects.controlObjectsCount; i++)
			{
				if (!RAGLINKPlatform.ControlObjects.controlObjectArrange[i].objectEnable)
				{
					switch ((RAGLINKPlatform.ControlObjects.ControlObjectsList)i)
					{
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.AIR_CONDITION:
							{
								menItemAircon.Enabled = false;
								buttonAirconOn.Enabled = false;
								buttonAirconOff.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.ATC:
							{
								menItemATC.Enabled = false;
								buttonATCOn.Enabled = false;
								buttonATCOff.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.COMBINED_HANDLE:
							{
								menItemHandle.Enabled = false;
								buttonHandleUp.Enabled = false;
								buttonHandleDown.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.DEADMAN:
							{
								menItemDeadman.Enabled = false;
								buttonDeadmanOn.Enabled = false;
								buttonDeadmanOff.Enabled = false;
								buttonDeadmanPress.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.DOOR:
							{
								menItemLeftDoor.Enabled = false;
								menItemRightDoor.Enabled = false;
								buttonLeftDoorOn.Enabled = false;
								buttonLeftDoorOff.Enabled = false;
								buttonRightDoorOn.Enabled = false;
								buttonRightDoorOff.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.EMERGENCY:
							{
								menItemEmergency.Enabled = false;
								buttonEmergencyOn.Enabled = false;
								buttonEmergencyOff.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.HORN:
							{
								menItemHorn.Enabled = false;
								buttonHornOn.Enabled = false;
								buttonHornOff.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.LIGHT:
							{
								menItemHeadlight.Enabled = false;
								buttonHeadlightOn.Enabled = false;
								buttonHeadlightOff.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.MASTER_KEY:
							{
								menItemMatserKey.Enabled = false;
								buttonMasterKeyOn.Enabled = false;
								buttonMasterKeyOff.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.PANTO:
							{
								menItemPanto.Enabled = false;
								buttonPantoOn.Enabled = false;
								buttonPantoOff.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.REVERSER:
							{
								menItemReverser.Enabled = false;
								buttonReverserF.Enabled = false;
								buttonReverserIDLE.Enabled = false;
								buttonReverserB.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.SAND:
							{
								menItemSand.Enabled = false;
								buttonSandOn.Enabled = false;
								buttonSandOff.Enabled = false;
								break;
							}
						case RAGLINKPlatform.ControlObjects.ControlObjectsList.TRACTION:
							{
								menItemTraction.Enabled = false;
								buttonTractionOn.Enabled = false;
								buttonTractionOff.Enabled = false;
								break;
							}
					}
				}
			}
		}
		private void UpdateLabels()
		{
			try
			{
				//Update monitor
				string labelSpeedTextPart0 = (Math.Abs((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.SPEED_HMI) / 10)).ToString();
				string labelSpeedTextPart1 = (Math.Abs((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.SPEED_HMI)) % 10).ToString();
				labelSpeed.Text = labelSpeedTextPart0 + "." + labelSpeedTextPart1 + " KM/H";
				string labelLimitText = ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.SPEED_LIMIT)).ToString();
				labelLimit.Text = labelLimitText + " KM/H";
				string labelSignalText = "无码";
				switch ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.SIGNAL))
				{
					case 0:
						{
							labelSignalText = "停车";
							break;
						}
					case 1:
						{
							labelSignalText = "接近";
							break;
						}
					case 2:
						{
							labelSignalText = "通过";
							break;
						}
                    case 3:
                        {
                            labelSignalText = "通过";
                            break;
                        }
				}
				labelSignal.Text = labelSignalText;
				string labelReverserText = "N";
				switch ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.REVERSER))
				{
					case 1:
						{
							labelReverserText = "F";
							break;
						}
					case 0:
						{
							labelReverserText = "N";
							break;
						}
					case -1:
						{
							labelReverserText = "B";
							break;
						}
				}
				labelReverser.Text = labelReverserText;
				string labelHandleText = "P0";
				switch ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.POWER))
				{
					case 1:
						{
							labelHandleText = "P1";
							break;
						}
					case 2:
						{
							labelHandleText = "P2";
							break;
						}
					case 3:
						{
							labelHandleText = "P3";
							break;
						}
				}
				switch ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.BRAKE))
				{
					case 2:
						{
							labelHandleText = "B1";
							break;
						}
					case 4:
						{
							labelHandleText = "B2";
							break;
						}
					case 6:
						{
							labelHandleText = "B3";
							break;
						}
				}
				labelHandle.Text = labelHandleText;
				string labelCurrentStationText = RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.CURRENT_STATION_NAME).ToString();
				labelCurrentStation.Text = "当前车站：" + labelCurrentStationText;
				string labelDepartTimeText = RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.CURRENT_STATION_DEPART_TIME).ToString();
				labelDepartTime.Text = "出发时间：" + labelDepartTimeText;
				string labelNextStationCombinedTextPart0 = RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.NEXT_STATION_NAME).ToString();
				string labelNextStationCombinedTextPart1 = (int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.NEXT_STATION_STOP) == 1 ? "通过" : "停车";
				labelNextStationCombined.Text = "下一站：" + labelNextStationCombinedTextPart0 + " / " + labelNextStationCombinedTextPart1;
				string labelArrivalTimeText = RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.NEXT_STATION_ARRIVAL_TIME).ToString();
				labelArrivalTime.Text = "到达时间：" + labelArrivalTimeText;
				double nextStationDistance = (double)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.NEXT_STATION_DISTANCE);
				string labelNextStationDistanceText = (nextStationDistance >= 1000 ?
				decimal.Round(decimal.Parse((nextStationDistance / 1000).ToString()), 2).ToString() :
				decimal.Round(decimal.Parse((nextStationDistance).ToString()), 1).ToString()) +
				((double)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.NEXT_STATION_DISTANCE) >= 1000 ? "KM" : "M");
				labelNextStationDistance.Text = "对标距离：" + labelNextStationDistanceText;
				string labelSignalDistanceText = ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.SIGNAL_DIST)).ToString();
				labelSignalDistance.Text = "信号距离: " + labelSignalDistanceText + "M";
				double trainPipePressure = (double)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.PIPE_PRESSURE);
				string labelTrainPipePressureText = decimal.Round(decimal.Parse((trainPipePressure).ToString()), 1).ToString();
				labelTrainPipePressure.Text = "列车管气压: " + labelTrainPipePressureText + "kPa";
				double cylinerPressure = (double)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.CYLINER_PRESSURE);
				string labelCylinderPressureText = decimal.Round(decimal.Parse((cylinerPressure).ToString()), 1).ToString();
				labelCylinderPressure.Text = "总风管气压: " + labelCylinderPressureText + "kPa";
				string labelCurrentTimeText = RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.CURRENT_TIME).ToString();
				labelCurrentTime.Text = "当前时间: " + labelCurrentTimeText;
				//Update labels and menu
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.MASTER_KEY) == 1)
				{
					labelMasterKey.BackColor = System.Drawing.Color.Green;
					menItemMatserKeyOn.Checked = true;
					menItemMatserKeyOff.Checked = false;
				}
				else
				{
					labelMasterKey.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemMatserKeyOn.Checked = false;
					menItemMatserKeyOff.Checked = true;
				}
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.HORN) == 1)
				{
					labelHorn.BackColor = System.Drawing.Color.Green;
					menItemHornOn.Checked = true;
					menItemHornOff.Checked = false;
				}
				else
				{
					labelHorn.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemHornOn.Checked = false;
					menItemHornOff.Checked = true;
				}
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.EMERGENCY) == 1)
				{
					labelEmergency.BackColor = System.Drawing.Color.Green;
					menItemEmergencyOn.Checked = true;
					menItemEmergencyOff.Checked = false;
				}
				else
				{
					labelEmergency.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemEmergencyOn.Checked = false;
					menItemEmergencyOff.Checked = true;
				}
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.SPEED_CONST) == 1)
				{
					labelATC.BackColor = System.Drawing.Color.Green;
					menItemATCOn.Checked = true;
					menItemATCOff.Checked = false;
				}
				else
				{
					labelATC.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemATCOn.Checked = false;
					menItemATCOff.Checked = true;
				}
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.LDOOR_OPEN) == 1)
				{
					labelLeftDoor.BackColor = System.Drawing.Color.Green;
					menItemLeftDoorOn.Checked = true;
					menItemLeftDoorOff.Checked = false;
				}
				else
				{
					labelLeftDoor.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemLeftDoorOn.Checked = false;
					menItemLeftDoorOff.Checked = true;
				}
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.RDOOR_OPEN) == 1)
				{
					labelRightDoor.BackColor = System.Drawing.Color.Green;
					menItemRightDoorOn.Checked = true;
					menItemRightDoorOff.Checked = false;
				}
				else
				{
					labelRightDoor.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemRightDoorOn.Checked = false;
					menItemRightDoorOff.Checked = true;
				}
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.SANDER_ON) == 1)
				{
					labelSand.BackColor = System.Drawing.Color.Green;
					menItemSandOn.Checked = true;
					menItemSandOff.Checked = false;
				}
				else
				{
					labelSand.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemSandOn.Checked = false;
					menItemSandOff.Checked = true;
				}
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.LIGHT_ON) == 1)
				{
					labelHeadlight.BackColor = System.Drawing.Color.Green;
					menItemHeadlightOn.Checked = true;
					menItemHeadlightOff.Checked = false;
				}
				else
				{
					labelHeadlight.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemHeadlightOn.Checked = false;
					menItemHeadlightOff.Checked = true;
				}
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.PANTO_UP) == 1)
				{
					labelPantograph.BackColor = System.Drawing.Color.Green;
					menItemPantoOn.Checked = true;
					menItemPantoOff.Checked = false;
				}
				else
				{
					labelPantograph.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemPantoOn.Checked = false;
					menItemPantoOff.Checked = true;
				}
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.TRACTION_ON) == 1)
				{
					labelTraction.BackColor = System.Drawing.Color.Green;
					menItemTractionOn.Checked = true;
					menItemTractionOff.Checked = false;
				}
				else
				{
					labelTraction.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemTractionOn.Checked = false;
					menItemTractionOff.Checked = true;
				}
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.AIR_CONDITION_ON) == 1)
				{
					labelAircon.BackColor = System.Drawing.Color.Green;
					menItemAirconOn.Checked = true;
					menItemAirconOff.Checked = false;
				}
				else
				{
					labelAircon.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemAirconOn.Checked = false;
					menItemAirconOff.Checked = true;
				}
				if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.DEADMAN_ENABLE) == 1)
				{
					labelDeadman.BackColor = System.Drawing.Color.Green;
					menItemDeadmanOn.Checked = true;
					menItemDeadmanOff.Checked = false;
				}
				else
				{
					labelDeadman.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
					menItemDeadmanOn.Checked = false;
					menItemDeadmanOff.Checked = true;
				}
				//Handle checks
				menItemHandleB3.Checked = false;
				menItemHandleB2.Checked = false;
				menItemHandleB1.Checked = false;
				menItemHandleP0.Checked = false;
				menItemHandleP1.Checked = false;
				menItemHandleP2.Checked = false;
				menItemHandleP3.Checked = false;
				switch ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.POWER))
				{
					case 0:
						{
							if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.BRAKE) > 0)
								menItemHandleP0.Checked = false;
							else menItemHandleP0.Checked = true;
							break;
						}
					case 1:
						{
							menItemHandleP1.Checked = true;
							break;
						}
					case 2:
						{
							menItemHandleP2.Checked = true;
							break;
						}
					case 3:
						{
							menItemHandleP3.Checked = true;
							break;
						}
				}
				switch ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.BRAKE))
				{
					case 0:
						{
							if ((int)RAGLINKPlatform.DataManager.GetTrainData((int)RAGLINKPlatform.DataManager.TrainDataMap.POWER) > 0)
								menItemHandleP0.Checked = false;
							else menItemHandleP0.Checked = true;
							break;
						}
					case 2:
						{
							menItemHandleB1.Checked = true;
							break;
						}
					case 4:
						{
							menItemHandleB2.Checked = true;
							break;
						}
					case 6:
						{
							menItemHandleB3.Checked = true;
							break;
						}
				}
			}
			catch (Exception) { };
		}
        private void SimulatorWindowsQuit()
        {
            this.Close();
        }
	}
}
