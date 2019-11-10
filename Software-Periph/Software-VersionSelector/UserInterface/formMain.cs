using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RStarter
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Hide();
            UpdatePlatformList();
            StartArgsEvents(FileIO.VersionManager.startArgs);
            timerAutoLaunch.Enabled = buttonLaunch.Enabled;
            if (!buttonLaunch.Enabled) labelLaunching.Visible = false;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        static int platformSelectedItem = -1;
        private void ListViewPlatform_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                for (int i = 0; i < e.Item.Index; i++) listViewPlatform.Items[i].Checked = false;
                for (int i = e.Item.Index + 1; i < listViewPlatform.Items.Count; i++) listViewPlatform.Items[i].Checked = false;
                platformSelectedItem = e.Item.Index;
                buttonLaunch.Enabled = true;
            }
            else
            {
                for (int i = 0; i < listViewPlatform.Items.Count; i++)
                {
                    if (listViewPlatform.Items[i].Checked) return;
                }
                buttonLaunch.Enabled = false;
                platformSelectedItem = -1;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string defaultPlatformVerification = string.Empty;
            if (platformSelectedItem != -1)
                defaultPlatformVerification = FileIO.VersionManager.platformList.platformVerification[platformSelectedItem];
            FileIO.VersionManager.SetDefaultPlatfrom(defaultPlatformVerification);
            Application.Exit();
        }

        private void ButtonLaunch_Click(object sender, EventArgs e)
        {
            if (FileIO.VersionManager.startArgsStr != string.Empty)
                FileIO.VersionManager.StartPlatformExecute(FileIO.VersionManager.platformList.platformVerification[platformSelectedItem],
                    FileIO.VersionManager.startArgsStr);
            else FileIO.VersionManager.StartPlatformExecute(FileIO.VersionManager.platformList.platformVerification[platformSelectedItem],
                    FileIO.VersionManager.platformList.platformUIMode[platformSelectedItem]);
            this.Close();
        }

        static private int timeToLaunch = 5;
        private void timerAutoLaunch_Tick(object sender, EventArgs e)
        {
            timeToLaunch--;
            labelLaunching.Text = "将于 " + timeToLaunch.ToString() + " 秒钟后自动启动选定项。";
            if (timeToLaunch == 0) ButtonLaunch_Click(null, null);
        }

        private void listViewPlatform_Click(object sender, EventArgs e)
        {
            timerAutoLaunch.Enabled = false;
            labelLaunching.Visible = false;
        }
    }
}
