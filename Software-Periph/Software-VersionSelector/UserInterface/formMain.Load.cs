using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RStarter
{
    public partial class FormMain : Form
    {
        private void UpdatePlatformList()
        {
            listViewPlatform.Items.Clear();
            listViewPlatform.BeginUpdate();
            for (int i = 0; i < FileIO.VersionManager.platformList.platformCount; i++)
            {
                ListViewItem itemAdded = new ListViewItem();
                itemAdded.Text = FileIO.VersionManager.platformList.platformVerification[i];
                itemAdded.SubItems.Add(FileIO.VersionManager.platformList.platformVersion[i].ToString());
                itemAdded.SubItems.Add(FileIO.VersionManager.platformList.platformNameDisplay[i].ToString());
                itemAdded.ImageIndex = 0;
                listViewPlatform.Items.Add(itemAdded);
            }
            listViewPlatform.EndUpdate();
            if (FileIO.VersionManager.platformList.platformDefault != string.Empty)
            {
                int defaultIndex = FileIO.VersionManager.platformList.platformVerification.IndexOf(FileIO.VersionManager.platformList.platformDefault);
                if (defaultIndex != -1) listViewPlatform.Items[defaultIndex].Checked = true;
            }
        }
        private void StartArgsEvents(string[] Args)
        {
            try
            {
                FileIO.VersionManager.startArgsStr = string.Empty;
                for (int i = 1; i < Args.Length; i++)
                {
                    FileIO.VersionManager.startArgsStr += Args[i];
                    if (i != Args.Length - 1) FileIO.VersionManager.startArgsStr += " ";
                }
            }
            catch (Exception) { };
        }
    }
}
