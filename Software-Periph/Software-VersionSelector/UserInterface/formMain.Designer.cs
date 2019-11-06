namespace RAGLINK_Version_Selector
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonLaunch = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.listViewPlatform = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLaunch
            // 
            this.buttonLaunch.Enabled = false;
            this.buttonLaunch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLaunch.Location = new System.Drawing.Point(223, 548);
            this.buttonLaunch.Name = "buttonLaunch";
            this.buttonLaunch.Size = new System.Drawing.Size(112, 29);
            this.buttonLaunch.TabIndex = 1;
            this.buttonLaunch.Text = "启动";
            this.buttonLaunch.UseVisualStyleBackColor = true;
            this.buttonLaunch.Click += new System.EventHandler(this.ButtonLaunch_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(341, 548);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(112, 29);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // listViewPlatform
            // 
            this.listViewPlatform.CheckBoxes = true;
            this.listViewPlatform.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderVersion,
            this.columnHeaderDesc});
            this.listViewPlatform.FullRowSelect = true;
            this.listViewPlatform.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPlatform.HideSelection = false;
            this.listViewPlatform.Location = new System.Drawing.Point(3, 405);
            this.listViewPlatform.MultiSelect = false;
            this.listViewPlatform.Name = "listViewPlatform";
            this.listViewPlatform.Size = new System.Drawing.Size(459, 134);
            this.listViewPlatform.TabIndex = 3;
            this.listViewPlatform.UseCompatibleStateImageBehavior = false;
            this.listViewPlatform.View = System.Windows.Forms.View.Details;
            this.listViewPlatform.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewPlatform_ItemChecked);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "平台名称";
            this.columnHeaderName.Width = 100;
            // 
            // columnHeaderVersion
            // 
            this.columnHeaderVersion.Text = "版本";
            this.columnHeaderVersion.Width = 80;
            // 
            // columnHeaderDesc
            // 
            this.columnHeaderDesc.Text = "简述";
            this.columnHeaderDesc.Width = 240;
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.Image = global::RAGLINK_Version_Selector.Properties.Resources.banner;
            this.pictureBoxBanner.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(469, 42);
            this.pictureBoxBanner.TabIndex = 4;
            this.pictureBoxBanner.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.BackgroundImage = global::RAGLINK_Version_Selector.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(465, 589);
            this.Controls.Add(this.pictureBoxBanner);
            this.Controls.Add(this.listViewPlatform);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonLaunch);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RAGLINK 版本选择器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonLaunch;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ListView listViewPlatform;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderVersion;
        private System.Windows.Forms.ColumnHeader columnHeaderDesc;
        private System.Windows.Forms.PictureBox pictureBoxBanner;
    }
}

