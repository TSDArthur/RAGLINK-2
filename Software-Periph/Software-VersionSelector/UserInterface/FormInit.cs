using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RStarter.UserInterface
{
    public partial class FormInit : Form
    {
        public FormInit()
        {
            InitializeComponent();
        }

        private void FormInit_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            FileIO.VersionManager.UpdatePlatformInfo();
            this.labelType.Text = FileIO.VersionManager.platformList.studioVersion.ToString() + " " + FileIO.VersionManager.platformList.studioType.ToString();
            timerDisplay.Enabled = true;
            FileIO.VersionManager.startArgs = Environment.GetCommandLineArgs();
        }

        private void timerDisplay_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.06;
            else
            {
                timerDisplay.Enabled = false;
                timerStart.Enabled = true;
            }
        }

        private void timerStart_Tick(object sender, EventArgs e)
        {
            timerStart.Enabled = false;
            FormMain formMain = new FormMain();
            formMain.ShowDialog();
        }
    }
}
