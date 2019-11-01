using System;
using System.Linq;
using System.Windows.Forms;

namespace RAGLINKCommons.RAGLINKPlatform
{
	public partial class formSummary : Form
	{
		private int lastSelectedItem = 0;
		private int currentLoadSummaryPage = -1;
		private void LoadItemSummary()
		{
			labelSummary.Text = "行车计划概述 - 总概览";
			labelDetailsLoading.Visible = true;
			listViewDetails.Cursor = System.Windows.Forms.Cursors.AppStarting;
			listViewDetails.Clear();
			listViewDetails.ShowGroups = false;
			listViewDetails.BeginUpdate();
			listViewDetails.Columns.Add("标题", 150, HorizontalAlignment.Left);
			listViewDetails.Columns.Add("内容", 450, HorizontalAlignment.Left);
			for (int i = 0; i < 12; i++)
			{
				ListViewItem listViewItem = new ListViewItem();
				switch (i)
				{
					case 0:
						{
							listViewItem.Text = "计划名称";
							listViewItem.SubItems.Add(ProjectsManager.projectInfo.projectName);
							break;
						}
					case 1:
						{
							listViewItem.Text = "GUID";
							listViewItem.SubItems.Add(ProjectsManager.projectInfo.projectID);
							break;
						}
					case 2:
						{
							listViewItem.Text = "计划描述";
							listViewItem.SubItems.Add(ProjectsManager.projectInfo.projectDescribe);
							break;
						}
					case 3:
						{
							listViewItem.Text = "计划版本";
							listViewItem.SubItems.Add(ProjectsManager.projectInfo.projectVersion.ToString());
							break;
						}
					case 4:
						{
							listViewItem.Text = "计划模拟器支持版本";
							listViewItem.SubItems.Add(ProjectsManager.projectInfo.projectSimulatorSupportVersion.ToString());
							break;
						}
					case 5:
						{
							listViewItem.Text = "计划作者";
							listViewItem.SubItems.Add(ProjectsManager.projectInfo.projectAuthor);
							break;
						}
					case 6:
						{
							listViewItem.Text = "计划网站";
							listViewItem.SubItems.Add(ProjectsManager.projectInfo.projectAuthorWebsite);
							break;
						}
					case 7:
						{
							listViewItem.Text = "主控板型号";
							listViewItem.SubItems.Add(BoardsManager.boardInfo.boardName);
							break;
						}
					case 8:
						{
							listViewItem.Text = "主控板编号";
							listViewItem.SubItems.Add(BoardsManager.boardInfo.boardID.ToString());
							break;
						}
					case 9:
						{
							listViewItem.Text = "主控板端口";
							listViewItem.SubItems.Add(ProjectsManager.projectInfo.boardPort);
							break;
						}
					case 10:
						{
							listViewItem.Text = "主控板资源使用情况";
							int usedCount = ProjectsManager.projectInfo.deviceUsedCount;
							int boardCount = BoardsManager.boardInfo.boardIOCount;
							listViewItem.SubItems.Add("共 " + boardCount.ToString() + " 个IO资源，已使用 "
								+ usedCount.ToString() + " 个("
								+ (usedCount * 100 / boardCount).ToString() + "%)。");
							break;
						}
                    case 11:
                        {
                            listViewItem.Text = "数据服务器地址";
                            listViewItem.SubItems.Add(CommunicationNetwork.dataServerAddress);
                            break;
                        }
				}
				listViewDetails.Items.Add(listViewItem);
			}
			listViewDetails.EndUpdate();
			listViewDetails.Cursor = System.Windows.Forms.Cursors.Default;
			labelDetailsLoading.Visible = false;
		}
		private void LoadItemBoardInfo()
		{
			labelSummary.Text = "行车计划概述 - 主控板";
			labelDetailsLoading.Visible = true;
			listViewDetails.Cursor = System.Windows.Forms.Cursors.AppStarting;
			listViewDetails.ShowGroups = false;
			listViewDetails.Clear();
			listViewDetails.BeginUpdate();
			listViewDetails.Columns.Add("标题", 150, HorizontalAlignment.Left);
			listViewDetails.Columns.Add("内容", 450, HorizontalAlignment.Left);
			for (int i = 0; i < 5; i++)
			{
				ListViewItem listViewItem = new ListViewItem();
				switch (i)
				{
					case 0:
						{
							listViewItem.Text = "主控板型号";
							listViewItem.SubItems.Add(BoardsManager.boardInfo.boardName);
							break;
						}
					case 1:
						{
							listViewItem.Text = "主控板编号";
							listViewItem.SubItems.Add(BoardsManager.boardInfo.boardID.ToString());
							break;
						}
					case 2:
						{
							listViewItem.Text = "主控板提供商";
							listViewItem.SubItems.Add(BoardsManager.boardInfo.boardSupplier);
							break;
						}
					case 3:
						{
							listViewItem.Text = "主控板简介";
							listViewItem.SubItems.Add(BoardsManager.boardInfo.boardDescribe);
							break;
						}
                    case 4:
                        {
                            listViewItem.Text = "主控板最低模拟器兼容版本";
                            listViewItem.SubItems.Add(BoardsManager.boardInfo.boardSupprtVersion.ToString());
                            break;
                        }
                }
				listViewDetails.Items.Add(listViewItem);   
			}
			listViewDetails.EndUpdate();
			listViewDetails.Cursor = System.Windows.Forms.Cursors.Default;
			labelDetailsLoading.Visible = false;
		}
		private void LoadItemControlObjectsInfo()
		{
			try
			{
				labelSummary.Text = "行车计划概述 - 操作对象";
				labelDetailsLoading.Visible = true;
				listViewDetails.Cursor = System.Windows.Forms.Cursors.AppStarting;
				listViewDetails.Clear();
				listViewDetails.ShowGroups = true ;
				listViewDetails.BeginUpdate();
				listViewDetails.Columns.Add("标题", 150, HorizontalAlignment.Left);
				listViewDetails.Columns.Add("内容", 250, HorizontalAlignment.Left);
				listViewDetails.Columns.Add("值列表", 200, HorizontalAlignment.Left);
				for (int i = 0; i < ControlObjects.controlObjectsCount; i++)
				{
					ListViewGroup listViewGroup = new ListViewGroup();
					listViewGroup.Header = "ID: " + i.ToString() + " " + ControlObjects.controlObjectsInfo[i].objectName;
					listViewDetails.Groups.Add(listViewGroup);
					ListViewItem listViewItem0 = new ListViewItem();
					listViewItem0.Text = "操作对象简述";
					listViewItem0.SubItems.Add(ControlObjects.controlObjectsInfo[i].objectDescribe);
					listViewDetails.Groups[i].Items.Add(listViewItem0);
					listViewDetails.Items.Add(listViewItem0);
					ListViewItem listViewItem1 = new ListViewItem();
					listViewItem1.Text = "必要操作对象";
					listViewItem1.SubItems.Add(ControlObjects.controlObjectsInfo[i].objectSetEnable == false ? "是" : "否");
					listViewDetails.Groups[i].Items.Add(listViewItem1);
					listViewDetails.Items.Add(listViewItem1);
					ListViewItem listViewItem2 = new ListViewItem();
					listViewItem2.Text = "主定时器绑定";
					listViewItem2.SubItems.Add(ControlObjects.controlObjectsInfo[i].objectTimerAttached == true ? "是" : "否");
					listViewDetails.Groups[i].Items.Add(listViewItem2);
					listViewDetails.Items.Add(listViewItem2);
					ListViewItem listViewItem3 = new ListViewItem();
					listViewItem3.Text = "启用";
					listViewItem3.SubItems.Add(ControlObjects.controlObjectArrange[i].objectEnable == true ? "是" : "否");
					if (ControlObjects.controlObjectArrange[i].objectEnable) listViewItem3.ForeColor = System.Drawing.Color.Green;
					else listViewItem3.ForeColor = System.Drawing.Color.Red;
					listViewDetails.Groups[i].Items.Add(listViewItem3);
					listViewDetails.Items.Add(listViewItem3);
					if (ControlObjects.controlObjectArrange[i].objectEnable)
					{
						ListViewItem listViewItem4 = new ListViewItem();
						listViewItem4.Text = "对象IO设备数量";
						listViewItem4.SubItems.Add(ControlObjects.controlObjectsInfo[i].objectIOCount.ToString());
						listViewDetails.Groups[i].Items.Add(listViewItem4);
						listViewDetails.Items.Add(listViewItem4);
						for (int j = 0; j < ControlObjects.controlObjectsInfo[i].objectIOCount; j++)
						{
							ListViewItem listViewItem = new ListViewItem();
							listViewItem.Text = ControlObjects.controlObjectsInfo[i].objectIODescrible[j];
							string ioModeDescribe = string.Empty;
                            ioModeDescribe = ControlObjects.controlObjectsInfo[i].objectIOMode[j].ToString();
							listViewItem.SubItems.Add(ioModeDescribe);
							if (ControlObjects.controlObjectsInfo[i].objectIOMode[j] == DevicesManager.DevicesIOMode.DEVICE_SERIAL)
							{
								listViewItem.SubItems.Add(ControlObjects.controlObjectArrange[i].objectIO[j].ToString()
									+ ", " + ControlObjects.controlObjectArrange[i].objectIOSerialBaud.ToString());
							}
							else listViewItem.SubItems.Add(ControlObjects.controlObjectArrange[i].objectIO[j].ToString());
							listViewDetails.Groups[i].Items.Add(listViewItem);
							listViewDetails.Items.Add(listViewItem);
						}
					}
				}
				listViewDetails.EndUpdate();
				listViewDetails.Cursor = System.Windows.Forms.Cursors.Default;
				labelDetailsLoading.Visible = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void LoadItemConnectedDevices()
		{
			labelSummary.Text = "行车计划概述 - 设备连接";
			labelDetailsLoading.Visible = true;
			listViewDetails.Cursor = System.Windows.Forms.Cursors.AppStarting;
			listViewDetails.Clear();
			listViewDetails.ShowGroups = false;
			listViewDetails.BeginUpdate();
			listViewDetails.Columns.Add("ID", 150, HorizontalAlignment.Left);
			listViewDetails.Columns.Add("端口", 450, HorizontalAlignment.Left);
			for (int i = 0; i < CommunicationSerial.GetSerialPortNames().Count; i++)
			{
				ListViewItem listViewItem = new ListViewItem();
				listViewItem.Text = "设备" + (i + 1).ToString();
				listViewItem.SubItems.Add(CommunicationSerial.GetSerialPortNames()[i]);
				listViewDetails.Items.Add(listViewItem);
			}
			listViewDetails.EndUpdate();
			listViewDetails.Cursor = System.Windows.Forms.Cursors.Default;
			labelDetailsLoading.Visible = false;
		}
	}
}
