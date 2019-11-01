namespace RAGLINKCommons.RAGLINKProxy
{
	partial class formDataMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formDataMonitor));
            this.buttonClose = new System.Windows.Forms.Button();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.labelDataMonitorTitle = new System.Windows.Forms.Label();
            this.panelDataMonitor = new System.Windows.Forms.Panel();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.listViewDataMonitor = new ListViewDoubleBuffered.ListViewNF();
            this.columnHeaderID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderUpdateType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBoxDataMonitor = new System.Windows.Forms.PictureBox();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.panelDataMonitor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDataMonitor)).BeginInit();
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
            // timerUpdate
            // 
            this.timerUpdate.Interval = 200;
            this.timerUpdate.Tick += new System.EventHandler(this.TimerUpdate_Tick);
            // 
            // labelDataMonitorTitle
            // 
            this.labelDataMonitorTitle.AutoEllipsis = true;
            this.labelDataMonitorTitle.AutoSize = true;
            this.labelDataMonitorTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelDataMonitorTitle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDataMonitorTitle.ForeColor = System.Drawing.Color.Black;
            this.labelDataMonitorTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelDataMonitorTitle.Location = new System.Drawing.Point(25, 5);
            this.labelDataMonitorTitle.Name = "labelDataMonitorTitle";
            this.labelDataMonitorTitle.Size = new System.Drawing.Size(65, 19);
            this.labelDataMonitorTitle.TabIndex = 25;
            this.labelDataMonitorTitle.Text = "数据列表";
            this.labelDataMonitorTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelDataMonitor
            // 
            this.panelDataMonitor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelDataMonitor.Controls.Add(this.buttonRefresh);
            this.panelDataMonitor.Controls.Add(this.listViewDataMonitor);
            this.panelDataMonitor.Controls.Add(this.labelDataMonitorTitle);
            this.panelDataMonitor.Controls.Add(this.pictureBoxDataMonitor);
            this.panelDataMonitor.Location = new System.Drawing.Point(0, 37);
            this.panelDataMonitor.Name = "panelDataMonitor";
            this.panelDataMonitor.Size = new System.Drawing.Size(670, 510);
            this.panelDataMonitor.TabIndex = 27;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.AutoEllipsis = true;
            this.buttonRefresh.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonRefresh.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonRefresh.Location = new System.Drawing.Point(576, 2);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(91, 24);
            this.buttonRefresh.TabIndex = 28;
            this.buttonRefresh.Text = "重新加载";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // listViewDataMonitor
            // 
            this.listViewDataMonitor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listViewDataMonitor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderID,
            this.columnHeaderDesc,
            this.columnHeaderType,
            this.columnHeaderUpdateType,
            this.columnHeaderValue});
            this.listViewDataMonitor.FullRowSelect = true;
            this.listViewDataMonitor.GridLines = true;
            this.listViewDataMonitor.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDataMonitor.HideSelection = false;
            this.listViewDataMonitor.Location = new System.Drawing.Point(3, 27);
            this.listViewDataMonitor.Name = "listViewDataMonitor";
            this.listViewDataMonitor.Size = new System.Drawing.Size(664, 480);
            this.listViewDataMonitor.TabIndex = 27;
            this.listViewDataMonitor.UseCompatibleStateImageBehavior = false;
            this.listViewDataMonitor.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderID
            // 
            this.columnHeaderID.Text = "ID";
            // 
            // columnHeaderDesc
            // 
            this.columnHeaderDesc.Text = "描述";
            this.columnHeaderDesc.Width = 140;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "数据类型";
            this.columnHeaderType.Width = 80;
            // 
            // columnHeaderUpdateType
            // 
            this.columnHeaderUpdateType.Text = "更新类型";
            this.columnHeaderUpdateType.Width = 140;
            // 
            // columnHeaderValue
            // 
            this.columnHeaderValue.Text = "值";
            this.columnHeaderValue.Width = 220;
            // 
            // pictureBoxDataMonitor
            // 
            this.pictureBoxDataMonitor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxDataMonitor.Image = global::RAGLINKCommons.Properties.Resources.About;
            this.pictureBoxDataMonitor.Location = new System.Drawing.Point(5, 5);
            this.pictureBoxDataMonitor.Name = "pictureBoxDataMonitor";
            this.pictureBoxDataMonitor.Size = new System.Drawing.Size(19, 19);
            this.pictureBoxDataMonitor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxDataMonitor.TabIndex = 26;
            this.pictureBoxDataMonitor.TabStop = false;
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.Image = global::RAGLINKCommons.Properties.Resources.banner;
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, -1);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(269, 38);
            this.pictureBoxBanner.TabIndex = 66;
            this.pictureBoxBanner.TabStop = false;
            // 
            // formDataMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(669, 581);
            this.Controls.Add(this.pictureBoxBanner);
            this.Controls.Add(this.panelDataMonitor);
            this.Controls.Add(this.buttonClose);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(685, 620);
            this.Name = "formDataMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据监视器";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormStreamRecord_Load);
            this.panelDataMonitor.ResumeLayout(false);
            this.panelDataMonitor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDataMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Timer timerUpdate;
		private System.Windows.Forms.Label labelDataMonitorTitle;
		private System.Windows.Forms.PictureBox pictureBoxDataMonitor;
		private System.Windows.Forms.Panel panelDataMonitor;
		private System.Windows.Forms.ColumnHeader columnHeaderID;
		private System.Windows.Forms.ColumnHeader columnHeaderType;
		private System.Windows.Forms.ColumnHeader columnHeaderUpdateType;
		private System.Windows.Forms.ColumnHeader columnHeaderValue;
		private System.Windows.Forms.ColumnHeader columnHeaderDesc;
		private System.Windows.Forms.Button buttonRefresh;
		private ListViewDoubleBuffered.ListViewNF listViewDataMonitor;
        private System.Windows.Forms.PictureBox pictureBoxBanner;
    }
}
