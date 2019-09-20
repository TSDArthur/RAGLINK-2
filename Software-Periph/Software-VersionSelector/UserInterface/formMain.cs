using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAGLINK_Version_Selector
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Hide();
            UpdatePlatformList();
            StartArgsEvents(Environment.GetCommandLineArgs());
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
        }

        private void ButtonLaunch_Click(object sender, EventArgs e)
        {
            FileIO.VersionManager.StartPlatformExecute(FileIO.VersionManager.platformList.platformVerification[platformSelectedItem], "-ui");
            this.Close();
        }
    }
}
