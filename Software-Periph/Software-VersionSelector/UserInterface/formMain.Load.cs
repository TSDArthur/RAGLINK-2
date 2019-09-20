using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RAGLINK_Version_Selector
{
    public partial class frmMain : Form
    {
        private void UpdatePlatformList()
        {
            FileIO.VersionManager.UpdatePlatformInfo();
            listViewPlatform.Items.Clear();
            listViewPlatform.BeginUpdate();
            for (int i = 0; i < FileIO.VersionManager.platformList.platformCount; i++)
            {
                ListViewItem itemAdded = new ListViewItem();
                itemAdded.Text = FileIO.VersionManager.platformList.platformVerification[i];
                itemAdded.SubItems.Add(FileIO.VersionManager.platformList.platformVersion[i].ToString());
                itemAdded.SubItems.Add(FileIO.VersionManager.platformList.platformNameDisplay[i].ToString());
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
            bool breakCircle = false;
            bool silenceMode = false;
            bool compileMode = false;
            bool specialArgs = false;
            string argsPlatformSelected = string.Empty;
            string argsProjectFilePath = string.Empty;
            string argsForPlatform = string.Empty;
            try
            {
                breakCircle = false;
                for (int i = 1; i < Args.Length; i++)
                {
                    switch (Args[i])
                    {
                        case "-select":
                            {
                                specialArgs = true;
                                silenceMode = false;
                                argsPlatformSelected = Args[i + 1].ToLower();
                                breakCircle = true;
                                break;
                            }
                        case "-start":
                            {
                                specialArgs = true;
                                silenceMode = true;
                                argsPlatformSelected = Args[i + 1];
                                for (int j = i + 2; j < Args.Length - 1; j++)
                                {
                                    argsForPlatform += Args[j];
                                    argsForPlatform += " ";
                                }
                                argsForPlatform += Args[Args.Length - 1];
                                breakCircle = true;
                                break;
                            }
                        case "-open":
                            {
                                specialArgs = true;
                                compileMode = true;
                                for (int j = i + 1; j < Args.Length; j++)
                                {
                                    argsForPlatform += Args[j];
                                    argsForPlatform += " ";
                                    if (File.Exists(argsForPlatform))
                                    {
                                        argsProjectFilePath = argsForPlatform;
                                        argsForPlatform = string.Empty;
                                    }
                                }
                                argsForPlatform += Args[Args.Length - 1];
                                breakCircle = true;
                                break;
                            }
                    }
                    if (breakCircle) break;
                }
                if(!specialArgs)
                {
                    compileMode = true;
                    for (int i = 1; i < Args.Length; i++)
                    {
                        argsForPlatform += Args[i];
                        argsForPlatform += " ";
                        if (File.Exists(argsForPlatform))
                        {
                            argsProjectFilePath = argsForPlatform;
                            argsForPlatform = string.Empty;
                        }
                    }
                    argsForPlatform += Args[Args.Length - 1];
                }
                if (compileMode)
                {
                    string projectFilePath = Path.GetDirectoryName(argsProjectFilePath);
                    FileIO.SettingsFileIO settingsFileIO = new FileIO.SettingsFileIO();
                    settingsFileIO.SetSettingsFilePath(Path.GetFullPath(projectFilePath + "\\" + "platform.ini"));
                    argsPlatformSelected = settingsFileIO.ReadValue("platform", "platform_verification").ToLower();
                    argsForPlatform = argsProjectFilePath + " " + argsForPlatform;
                    silenceMode = true;
                }
                FileIO.VersionManager.UpdatePlatformInfo();
                int platformIndex = FileIO.VersionManager.platformList.platformVerification.ConvertAll(c => c.ToLower()).IndexOf(argsPlatformSelected);
                if (platformIndex != -1)
                {
                    listViewPlatform.Items[platformIndex].Checked = true;
                    if (silenceMode)
                    {
                        FileIO.VersionManager.StartPlatformExecute(argsPlatformSelected, argsForPlatform);
                        this.Close();
                    }
                    else this.Show();
                }
            }
            catch (Exception) { };
        }
    }
}
