﻿namespace RAGLINKCommons.RAGLINKPlatform
{
	partial class formSummary
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "总概览"}, 0, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "主控板"}, 1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))));
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "操作对象"}, 2, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))));
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "设备连接"}, 3, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))));
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formSummary));
            this.labelPlanName = new System.Windows.Forms.Label();
            this.labelTitleName = new System.Windows.Forms.Label();
            this.panelProjectSummary = new System.Windows.Forms.Panel();
            this.labelDetailsLoading = new System.Windows.Forms.Label();
            this.listViewDetails = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewSummaryItem = new System.Windows.Forms.ListView();
            this.imageListSummaryItem = new System.Windows.Forms.ImageList(this.components);
            this.pictureBoxPlanName = new System.Windows.Forms.PictureBox();
            this.labelSummary = new System.Windows.Forms.Label();
            this.panelError = new System.Windows.Forms.Panel();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.buttonLaunch = new System.Windows.Forms.Button();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelMain = new System.Windows.Forms.ToolStripLabel();
            this.listViewErrors = new System.Windows.Forms.ListView();
            this.columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListError = new System.Windows.Forms.ImageList(this.components);
            this.timerEvents = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxBVE = new System.Windows.Forms.PictureBox();
            this.timerTopMost = new System.Windows.Forms.Timer(this.components);
            this.panelProjectSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlanName)).BeginInit();
            this.panelError.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBVE)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPlanName
            // 
            this.labelPlanName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlanName.AutoEllipsis = true;
            this.labelPlanName.BackColor = System.Drawing.Color.Gainsboro;
            this.labelPlanName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPlanName.ForeColor = System.Drawing.Color.Black;
            this.labelPlanName.Location = new System.Drawing.Point(468, 4);
            this.labelPlanName.Name = "labelPlanName";
            this.labelPlanName.Size = new System.Drawing.Size(579, 24);
            this.labelPlanName.TabIndex = 62;
            this.labelPlanName.Text = "已加载的行车计划：无";
            this.labelPlanName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTitleName
            // 
            this.labelTitleName.AutoEllipsis = true;
            this.labelTitleName.BackColor = System.Drawing.Color.Gainsboro;
            this.labelTitleName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitleName.ForeColor = System.Drawing.Color.Black;
            this.labelTitleName.Location = new System.Drawing.Point(34, 5);
            this.labelTitleName.Name = "labelTitleName";
            this.labelTitleName.Size = new System.Drawing.Size(169, 24);
            this.labelTitleName.TabIndex = 61;
            this.labelTitleName.Text = "RAGLINK Simulator OB";
            this.labelTitleName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelProjectSummary
            // 
            this.panelProjectSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProjectSummary.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelProjectSummary.Controls.Add(this.labelDetailsLoading);
            this.panelProjectSummary.Controls.Add(this.listViewDetails);
            this.panelProjectSummary.Controls.Add(this.listViewSummaryItem);
            this.panelProjectSummary.Controls.Add(this.pictureBoxPlanName);
            this.panelProjectSummary.Controls.Add(this.labelSummary);
            this.panelProjectSummary.Location = new System.Drawing.Point(3, 37);
            this.panelProjectSummary.Name = "panelProjectSummary";
            this.panelProjectSummary.Size = new System.Drawing.Size(1056, 503);
            this.panelProjectSummary.TabIndex = 63;
            // 
            // labelDetailsLoading
            // 
            this.labelDetailsLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDetailsLoading.AutoEllipsis = true;
            this.labelDetailsLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelDetailsLoading.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDetailsLoading.ForeColor = System.Drawing.Color.Black;
            this.labelDetailsLoading.Location = new System.Drawing.Point(170, 185);
            this.labelDetailsLoading.Name = "labelDetailsLoading";
            this.labelDetailsLoading.Size = new System.Drawing.Size(844, 184);
            this.labelDetailsLoading.TabIndex = 65;
            this.labelDetailsLoading.Text = "载入中...";
            this.labelDetailsLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listViewDetails
            // 
            this.listViewDetails.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDetails.BackColor = System.Drawing.Color.White;
            this.listViewDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewDetails.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewDetails.FullRowSelect = true;
            this.listViewDetails.GridLines = true;
            this.listViewDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDetails.HideSelection = false;
            this.listViewDetails.Location = new System.Drawing.Point(123, 29);
            this.listViewDetails.MultiSelect = false;
            this.listViewDetails.Name = "listViewDetails";
            this.listViewDetails.Size = new System.Drawing.Size(929, 470);
            this.listViewDetails.TabIndex = 17;
            this.listViewDetails.UseCompatibleStateImageBehavior = false;
            this.listViewDetails.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "标题";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "内容";
            this.columnHeader2.Width = 550;
            // 
            // listViewSummaryItem
            // 
            this.listViewSummaryItem.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listViewSummaryItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewSummaryItem.AutoArrange = false;
            this.listViewSummaryItem.BackColor = System.Drawing.Color.White;
            this.listViewSummaryItem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewSummaryItem.FullRowSelect = true;
            this.listViewSummaryItem.GridLines = true;
            this.listViewSummaryItem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewSummaryItem.HideSelection = false;
            this.listViewSummaryItem.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.listViewSummaryItem.Location = new System.Drawing.Point(4, 29);
            this.listViewSummaryItem.MultiSelect = false;
            this.listViewSummaryItem.Name = "listViewSummaryItem";
            this.listViewSummaryItem.Size = new System.Drawing.Size(118, 470);
            this.listViewSummaryItem.SmallImageList = this.imageListSummaryItem;
            this.listViewSummaryItem.TabIndex = 17;
            this.listViewSummaryItem.UseCompatibleStateImageBehavior = false;
            this.listViewSummaryItem.View = System.Windows.Forms.View.List;
            // 
            // imageListSummaryItem
            // 
            this.imageListSummaryItem.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSummaryItem.ImageStream")));
            this.imageListSummaryItem.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSummaryItem.Images.SetKeyName(0, "Summary");
            this.imageListSummaryItem.Images.SetKeyName(1, "Board");
            this.imageListSummaryItem.Images.SetKeyName(2, "Object");
            this.imageListSummaryItem.Images.SetKeyName(3, "Connection");
            this.imageListSummaryItem.Images.SetKeyName(4, "Record");
            this.imageListSummaryItem.Images.SetKeyName(5, "Fault");
            // 
            // pictureBoxPlanName
            // 
            this.pictureBoxPlanName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxPlanName.Image = global::RAGLINKCommons.Properties.Resources.About;
            this.pictureBoxPlanName.Location = new System.Drawing.Point(5, 7);
            this.pictureBoxPlanName.Name = "pictureBoxPlanName";
            this.pictureBoxPlanName.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxPlanName.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxPlanName.TabIndex = 17;
            this.pictureBoxPlanName.TabStop = false;
            // 
            // labelSummary
            // 
            this.labelSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSummary.AutoEllipsis = true;
            this.labelSummary.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelSummary.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSummary.ForeColor = System.Drawing.Color.Black;
            this.labelSummary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelSummary.Location = new System.Drawing.Point(23, 3);
            this.labelSummary.Name = "labelSummary";
            this.labelSummary.Size = new System.Drawing.Size(1025, 24);
            this.labelSummary.TabIndex = 4;
            this.labelSummary.Text = "行车计划概述";
            this.labelSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelError
            // 
            this.panelError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelError.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelError.Controls.Add(this.buttonQuit);
            this.panelError.Controls.Add(this.buttonLaunch);
            this.panelError.Controls.Add(this.toolStripMain);
            this.panelError.Controls.Add(this.listViewErrors);
            this.panelError.Location = new System.Drawing.Point(2, 537);
            this.panelError.Name = "panelError";
            this.panelError.Size = new System.Drawing.Size(1056, 182);
            this.panelError.TabIndex = 64;
            // 
            // buttonQuit
            // 
            this.buttonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuit.AutoEllipsis = true;
            this.buttonQuit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonQuit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonQuit.Location = new System.Drawing.Point(812, 154);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(120, 24);
            this.buttonQuit.TabIndex = 13;
            this.buttonQuit.Text = "取消模拟";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.ButtonQuit_Click);
            // 
            // buttonLaunch
            // 
            this.buttonLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLaunch.AutoEllipsis = true;
            this.buttonLaunch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonLaunch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLaunch.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonLaunch.Location = new System.Drawing.Point(933, 154);
            this.buttonLaunch.Name = "buttonLaunch";
            this.buttonLaunch.Size = new System.Drawing.Size(120, 24);
            this.buttonLaunch.TabIndex = 14;
            this.buttonLaunch.Text = "确认并进入模拟";
            this.buttonLaunch.UseVisualStyleBackColor = true;
            this.buttonLaunch.Click += new System.EventHandler(this.ButtonLaunch_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelMain});
            this.toolStripMain.Location = new System.Drawing.Point(0, 157);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1056, 25);
            this.toolStripMain.TabIndex = 16;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripLabelMain
            // 
            this.toolStripLabelMain.Image = global::RAGLINKCommons.Properties.Resources.Info;
            this.toolStripLabelMain.Name = "toolStripLabelMain";
            this.toolStripLabelMain.Size = new System.Drawing.Size(75, 22);
            this.toolStripLabelMain.Text = "加载成功.";
            // 
            // listViewErrors
            // 
            this.listViewErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewErrors.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listViewErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderType,
            this.columnHeaderCode,
            this.columnHeaderDesc});
            this.listViewErrors.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewErrors.FullRowSelect = true;
            this.listViewErrors.GridLines = true;
            this.listViewErrors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewErrors.HideSelection = false;
            this.listViewErrors.LargeImageList = this.imageListError;
            this.listViewErrors.Location = new System.Drawing.Point(5, 0);
            this.listViewErrors.MultiSelect = false;
            this.listViewErrors.Name = "listViewErrors";
            this.listViewErrors.Size = new System.Drawing.Size(1048, 150);
            this.listViewErrors.SmallImageList = this.imageListError;
            this.listViewErrors.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewErrors.StateImageList = this.imageListError;
            this.listViewErrors.TabIndex = 15;
            this.listViewErrors.UseCompatibleStateImageBehavior = false;
            this.listViewErrors.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderType
            // 
            this.columnHeaderType.Text = "类型";
            this.columnHeaderType.Width = 100;
            // 
            // columnHeaderCode
            // 
            this.columnHeaderCode.Text = "代号";
            this.columnHeaderCode.Width = 120;
            // 
            // columnHeaderDesc
            // 
            this.columnHeaderDesc.Text = "描述";
            this.columnHeaderDesc.Width = 400;
            // 
            // imageListError
            // 
            this.imageListError.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListError.ImageStream")));
            this.imageListError.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListError.Images.SetKeyName(0, "Info");
            this.imageListError.Images.SetKeyName(1, "Warning");
            this.imageListError.Images.SetKeyName(2, "Error");
            // 
            // timerEvents
            // 
            this.timerEvents.Enabled = true;
            this.timerEvents.Tick += new System.EventHandler(this.TimerEvents_Tick);
            // 
            // pictureBoxBVE
            // 
            this.pictureBoxBVE.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBoxBVE.Image = global::RAGLINKCommons.Properties.Resources.icon;
            this.pictureBoxBVE.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxBVE.Name = "pictureBoxBVE";
            this.pictureBoxBVE.Size = new System.Drawing.Size(30, 28);
            this.pictureBoxBVE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBVE.TabIndex = 60;
            this.pictureBoxBVE.TabStop = false;
            // 
            // timerTopMost
            // 
            this.timerTopMost.Enabled = true;
            this.timerTopMost.Tick += new System.EventHandler(this.TimerTopMost_Tick);
            // 
            // formSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1059, 719);
            this.Controls.Add(this.panelError);
            this.Controls.Add(this.panelProjectSummary);
            this.Controls.Add(this.labelPlanName);
            this.Controls.Add(this.labelTitleName);
            this.Controls.Add(this.pictureBoxBVE);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(810, 617);
            this.Name = "formSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "行车计划概述";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSummary_FormClosed);
            this.Load += new System.EventHandler(this.FormPlatform_Load);
            this.panelProjectSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlanName)).EndInit();
            this.panelError.ResumeLayout(false);
            this.panelError.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBVE)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelPlanName;
		private System.Windows.Forms.Label labelTitleName;
		private System.Windows.Forms.PictureBox pictureBoxBVE;
		private System.Windows.Forms.Panel panelProjectSummary;
		private System.Windows.Forms.Panel panelError;
		private System.Windows.Forms.Button buttonLaunch;
		private System.Windows.Forms.Button buttonQuit;
		private System.Windows.Forms.ListView listViewErrors;
		private System.Windows.Forms.ColumnHeader columnHeaderType;
		private System.Windows.Forms.ColumnHeader columnHeaderCode;
		private System.Windows.Forms.ColumnHeader columnHeaderDesc;
		private System.Windows.Forms.ToolStrip toolStripMain;
		private System.Windows.Forms.ToolStripLabel toolStripLabelMain;
		private System.Windows.Forms.ImageList imageListError;
		private System.Windows.Forms.Label labelSummary;
		private System.Windows.Forms.PictureBox pictureBoxPlanName;
		private System.Windows.Forms.ListView listViewSummaryItem;
		private System.Windows.Forms.ImageList imageListSummaryItem;
		private System.Windows.Forms.ListView listViewDetails;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label labelDetailsLoading;
		private System.Windows.Forms.Timer timerEvents;
        private System.Windows.Forms.Timer timerTopMost;
    }
}