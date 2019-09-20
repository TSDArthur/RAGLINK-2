namespace RAGLINKCommons.RAGLINKPlatform
{
	partial class formSummaryInit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formSummaryInit));
            this.labelInit = new System.Windows.Forms.Label();
            this.timerLoader = new System.Windows.Forms.Timer(this.components);
            this.progressBarLoading = new System.Windows.Forms.ProgressBar();
            this.pictureBoxBackground = new System.Windows.Forms.PictureBox();
            this.timerTopMost = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // labelInit
            // 
            this.labelInit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInit.AutoSize = true;
            this.labelInit.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInit.ForeColor = System.Drawing.Color.Black;
            this.labelInit.Location = new System.Drawing.Point(2, 196);
            this.labelInit.Name = "labelInit";
            this.labelInit.Size = new System.Drawing.Size(53, 17);
            this.labelInit.TabIndex = 1;
            this.labelInit.Text = "等待中...";
            // 
            // timerLoader
            // 
            this.timerLoader.Interval = 600;
            this.timerLoader.Tick += new System.EventHandler(this.TimerLoader_Tick);
            // 
            // progressBarLoading
            // 
            this.progressBarLoading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarLoading.Location = new System.Drawing.Point(5, 216);
            this.progressBarLoading.Name = "progressBarLoading";
            this.progressBarLoading.Size = new System.Drawing.Size(367, 17);
            this.progressBarLoading.TabIndex = 2;
            // 
            // pictureBoxBackground
            // 
            this.pictureBoxBackground.Image = global::RAGLINKCommons.Properties.Resources.Background;
            this.pictureBoxBackground.Location = new System.Drawing.Point(-1, 0);
            this.pictureBoxBackground.Name = "pictureBoxBackground";
            this.pictureBoxBackground.Size = new System.Drawing.Size(381, 189);
            this.pictureBoxBackground.TabIndex = 3;
            this.pictureBoxBackground.TabStop = false;
            // 
            // timerTopMost
            // 
            this.timerTopMost.Tick += new System.EventHandler(this.TimerTopMost_Tick);
            // 
            // formSummaryInit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(379, 239);
            this.Controls.Add(this.pictureBoxBackground);
            this.Controls.Add(this.progressBarLoading);
            this.Controls.Add(this.labelInit);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formSummaryInit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "传输数据到模拟器...";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSummaryInit_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSummaryInit_FormClosed);
            this.Load += new System.EventHandler(this.FormSummaryInit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label labelInit;
		private System.Windows.Forms.Timer timerLoader;
        private System.Windows.Forms.ProgressBar progressBarLoading;
        private System.Windows.Forms.PictureBox pictureBoxBackground;
        private System.Windows.Forms.Timer timerTopMost;
    }
}