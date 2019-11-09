using System;
using System.IO;
using System.Windows.Forms;
using SystemHotKey;

namespace RAGLINKFileEditor.UserInterface
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        Hotkey hotkey;
        int HotkeySaveFile;
        private void FormMain_Load(object sender, EventArgs e)
        {
            //Load theme
            FormThemeApplier.SetStyle(MainMenuStrip, WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender.VsVersion.Vs2015, DockPanelTheme);
            FormThemeApplier.SetStyle(contextMenuStripMain, WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender.VsVersion.Vs2015, DockPanelTheme);
            //Update paths
            RAGLINKCommons.RPlatform.SettingsContent.UpdateSettingsPath();
            //Setup hotkey
            /*
            hotkey = new Hotkey(this.Handle);
            HotkeySaveFile = hotkey.RegisterHotkey(Keys.S, Hotkey.KeyFlags.MOD_CONTROL);
            hotkey.OnHotkey += new HotkeyEventHandler(SaveFileHotKey);
            */
            //Open file
            if (FileIO.FileInfo.argsFileNameList.Count > 0)
            {
                OpenFile(FileIO.FileInfo.argsFileNameList[0]);
            }
            else
            {
                FileIO.FileInfo.isFileEdited = false;
                NewFile();
            }
        }

        private void MenuItemOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "RAGLINK文件|*" +
                RAGLINKCommons.RPlatform.SettingsContent.projectFileExtName + ";*" +
                RAGLINKCommons.RPlatform.SettingsContent.universalFileExtName
            };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openDialog.FileName))
                {
                    CloseFile();
                    OpenFile(openDialog.FileName);
                }
            }
        }

        private void MenuItemQuit_Click(object sender, EventArgs e)
        {
            CloseFile();
            Application.Exit();
        }

        private void MenuItemUndo_Click(object sender, EventArgs e)
        {
            textBoxEditor.Undo();
        }

        private void MenuItemRedo_Click(object sender, EventArgs e)
        {
            textBoxEditor.Undo();
        }

        private void MenuItemCopy_Click(object sender, EventArgs e)
        {
            textBoxEditor.Copy();
        }

        private void MenuItemPaste_Click(object sender, EventArgs e)
        {
            textBoxEditor.Paste();
        }

        private void MenuItemCut_Click(object sender, EventArgs e)
        {
            textBoxEditor.Cut();
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            textBoxEditor.ClearSelected();
        }

        private void MenuItemFind_Click(object sender, EventArgs e)
        {
            textBoxEditor.ShowFindDialog();
        }

        private void MenuItemReplace_Click(object sender, EventArgs e)
        {
            textBoxEditor.ShowReplaceDialog();
        }

        private void MenuItemSelectAll_Click(object sender, EventArgs e)
        {
            textBoxEditor.SelectAll();
        }

        private void MenuItemZoomPlus_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxEditor.Zoom += 10;
            }
            catch (Exception) { };
        }

        private void MenuItemZoomDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxEditor.Zoom >= 10)
                {
                    textBoxEditor.Zoom -= 10;
                }
            }
            catch (Exception) { };
        }

        private void TimerEvent_Tick(object sender, EventArgs e)
        {
            buttonAddText.Enabled = textBoxAddText.Text.Length > 0;
            menuItemUndo.Enabled = textBoxEditor.UndoEnabled;
            menuItemRedo.Enabled = textBoxEditor.RedoEnabled;
            menuItemPaste.Enabled = Clipboard.GetDataObject() != null;
            menuItemSaveFile.Enabled = FileIO.FileInfo.isFileEdited;
            menuItemSaveAs.Enabled = (!FileIO.FileInfo.isFileEdited) && (FileIO.FileInfo.filePath != string.Empty);
        }

        private void TextBoxEditor_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (!FileIO.FileInfo.isFileEdited && !inLoading)
            {
                this.Text += "*";
            }

            if (!inLoading)
            {
                FileIO.FileInfo.isFileEdited = true;
            }
        }

        private void MenuItemNew_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void MenuItemSaveFile_Click(object sender, EventArgs e)
        {
            SaveFile(false);
        }

        private void MenuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RAGLINK 文件编辑器\n版本：1.01\n©2019 TSDWorks\nRAGLINK 文件编辑器是轻量化实验平台RAGLINK Studio的组件。", "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseFile();
        }

        private void ButtonAddText_Click(object sender, EventArgs e)
        {
            textBoxEditor.InsertText(textBoxAddText.Text);
            textBoxAddText.Text = string.Empty;
        }

        private void MenuItemSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileIO.FileInfo.isFileEdited)
                {
                    SaveFile(false);
                }

                string saveAsPath = SelectPath("另存为...");
                if (saveAsPath != string.Empty)
                {
                    SaveFileByPath(saveAsPath, false);
                    if (File.Exists(saveAsPath))
                    {
                        CloseFile();
                        OpenFile(saveAsPath);
                    }
                }
            }
            catch (Exception) { };
        }
    }
}
