using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RAGLINKCommons.RAGLINKProxy
{
    public partial class formStreamRecord : Form
	{
		public formStreamRecord()
		{
			InitializeComponent();
			Control.CheckForIllegalCrossThreadCalls = false;
		}

		private void FormStreamRecord_Load(object sender, EventArgs e)
		{
			this.Left = UserInterfaceSwap.formControllerLeft;
			this.Top = UserInterfaceSwap.formControllerTop + (UserInterfaceSwap.formControllerHeight - this.Height) / 2;
			this.TopMost = true;
			//Menu recieved
			ContextMenuStrip listboxMenuRecieved = new ContextMenuStrip();
			ToolStripMenuItem rightMenuRecieved = new ToolStripMenuItem("复制项");
			rightMenuRecieved.Click += new EventHandler(CopyItemRecieved);
			listboxMenuRecieved.Items.AddRange(new ToolStripItem[] { rightMenuRecieved });
			listBoxRecieved.ContextMenuStrip = listboxMenuRecieved;
			//Menu sent
			ContextMenuStrip listboxMenuSent = new ContextMenuStrip();
			ToolStripMenuItem rightMenuSent = new ToolStripMenuItem("复制项");
			rightMenuSent.Click += new EventHandler(CopyItemSent);
			listboxMenuSent.Items.AddRange(new ToolStripItem[] { rightMenuSent });
			listBoxSent.ContextMenuStrip = listboxMenuSent;
		}
		private void CopyItemRecieved(object sender, EventArgs e)
		{
			var newThread = new Thread(() => Clipboard.SetText(listBoxRecieved.Items[listBoxRecieved.SelectedIndex].ToString()));
			newThread.SetApartmentState(ApartmentState.STA);
			newThread.Start();
		}
		private void CopyItemSent(object sender, EventArgs e)
		{
			var newThread = new Thread(() => Clipboard.SetText(listBoxSent.Items[listBoxSent.SelectedIndex].ToString()));
			newThread.SetApartmentState(ApartmentState.STA);
			newThread.Start();
		}
		private void ButtonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void TimerUpdate_Tick(object sender, EventArgs e)
		{
			try
			{
				if (!RAGLINKPlatform.CommunicationSerial.serialList[0].IsOpen)
				{
					RAGLINKPlatform.CommunicationSerial.StreamRecord.Clear();
					listBoxCombinedStream.Items.Clear();
					listBoxSent.Items.Clear();
					listBoxRecieved.Items.Clear();
					listBoxCombinedStream.Enabled = false;
					listBoxRecieved.Enabled = false;
					listBoxSent.Enabled = false;
					tabControlShowType.Enabled = false;
					labelNoConnection.Visible = true;
					return;
				}
				listBoxCombinedStream.Enabled = true;
				listBoxRecieved.Enabled = true;
				listBoxSent.Enabled = true;
				tabControlShowType.Enabled = true;
				labelNoConnection.Visible = false; ;
				listBoxCombinedStream.BeginUpdate();
				listBoxRecieved.BeginUpdate();
				listBoxSent.BeginUpdate();
				try
				{
					if (listBoxCombinedStream.Items.Count < RAGLINKPlatform.CommunicationSerial.StreamRecord.boardStreamCombined.Count)
					{
						for (int i = listBoxCombinedStream.Items.Count; i < RAGLINKPlatform.CommunicationSerial.StreamRecord.boardStreamCombined.Count; i++)
						{
							string typeString = RAGLINKPlatform.CommunicationSerial.StreamRecord.boardStreamCombined[i].recordType == 0 ? "接收: " : "发送: ";
							listBoxCombinedStream.Items.Add(typeString + RAGLINKPlatform.CommunicationSerial.StreamRecord.boardStreamCombined[i].dataStream);
							listBoxCombinedStream.SelectedIndex = listBoxCombinedStream.Items.Count - 1;
						}
					}
					else if(listBoxCombinedStream.Items.Count > RAGLINKPlatform.CommunicationSerial.StreamRecord.boardStreamCombined.Count)
					{
						listBoxCombinedStream.Items.Clear();
					}
				}
				catch (Exception) { };
				try
				{
					if (listBoxRecieved.Items.Count < RAGLINKPlatform.CommunicationSerial.StreamRecord.boardSerialStreamRecord.Count)
					{
						for (int i = listBoxRecieved.Items.Count; i < RAGLINKPlatform.CommunicationSerial.StreamRecord.boardSerialStreamRecord.Count; i++)
							listBoxRecieved.Items.Add(RAGLINKPlatform.CommunicationSerial.StreamRecord.boardSerialStreamRecord[i]);
						listBoxRecieved.SelectedIndex = listBoxRecieved.Items.Count - 1;
					}
					else if (listBoxRecieved.Items.Count > RAGLINKPlatform.CommunicationSerial.StreamRecord.boardSerialStreamRecord.Count)
					{
						listBoxRecieved.Items.Clear();
					}
				}
				catch (Exception) { };
				try
				{
					if (listBoxSent.Items.Count < RAGLINKPlatform.CommunicationSerial.StreamRecord.boardSentStreamRecord.Count)
					{
						for (int i = listBoxSent.Items.Count; i < RAGLINKPlatform.CommunicationSerial.StreamRecord.boardSentStreamRecord.Count; i++)
							listBoxSent.Items.Add(RAGLINKPlatform.CommunicationSerial.StreamRecord.boardSentStreamRecord[i]);
						listBoxSent.SelectedIndex = listBoxSent.Items.Count - 1;
					}
					else if (listBoxSent.Items.Count > RAGLINKPlatform.CommunicationSerial.StreamRecord.boardSentStreamRecord.Count)
					{
						listBoxSent.Items.Clear();
					}
				}
				catch (Exception) { };
				listBoxCombinedStream.EndUpdate();
				listBoxRecieved.EndUpdate();
				listBoxSent.EndUpdate();
			}
			catch (Exception) { };
		}

		private void ButtonRefreshEnable_Click(object sender, EventArgs e)
		{
			if (timerUpdate.Enabled == true)
			{
				timerUpdate.Enabled = false;
				buttonRefreshEnable.Text = "继续刷新";
			}
			else
			{
				timerUpdate.Enabled = true;
				buttonRefreshEnable.Text = "停止刷新";
			}
		}
	}
}
