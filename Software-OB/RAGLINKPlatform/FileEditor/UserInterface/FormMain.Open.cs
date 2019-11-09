using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RAGLINKFileEditor.UserInterface
{
    public partial class FormMain : Form
    {
        static public bool inLoading = false;
        private bool OpenFile(string filePath)
        {
            bool retValue = false;
            try
            {
                inLoading = true;
                //MessageBox.Show(SettingsContent.tempFilePath);
                toolStripLabelMain.Text = "正在打开: " + filePath;
                if (!File.Exists(filePath))
                {
                    retValue = false;
                    return retValue;
                }
                string tempFileGUID = RAGLINKCommons.RPlatform.FileEncryption.GUIDGenerate() + ".tmp";
                if (!RAGLINKCommons.RPlatform.FileEncryption.DesDecryptFile(filePath, RAGLINKCommons.RPlatform.SettingsContent.tempFilePath + "\\" + tempFileGUID, RAGLINKCommons.RPlatform.SettingsContent.encryptKey))
                {
                    retValue = false;
                    return retValue;
                }
                FileIO.FileInfo.filePath = filePath;
                FileIO.FileInfo.fileName = Path.GetFileName(filePath);
                FileIO.FileInfo.fileConvertedPath = RAGLINKCommons.RPlatform.SettingsContent.tempFilePath + "\\" + tempFileGUID;
                //MessageBox.Show(FileIO.FileInfo.fileConvertedPath);
                //Read file meta data start
                RAGLINKCommons.RPlatform.SettingsFileIO settingsFileIO = new RAGLINKCommons.RPlatform.SettingsFileIO();
                settingsFileIO.SetSettingsFilePath(filePath);
                FileIO.FileInfo.fileType = settingsFileIO.GetFileType();
                settingsFileIO.Dispose();
                //Read file meta data end
                FileIO.FileInfo.isFileOpened = true;
                FileIO.FileInfo.isFileEdited = false;
                textBoxEditor.OpenFile(FileIO.FileInfo.fileConvertedPath, Encoding.Default);
                LockFile(filePath);
                //MessageBox.Show(FileIO.FileInfo.isFileEdited.ToString());
                this.Text = "RAGLINK 文件编辑器 - " + "[" + FileIO.FileInfo.fileType.ToString() + "]" + Path.GetFileName(filePath);
                toolStripLabelMain.Text = "就绪.";
            }
            catch (Exception) { };
            inLoading = false;
            return retValue;
        }
        private bool CloseFile()
        {
            bool retValue = false;
            try
            {
                if (FileIO.FileInfo.isFileEdited)
                {
                    try
                    {
                        if (MessageBox.Show("当前打开文件尚未保存，是否保存？", "关闭文件", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            SaveFile(true);
                        }
                        else
                        {
                            retValue = false;
                        }
                    }
                    catch (Exception) { };
                }
                UnlockFile();
                try
                {
                    File.Delete(FileIO.FileInfo.fileConvertedPath);
                }
                catch (Exception) { };
            }
            catch (Exception) { };
            return retValue;
        }
        private void NewFile()
        {
            try
            {
                CloseFile();
                textBoxEditor.Clear();
                FileIO.FileInfo.isFileOpened = true;
                FileIO.FileInfo.isFileEdited = false;
                this.Text = "RAGLINK 文件编辑器 - Untitled";
                string tempFileGUID = RAGLINKCommons.RPlatform.FileEncryption.GUIDGenerate() + ".tmp";
                FileIO.FileInfo.filePath = string.Empty;
                FileIO.FileInfo.fileName = string.Empty;
                FileIO.FileInfo.fileConvertedPath = RAGLINKCommons.RPlatform.SettingsContent.tempFilePath + "\\" + tempFileGUID;
            }
            catch (Exception) { };
        }
        private void SaveFileHotKey(int HotKeyID)
        {
            if (HotKeyID == HotkeySaveFile)
            {
                SaveFile(false);
            }
        }
        private string SelectPath(string dialogTitle)
        {
            string retValue = string.Empty;
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "RAGLINK 行车计划文件|*" + RAGLINKCommons.RPlatform.SettingsContent.projectFileExtName + "|"
                            + "RAGLINK 通用扩展文件|*" + RAGLINKCommons.RPlatform.SettingsContent.universalFileExtName,
                    FilterIndex = FileIO.FileInfo.fileType == RAGLINKCommons.RPlatform.SettingsContent.FileType.PROJECT ? 1 : 2,
                    RestoreDirectory = true,
                    CheckPathExists = true,
                    Title = dialogTitle
                };
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    retValue = saveDialog.FileName;
                }
            }
            catch (Exception) { };
            return retValue;
        }
        private void SaveFile(bool inClosingFile)
        {
            try
            {
                bool newFile = false;
                toolStripLabelMain.Text = "正在保存文件...";
                if (FileIO.FileInfo.isFileEdited)
                {
                    if (FileIO.FileInfo.filePath == string.Empty)
                    {
                        newFile = true;
                        string savePath = SelectPath("保存,..");
                        if (savePath != string.Empty)
                        {
                            FileIO.FileInfo.filePath = savePath;
                            SaveFileByPath(FileIO.FileInfo.filePath, inClosingFile);
                        }
                        else
                        {
                            goto endSave;
                        }
                    }
                    else
                    {
                        SaveFileByPath(FileIO.FileInfo.filePath, inClosingFile);
                    }
                }
                //Read file meta data start
                RAGLINKCommons.RPlatform.SettingsFileIO settingsFileIO = new RAGLINKCommons.RPlatform.SettingsFileIO();
                settingsFileIO.SetSettingsFilePath(FileIO.FileInfo.fileConvertedPath);
                FileIO.FileInfo.fileType = settingsFileIO.GetFileType();
                settingsFileIO.Dispose();
                //Read file meta data end
                if (FileIO.FileInfo.fileType != RAGLINKCommons.RPlatform.SettingsContent.FileType.UNKNOW)
                {
                    this.Text = "RAGLINK 文件编辑器 - " + "[" + FileIO.FileInfo.fileType.ToString() + "]" + Path.GetFileName(FileIO.FileInfo.filePath);
                    FileIO.FileInfo.isFileEdited = false;
                }
                else
                {
                    if (newFile)
                    {
                        FileIO.FileInfo.fileName = string.Empty;
                        FileIO.FileInfo.filePath = string.Empty;
                    }
                }
            endSave:
                toolStripLabelMain.Text = "就绪.";
            }
            catch (Exception) { };
        }
        private bool SaveFileByPath(string filePath, bool inClosingFile)
        {
            bool retValue = false;
            try
            {
                if (FileIO.FileInfo.fileConvertedPath == string.Empty)
                {
                    retValue = false;
                    return retValue;
                }
                textBoxEditor.SaveToFile(FileIO.FileInfo.fileConvertedPath, Encoding.Default);
                //Read file meta data start
                RAGLINKCommons.RPlatform.SettingsFileIO settingsFileIO = new RAGLINKCommons.RPlatform.SettingsFileIO();
                settingsFileIO.SetSettingsFilePath(FileIO.FileInfo.fileConvertedPath);
                FileIO.FileInfo.fileType = settingsFileIO.GetFileType();
                settingsFileIO.Dispose();
                //Read file meta data end
                //MessageBox.Show(FileIO.FileInfo.fileType.ToString());
                if (FileIO.FileInfo.fileType != RAGLINKCommons.RPlatform.SettingsContent.FileType.UNKNOW)
                {
                    UnlockFile();
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    RAGLINKCommons.RPlatform.FileEncryption.DesEncryptFile(FileIO.FileInfo.fileConvertedPath, filePath, RAGLINKCommons.RPlatform.SettingsContent.encryptKey);
                    //MessageBox.Show(FileIO.FileInfo.fileConvertedPath);
                    if (inClosingFile)
                    {
                        File.Delete(FileIO.FileInfo.fileConvertedPath);
                    }

                    this.Text = "RAGLINK 文件编辑器 - " + "[" + FileIO.FileInfo.fileType.ToString() + "]" + Path.GetFileName(FileIO.FileInfo.filePath);
                }
                else
                {
                    MessageBox.Show("保存失败：文件头信息丢失。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception) { };
            if (!inClosingFile)
            {
                LockFile(filePath);
            }

            return retValue;
        }

        static public FileStream fileStream;
        private void LockFile(string filePath)
        {
            try
            {
                UnlockFile();
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }
            catch (Exception) { };
            return;
        }
        private void UnlockFile()
        {
            try
            {
                fileStream.Close();
            }
            catch (Exception) { };
            return;
        }
    }
}
