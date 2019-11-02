using System;
using System.Drawing;
using System.Windows.Forms;

namespace RAGLINKCommons.RAGLINKPlatform
{

	public partial class formSummary : Form
	{
        public delegate void FormMainStartButtonHandler(bool dataValue);
        static public event FormMainStartButtonHandler FormMainStartButtonEvent;
        public formSummary()
		{
			InitializeComponent();
		}
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        public int errorCount = 0;
		public int warningCount = 0;
		public int infoCount = 0;
        public bool toLaunchSimualtor = false;
		private void FormPlatform_Load(object sender, EventArgs e)
		{
            FormMainStartButtonEvent(false);
            this.TopMost = true;
            labelPlanName.Text = "已加载的行车计划：" + ProjectsManager.projectInfo.projectName + (ProjectsManager.projectInfo.projectDebug ? "[调试模式]" : "[运转模式]");
			errorCount = 0;
			warningCount = 0;
			infoCount = 0;
			listViewErrors.Items.Clear();
			listViewErrors.BeginUpdate();
			for (int i = 0; i < UserInterfaceSwap.errorContent.Count; i++)
			{
				ListViewItem itemAdded = new ListViewItem();
				switch (UserInterfaceSwap.errorContent[i].errorType)
				{
					case ProjectsManager.ErrorType.ERROR:
						{
							itemAdded.ImageIndex = 2;
							itemAdded.Text = "错误";
							itemAdded.ForeColor = Color.Red;
							errorCount++;
							break;
						}
						case ProjectsManager.ErrorType.WARNING:
						{
							itemAdded.ImageIndex = 1;
							itemAdded.Text = "警告";
							itemAdded.ForeColor = Color.Orange;
                            warningCount++;
							break;
						}
					case ProjectsManager.ErrorType.INFORMATION:
						{
							itemAdded.ImageIndex = 0;
							itemAdded.Text = "消息";
							itemAdded.ForeColor = Color.Black;
                            infoCount++;
							break;
						}
				}
				itemAdded.SubItems.Add(UserInterfaceSwap.errorContent[i].errorCode.ToString());
				itemAdded.SubItems.Add(UserInterfaceSwap.errorContent[i].errorTitle);
				listViewErrors.Items.Add(itemAdded);
			}
			listViewErrors.EndUpdate();
			if (errorCount > 0)
			{
				//buttonLaunch.Enabled = false;
				toolStripLabelMain.Image = imageListError.Images[2];
				toolStripLabelMain.Text = "加载失败，存在" +
					errorCount.ToString() + "个错误，" +
					warningCount.ToString() + "个警告，" +
					infoCount.ToString() + "个消息。请联系系统管理员获得技术支持。";
                if (!RAGLINKPlatform.ProjectsManager.projectInfo.projectDebug) buttonLaunch.Enabled = false;
			}
			else if (warningCount > 0)
			{
				toolStripLabelMain.Image = imageListError.Images[1];
				toolStripLabelMain.Text = "加载成功，存在" +
					warningCount.ToString() + "个警告，" +
					infoCount.ToString() + "个消息。可能存在不可预见的错误。";
                if (!RAGLINKPlatform.ProjectsManager.projectInfo.projectDebug) buttonLaunch.Enabled = false;
            }
			else
			{
				toolStripLabelMain.Image = imageListError.Images[0];
				toolStripLabelMain.Text = "加载成功，存在" +
					infoCount.ToString() + "个消息。";
			}
			listViewSummaryItem.Items[lastSelectedItem].Selected = true;
		}

		private void ButtonLaunch_Click(object sender, EventArgs e)
		{
            timerTopMost.Enabled = false;
            if (listViewErrors.Items.Count > 0 && MessageBox.Show(
                "调试模式下可以忽略错误与警告并进入模拟，但可能产生无法预料的后果（如强制退出、功能失效等），是否继续？", "调试模式", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                timerTopMost.Enabled = true;
                return;
            }
            RAGLINKProxy.UserInterfaceSwap.debugMode = RAGLINKPlatform.ProjectsManager.projectInfo.projectDebug;
            RAGLINKProxy.UserInterfaceSwap.showFormController = RAGLINKPlatform.ProjectsManager.projectInfo.projectDebug;
            new System.Threading.Thread((System.Threading.ThreadStart)delegate
			{
				Application.Run(new RAGLINKProxy.formController());
			}).Start();
			UserInterfaceSwap.simulatorLaunchTrig = true;
            ProjectLoader.ProjectLoaderProcess(ProjectLoader.LoadingState.DONE);
            toLaunchSimualtor = true;
            this.Close();
		}

		private void ButtonQuit_Click(object sender, EventArgs e)
		{
            this.Close();
        }

		private void TimerEvents_Tick(object sender, EventArgs e)
		{
			if (listViewSummaryItem.SelectedIndices.Count == 0)
			{
				listViewSummaryItem.Items[lastSelectedItem].Selected = true;
				listViewSummaryItem.SelectedItems[0].BackColor = SystemColors.Highlight;
				listViewSummaryItem.SelectedItems[0].ForeColor = Color.White;
			}
			else
			{
				if (listViewSummaryItem.SelectedIndices[0] != lastSelectedItem)
				{
					listViewSummaryItem.Items[lastSelectedItem].BackColor = Color.White;
					listViewSummaryItem.Items[lastSelectedItem].ForeColor = Color.Black;
					listViewSummaryItem.Items[listViewSummaryItem.SelectedIndices[0]].BackColor = SystemColors.Highlight;
					listViewSummaryItem.Items[listViewSummaryItem.SelectedIndices[0]].ForeColor = Color.White;
					lastSelectedItem = listViewSummaryItem.SelectedIndices[0];
				}
			}
			if (currentLoadSummaryPage != listViewSummaryItem.SelectedIndices[0])
			{
				switch (listViewSummaryItem.SelectedIndices[0])
				{
					case 0:
						{
							LoadItemSummary();
							break;
						}
					case 1:
						{
							LoadItemBoardInfo();
							break;
						}
					case 2:
						{
							LoadItemControlObjectsInfo();
							break;
						}
					case 3:
						{
							LoadItemConnectedDevices();
							break;
						}
				}
				currentLoadSummaryPage = listViewSummaryItem.SelectedIndices[0];
			}
		}

        private void FormSummary_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormMainStartButtonEvent(true);
        }

        private void TimerTopMost_Tick(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
