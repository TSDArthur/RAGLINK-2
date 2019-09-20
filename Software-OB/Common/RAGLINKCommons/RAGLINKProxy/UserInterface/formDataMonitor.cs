using System;
using System.Windows.Forms;

namespace RAGLINKCommons.RAGLINKProxy
{
	public partial class formDataMonitor : Form
	{
		public formDataMonitor()
		{
			InitializeComponent();
			Control.CheckForIllegalCrossThreadCalls = false;
		}

		private void FormStreamRecord_Load(object sender, EventArgs e)
		{
			this.Left = UserInterfaceSwap.formControllerLeft;
			this.Top = UserInterfaceSwap.formControllerTop + (UserInterfaceSwap.formControllerHeight - this.Height) / 2;
			this.TopMost = true;
			ListViewFirstUpdate();
			timerUpdate.Enabled = true;
		}

		private void ButtonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void TimerUpdate_Tick(object sender, EventArgs e)
		{
			UpdateDataMonitor();
		}

		private void ButtonRefresh_Click(object sender, EventArgs e)
		{
			timerUpdate.Enabled = false;
			ListViewFirstUpdate();
			timerUpdate.Enabled = true;
		}
	}
}
