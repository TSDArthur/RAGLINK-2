namespace RStarter
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonLaunch = new System.Windows.Forms.Button();
            this.listViewPlatform = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.labelLaunching = new System.Windows.Forms.Label();
            this.timerAutoLaunch = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonLaunch
            // 
            this.buttonLaunch.Enabled = false;
            this.buttonLaunch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLaunch.Location = new System.Drawing.Point(237, 242);
            this.buttonLaunch.Name = "buttonLaunch";
            this.buttonLaunch.Size = new System.Drawing.Size(112, 29);
            this.buttonLaunch.TabIndex = 1;
            this.buttonLaunch.Text = "启动";
            this.buttonLaunch.UseVisualStyleBackColor = true;
            this.buttonLaunch.Click += new System.EventHandler(this.ButtonLaunch_Click);
            // 
            // listViewPlatform
            // 
            this.listViewPlatform.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.listViewPlatform.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewPlatform.CheckBoxes = true;
            this.listViewPlatform.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderVersion,
            this.columnHeaderDesc});
            this.listViewPlatform.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewPlatform.FullRowSelect = true;
            this.listViewPlatform.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPlatform.HideSelection = false;
            this.listViewPlatform.Location = new System.Drawing.Point(2, 2);
            this.listViewPlatform.MultiSelect = false;
            this.listViewPlatform.Name = "listViewPlatform";
            this.listViewPlatform.Size = new System.Drawing.Size(347, 236);
            this.listViewPlatform.SmallImageList = this.imageList;
            this.listViewPlatform.TabIndex = 3;
            this.listViewPlatform.UseCompatibleStateImageBehavior = false;
            this.listViewPlatform.View = System.Windows.Forms.View.Details;
            this.listViewPlatform.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewPlatform_ItemChecked);
            this.listViewPlatform.Click += new System.EventHandler(this.listViewPlatform_Click);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "平台名称";
            this.columnHeaderName.Width = 100;
            // 
            // columnHeaderVersion
            // 
            this.columnHeaderVersion.Text = "版本";
            this.columnHeaderVersion.Width = 45;
            // 
            // columnHeaderDesc
            // 
            this.columnHeaderDesc.Text = "简述";
            this.columnHeaderDesc.Width = 202;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "PlugIns.png");
            // 
            // labelLaunching
            // 
            this.labelLaunching.AutoSize = true;
            this.labelLaunching.Location = new System.Drawing.Point(4, 249);
            this.labelLaunching.Name = "labelLaunching";
            this.labelLaunching.Size = new System.Drawing.Size(179, 17);
            this.labelLaunching.TabIndex = 4;
            this.labelLaunching.Text = "将于 5 秒钟后自动启动选定项。";
            // 
            // timerAutoLaunch
            // 
            this.timerAutoLaunch.Interval = 1000;
            this.timerAutoLaunch.Tick += new System.EventHandler(this.timerAutoLaunch_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(351, 276);
            this.Controls.Add(this.labelLaunching);
            this.Controls.Add(this.buttonLaunch);
            this.Controls.Add(this.listViewPlatform);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择平台";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonLaunch;
        private System.Windows.Forms.ListView listViewPlatform;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderVersion;
        private System.Windows.Forms.ColumnHeader columnHeaderDesc;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label labelLaunching;
        private System.Windows.Forms.Timer timerAutoLaunch;
    }
}

