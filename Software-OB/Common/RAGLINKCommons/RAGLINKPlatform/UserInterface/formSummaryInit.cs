using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RAGLINKCommons;

namespace RAGLINKCommons.RAGLINKPlatform
{
	public partial class formSummaryInit : Form
	{
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        private bool loadingInBusy = false;
        private ProjectLoader.LoadingState currentLoadingState;
        public delegate void SetLabelInitTextDelegate(string textValue);
        public delegate void SetProgressBarValueDelegate(int dataValue);
        public formSummaryInit()
		{
			InitializeComponent();
		}
		private void FormSummaryInit_Load(object sender, EventArgs e)
		{
			this.TopMost = true;
			UserInterfaceSwap.simulatorLaunchTrig = false;
            ProjectLoader.ProcessEvent += LoadingProcessManage;
            ProjectLoader.ProjectLoaderSetup(UserInterfaceSwap.projectGUID);
            currentLoadingState = ProjectLoader.LoadingState.WAIT_FOR_START;
            loadingInBusy = false;
            timerLoader.Enabled = true;
		}
        private void SetLabelInitText(string textValue)
        {
            labelInit.Text = textValue;
        }
        private void SetProgressBarValue(int dataValue)
        {
            progressBarLoading.Value = dataValue;
        }
        private void LoadingProcessManage(ProjectLoader.LoadingState processValue)
        {
            switch (processValue)
            {
                case ProjectLoader.LoadingState.READY:
                    {
                        SetLabelInitTextDelegate setLabelInitTextDelegate = new SetLabelInitTextDelegate(SetLabelInitText);
                        Invoke(setLabelInitTextDelegate, "初始化环境...");
                        SetProgressBarValueDelegate setProgressBarValueDelegate = new SetProgressBarValueDelegate(SetProgressBarValue);
                        Invoke(setProgressBarValueDelegate, 5);
                        currentLoadingState = ProjectLoader.LoadingState.READY;
                        loadingInBusy = false;
                        break;
                    }
                case ProjectLoader.LoadingState.DATA_RESET:
                    {
                        SetLabelInitTextDelegate setLabelInitTextDelegate = new SetLabelInitTextDelegate(SetLabelInitText);
                        Invoke(setLabelInitTextDelegate, "更新环境数据...");
                        SetProgressBarValueDelegate setProgressBarValueDelegate = new SetProgressBarValueDelegate(SetProgressBarValue);
                        Invoke(setProgressBarValueDelegate, 10);
                        currentLoadingState = ProjectLoader.LoadingState.DATA_RESET;
                        loadingInBusy = false;
                        break;
                    }
                case ProjectLoader.LoadingState.DATA_UPDATED:
                    {
                        SetLabelInitTextDelegate setLabelInitTextDelegate = new SetLabelInitTextDelegate(SetLabelInitText);
                        Invoke(setLabelInitTextDelegate, "传输计划数据...");
                        SetProgressBarValueDelegate setProgressBarValueDelegate = new SetProgressBarValueDelegate(SetProgressBarValue);
                        Invoke(setProgressBarValueDelegate, 20);
                        currentLoadingState = ProjectLoader.LoadingState.DATA_UPDATED;
                        loadingInBusy = false;
                        break;
                    }
                case ProjectLoader.LoadingState.PROJECT_DEFINE_LOADED:
                    {
                        SetLabelInitTextDelegate setLabelInitTextDelegate = new SetLabelInitTextDelegate(SetLabelInitText);
                        Invoke(setLabelInitTextDelegate, "传输资源包数据...");
                        SetProgressBarValueDelegate setProgressBarValueDelegate = new SetProgressBarValueDelegate(SetProgressBarValue);
                        Invoke(setProgressBarValueDelegate, 30);
                        currentLoadingState = ProjectLoader.LoadingState.PROJECT_DEFINE_LOADED;
                        loadingInBusy = false;
                        break;
                    }
                case ProjectLoader.LoadingState.PACKAGE_DEFINE_LOADED:
                    {
                        SetLabelInitTextDelegate setLabelInitTextDelegate = new SetLabelInitTextDelegate(SetLabelInitText);
                        Invoke(setLabelInitTextDelegate, "传输车辆数据...");
                        SetProgressBarValueDelegate setProgressBarValueDelegate = new SetProgressBarValueDelegate(SetProgressBarValue);
                        Invoke(setProgressBarValueDelegate, 40);
                        currentLoadingState = ProjectLoader.LoadingState.PACKAGE_DEFINE_LOADED;
                        loadingInBusy = false;
                        break;
                    }
                case ProjectLoader.LoadingState.TRAIN_LOADED:
                    {
                        SetLabelInitTextDelegate setLabelInitTextDelegate = new SetLabelInitTextDelegate(SetLabelInitText);
                        Invoke(setLabelInitTextDelegate, "传输线路数据（可能需要一定时间）...");
                        SetProgressBarValueDelegate setProgressBarValueDelegate = new SetProgressBarValueDelegate(SetProgressBarValue);
                        Invoke(setProgressBarValueDelegate, 50);
                        currentLoadingState = ProjectLoader.LoadingState.TRAIN_LOADED;
                        loadingInBusy = false;
                        break;
                    }
                case ProjectLoader.LoadingState.ROUTE_LOADED:
                    {
                        SetLabelInitTextDelegate setLabelInitTextDelegate = new SetLabelInitTextDelegate(SetLabelInitText);
                        Invoke(setLabelInitTextDelegate, "传输模拟器画面设置...");
                        SetProgressBarValueDelegate setProgressBarValueDelegate = new SetProgressBarValueDelegate(SetProgressBarValue);
                        Invoke(setProgressBarValueDelegate, 80);
                        currentLoadingState = ProjectLoader.LoadingState.ROUTE_LOADED;
                        loadingInBusy = false;
                        break;
                    }
                case ProjectLoader.LoadingState.SIM_OPTIONS_LOADED:
                    {
                        SetLabelInitTextDelegate setLabelInitTextDelegate = new SetLabelInitTextDelegate(SetLabelInitText);
                        Invoke(setLabelInitTextDelegate, "配置嵌入式数据服务器...");
                        SetProgressBarValueDelegate setProgressBarValueDelegate = new SetProgressBarValueDelegate(SetProgressBarValue);
                        Invoke(setProgressBarValueDelegate, 85);
                        currentLoadingState = ProjectLoader.LoadingState.SIM_OPTIONS_LOADED;
                        loadingInBusy = false;
                        break;
                    }
                case ProjectLoader.LoadingState.DATA_SERVER_START:
                    {
                        SetLabelInitTextDelegate setLabelInitTextDelegate = new SetLabelInitTextDelegate(SetLabelInitText);
                        Invoke(setLabelInitTextDelegate, "校验行车计划...");
                        SetProgressBarValueDelegate setProgressBarValueDelegate = new SetProgressBarValueDelegate(SetProgressBarValue);
                        Invoke(setProgressBarValueDelegate, 90);
                        currentLoadingState = ProjectLoader.LoadingState.DATA_SERVER_START;
                        loadingInBusy = false;
                        break;
                    }
                case ProjectLoader.LoadingState.DONE:
                    {
                        SetLabelInitTextDelegate setLabelInitTextDelegate = new SetLabelInitTextDelegate(SetLabelInitText);
                        Invoke(setLabelInitTextDelegate, "数据传输完成。");
                        SetProgressBarValueDelegate setProgressBarValueDelegate = new SetProgressBarValueDelegate(SetProgressBarValue);
                        Invoke(setProgressBarValueDelegate, 100);
                        currentLoadingState = ProjectLoader.LoadingState.DONE;
                        loadingInBusy = false;
                        break;
                    }
                case ProjectLoader.LoadingState.ERROR:
                    {
                        SetLabelInitTextDelegate setLabelInitTextDelegate = new SetLabelInitTextDelegate(SetLabelInitText);
                        Invoke(setLabelInitTextDelegate, "数据传输失败。");
                        SetProgressBarValueDelegate setProgressBarValueDelegate = new SetProgressBarValueDelegate(SetProgressBarValue);
                        Invoke(setProgressBarValueDelegate, 100);
                        currentLoadingState = ProjectLoader.LoadingState.ERROR;
                        loadingInBusy = false;
                        break;
                    }
            }
        }
		private void TimerLoader_Tick(object sender, EventArgs e)
		{
			timerLoader.Enabled = false;
			if(!loadingInBusy)
            {
                if (currentLoadingState == ProjectLoader.LoadingState.DONE)
                {
                    timerLoader.Enabled = false;
                    UserInterfaceSwap.showMode = UserInterfaceSwap.formSummaryShowMode.STARTER;
                    new System.Threading.Thread((System.Threading.ThreadStart)delegate
                    {
                        Application.Run(new formSummary());
                    }).Start();
                    this.Close();
                    return;
                }
                else if (currentLoadingState == ProjectLoader.LoadingState.ERROR)
                {
                    MessageBox.Show("加载过程中遇到错误，加载中止。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                else
                {
                    //MessageBox.Show(currentLoadingState.ToString());
                    loadingInBusy = true;
                    ProjectLoader.ProjectLoaderProcess(currentLoadingState);
                }
            }
			timerLoader.Enabled = true;
		}

        private void FormSummaryInit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentLoadingState != ProjectLoader.LoadingState.DONE &&
                currentLoadingState != ProjectLoader.LoadingState.ERROR)
                e.Cancel = true;
        }

        private void TimerTopMost_Tick(object sender, EventArgs e)
        {
            SetForegroundWindow(this.Handle);
        }

        private void FormSummaryInit_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProjectLoader.ProcessEvent -= LoadingProcessManage;
        }
    }
}
