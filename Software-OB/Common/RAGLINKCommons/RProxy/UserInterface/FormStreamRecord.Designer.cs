namespace RAGLINKCommons.RProxy
{
	partial class FormStreamRecord
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStreamRecord));
            this.buttonClose = new System.Windows.Forms.Button();
            this.pictureBoxInfo0 = new System.Windows.Forms.PictureBox();
            this.labelRecieved = new System.Windows.Forms.Label();
            this.pictureBoxInfo1 = new System.Windows.Forms.PictureBox();
            this.labelSent = new System.Windows.Forms.Label();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.tabControlShowType = new System.Windows.Forms.TabControl();
            this.tabPageCombined = new System.Windows.Forms.TabPage();
            this.labelNoConnection = new System.Windows.Forms.Label();
            this.labelCombinedTitle = new System.Windows.Forms.Label();
            this.listBoxCombinedStream = new ListBoxDoubleBuffered.ListBoxNF();
            this.pictureBoxCombined = new System.Windows.Forms.PictureBox();
            this.tabPageSingle = new System.Windows.Forms.TabPage();
            this.listBoxRecieved = new ListBoxDoubleBuffered.ListBoxNF();
            this.listBoxSent = new ListBoxDoubleBuffered.ListBoxNF();
            this.buttonRefreshEnable = new System.Windows.Forms.Button();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo1)).BeginInit();
            this.tabControlShowType.SuspendLayout();
            this.tabPageCombined.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCombined)).BeginInit();
            this.tabPageSingle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.AutoEllipsis = true;
            this.buttonClose.BackColor = System.Drawing.SystemColors.Window;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonClose.Location = new System.Drawing.Point(543, 552);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(120, 24);
            this.buttonClose.TabIndex = 20;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // pictureBoxInfo0
            // 
            this.pictureBoxInfo0.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxInfo0.Image = global::RAGLINKCommons.Properties.Resources.About;
            this.pictureBoxInfo0.Location = new System.Drawing.Point(3, 244);
            this.pictureBoxInfo0.Name = "pictureBoxInfo0";
            this.pictureBoxInfo0.Size = new System.Drawing.Size(19, 19);
            this.pictureBoxInfo0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxInfo0.TabIndex = 19;
            this.pictureBoxInfo0.TabStop = false;
            // 
            // labelRecieved
            // 
            this.labelRecieved.AutoEllipsis = true;
            this.labelRecieved.AutoSize = true;
            this.labelRecieved.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelRecieved.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelRecieved.ForeColor = System.Drawing.Color.Black;
            this.labelRecieved.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelRecieved.Location = new System.Drawing.Point(22, 244);
            this.labelRecieved.Name = "labelRecieved";
            this.labelRecieved.Size = new System.Drawing.Size(65, 19);
            this.labelRecieved.TabIndex = 18;
            this.labelRecieved.Text = "接收区域";
            this.labelRecieved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBoxInfo1
            // 
            this.pictureBoxInfo1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxInfo1.Image = global::RAGLINKCommons.Properties.Resources.About;
            this.pictureBoxInfo1.Location = new System.Drawing.Point(3, 7);
            this.pictureBoxInfo1.Name = "pictureBoxInfo1";
            this.pictureBoxInfo1.Size = new System.Drawing.Size(19, 19);
            this.pictureBoxInfo1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxInfo1.TabIndex = 21;
            this.pictureBoxInfo1.TabStop = false;
            // 
            // labelSent
            // 
            this.labelSent.AutoEllipsis = true;
            this.labelSent.AutoSize = true;
            this.labelSent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelSent.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSent.ForeColor = System.Drawing.Color.Black;
            this.labelSent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelSent.Location = new System.Drawing.Point(22, 7);
            this.labelSent.Name = "labelSent";
            this.labelSent.Size = new System.Drawing.Size(65, 19);
            this.labelSent.TabIndex = 20;
            this.labelSent.Text = "发送区域";
            this.labelSent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerUpdate
            // 
            this.timerUpdate.Enabled = true;
            this.timerUpdate.Interval = 50;
            this.timerUpdate.Tick += new System.EventHandler(this.TimerUpdate_Tick);
            // 
            // tabControlShowType
            // 
            this.tabControlShowType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlShowType.Controls.Add(this.tabPageCombined);
            this.tabControlShowType.Controls.Add(this.tabPageSingle);
            this.tabControlShowType.Location = new System.Drawing.Point(2, 37);
            this.tabControlShowType.Name = "tabControlShowType";
            this.tabControlShowType.SelectedIndex = 0;
            this.tabControlShowType.Size = new System.Drawing.Size(664, 512);
            this.tabControlShowType.TabIndex = 25;
            // 
            // tabPageCombined
            // 
            this.tabPageCombined.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageCombined.Controls.Add(this.labelNoConnection);
            this.tabPageCombined.Controls.Add(this.labelCombinedTitle);
            this.tabPageCombined.Controls.Add(this.listBoxCombinedStream);
            this.tabPageCombined.Controls.Add(this.pictureBoxCombined);
            this.tabPageCombined.Location = new System.Drawing.Point(4, 26);
            this.tabPageCombined.Name = "tabPageCombined";
            this.tabPageCombined.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCombined.Size = new System.Drawing.Size(656, 482);
            this.tabPageCombined.TabIndex = 0;
            this.tabPageCombined.Text = "归并查看";
            // 
            // labelNoConnection
            // 
            this.labelNoConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNoConnection.AutoEllipsis = true;
            this.labelNoConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelNoConnection.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelNoConnection.ForeColor = System.Drawing.Color.Black;
            this.labelNoConnection.Location = new System.Drawing.Point(194, 223);
            this.labelNoConnection.Name = "labelNoConnection";
            this.labelNoConnection.Size = new System.Drawing.Size(260, 24);
            this.labelNoConnection.TabIndex = 66;
            this.labelNoConnection.Text = "未连接主控板";
            this.labelNoConnection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCombinedTitle
            // 
            this.labelCombinedTitle.AutoEllipsis = true;
            this.labelCombinedTitle.AutoSize = true;
            this.labelCombinedTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelCombinedTitle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCombinedTitle.ForeColor = System.Drawing.Color.Black;
            this.labelCombinedTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCombinedTitle.Location = new System.Drawing.Point(22, 7);
            this.labelCombinedTitle.Name = "labelCombinedTitle";
            this.labelCombinedTitle.Size = new System.Drawing.Size(65, 19);
            this.labelCombinedTitle.TabIndex = 25;
            this.labelCombinedTitle.Text = "归并查看";
            this.labelCombinedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxCombinedStream
            // 
            this.listBoxCombinedStream.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxCombinedStream.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBoxCombinedStream.FormattingEnabled = true;
            this.listBoxCombinedStream.ItemHeight = 17;
            this.listBoxCombinedStream.Location = new System.Drawing.Point(3, 29);
            this.listBoxCombinedStream.Name = "listBoxCombinedStream";
            this.listBoxCombinedStream.Size = new System.Drawing.Size(649, 412);
            this.listBoxCombinedStream.TabIndex = 27;
            // 
            // pictureBoxCombined
            // 
            this.pictureBoxCombined.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxCombined.Image = global::RAGLINKCommons.Properties.Resources.About;
            this.pictureBoxCombined.Location = new System.Drawing.Point(3, 7);
            this.pictureBoxCombined.Name = "pictureBoxCombined";
            this.pictureBoxCombined.Size = new System.Drawing.Size(19, 19);
            this.pictureBoxCombined.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxCombined.TabIndex = 26;
            this.pictureBoxCombined.TabStop = false;
            // 
            // tabPageSingle
            // 
            this.tabPageSingle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageSingle.Controls.Add(this.labelSent);
            this.tabPageSingle.Controls.Add(this.pictureBoxInfo0);
            this.tabPageSingle.Controls.Add(this.labelRecieved);
            this.tabPageSingle.Controls.Add(this.pictureBoxInfo1);
            this.tabPageSingle.Controls.Add(this.listBoxRecieved);
            this.tabPageSingle.Controls.Add(this.listBoxSent);
            this.tabPageSingle.Location = new System.Drawing.Point(4, 26);
            this.tabPageSingle.Name = "tabPageSingle";
            this.tabPageSingle.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSingle.Size = new System.Drawing.Size(656, 482);
            this.tabPageSingle.TabIndex = 1;
            this.tabPageSingle.Text = "分类查看";
            // 
            // listBoxRecieved
            // 
            this.listBoxRecieved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxRecieved.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBoxRecieved.FormattingEnabled = true;
            this.listBoxRecieved.ItemHeight = 17;
            this.listBoxRecieved.Location = new System.Drawing.Point(2, 267);
            this.listBoxRecieved.Name = "listBoxRecieved";
            this.listBoxRecieved.Size = new System.Drawing.Size(649, 208);
            this.listBoxRecieved.TabIndex = 23;
            // 
            // listBoxSent
            // 
            this.listBoxSent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBoxSent.FormattingEnabled = true;
            this.listBoxSent.ItemHeight = 17;
            this.listBoxSent.Location = new System.Drawing.Point(3, 29);
            this.listBoxSent.Name = "listBoxSent";
            this.listBoxSent.Size = new System.Drawing.Size(649, 208);
            this.listBoxSent.TabIndex = 24;
            // 
            // buttonRefreshEnable
            // 
            this.buttonRefreshEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefreshEnable.AutoEllipsis = true;
            this.buttonRefreshEnable.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonRefreshEnable.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonRefreshEnable.Location = new System.Drawing.Point(572, 36);
            this.buttonRefreshEnable.Name = "buttonRefreshEnable";
            this.buttonRefreshEnable.Size = new System.Drawing.Size(91, 24);
            this.buttonRefreshEnable.TabIndex = 26;
            this.buttonRefreshEnable.Text = "停止刷新";
            this.buttonRefreshEnable.UseVisualStyleBackColor = true;
            this.buttonRefreshEnable.Click += new System.EventHandler(this.ButtonRefreshEnable_Click);
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.Image = global::RAGLINKCommons.Properties.Resources.banner;
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, -1);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(274, 38);
            this.pictureBoxBanner.TabIndex = 66;
            this.pictureBoxBanner.TabStop = false;
            // 
            // FormStreamRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(669, 581);
            this.Controls.Add(this.pictureBoxBanner);
            this.Controls.Add(this.buttonRefreshEnable);
            this.Controls.Add(this.tabControlShowType);
            this.Controls.Add(this.buttonClose);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(685, 620);
            this.Name = "FormStreamRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "指令日志";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormStreamRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo1)).EndInit();
            this.tabControlShowType.ResumeLayout(false);
            this.tabPageCombined.ResumeLayout(false);
            this.tabPageCombined.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCombined)).EndInit();
            this.tabPageSingle.ResumeLayout(false);
            this.tabPageSingle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.PictureBox pictureBoxInfo0;
		private System.Windows.Forms.Label labelRecieved;
		private System.Windows.Forms.PictureBox pictureBoxInfo1;
		private System.Windows.Forms.Label labelSent;
		private System.Windows.Forms.Timer timerUpdate;
		private System.Windows.Forms.TabControl tabControlShowType;
		private System.Windows.Forms.TabPage tabPageCombined;
		private System.Windows.Forms.TabPage tabPageSingle;
		private System.Windows.Forms.Button buttonRefreshEnable;
		private System.Windows.Forms.Label labelCombinedTitle;
		private System.Windows.Forms.PictureBox pictureBoxCombined;
		private System.Windows.Forms.Label labelNoConnection;
		private ListBoxDoubleBuffered.ListBoxNF listBoxRecieved;
		private ListBoxDoubleBuffered.ListBoxNF listBoxSent;
		private ListBoxDoubleBuffered.ListBoxNF listBoxCombinedStream;
        private System.Windows.Forms.PictureBox pictureBoxBanner;
    }
}
