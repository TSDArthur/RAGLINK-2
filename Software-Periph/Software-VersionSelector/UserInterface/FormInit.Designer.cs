namespace RStarter.UserInterface
{
    partial class FormInit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInit));
            this.labelType = new System.Windows.Forms.Label();
            this.timerDisplay = new System.Windows.Forms.Timer(this.components);
            this.timerStart = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.BackColor = System.Drawing.Color.Transparent;
            this.labelType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelType.ForeColor = System.Drawing.Color.White;
            this.labelType.Location = new System.Drawing.Point(3, 347);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(112, 19);
            this.labelType.TabIndex = 0;
            this.labelType.Text = "1.2 Laboratory";
            // 
            // timerDisplay
            // 
            this.timerDisplay.Interval = 2;
            this.timerDisplay.Tick += new System.EventHandler(this.timerDisplay_Tick);
            // 
            // timerStart
            // 
            this.timerStart.Interval = 2000;
            this.timerStart.Tick += new System.EventHandler(this.timerStart_Tick);
            // 
            // FormInit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::RStarter.Properties.Resources.Init;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(650, 370);
            this.Controls.Add(this.labelType);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormInit";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RAGLINK+ 2";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormInit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Timer timerDisplay;
        private System.Windows.Forms.Timer timerStart;
    }
}