using System;
using System.Collections.Generic;
using System.IO;

namespace RAGLINKCommons.RPlatform
{
    public class SettingsFileIO
    {
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);
        private string sPath = null;
        private string originalPath = null;
        private string tempFileGUID = null;
        public bool fileEncrypt = false;
        public void CheckFileEncrypt()
        {
            if (ReadValue("file_summary", "encrypt") == "0" || Path.GetExtension(this.sPath) == ".tmp")
            {
                fileEncrypt = false;
            }
            else
            {
                fileEncrypt = true;
            }
            //MessageBox.Show(fileEncrypt.ToString());
        }
        public void SetSettingsFilePath(string path)
        {
            this.sPath = path;
            CheckFileEncrypt();
            if (fileEncrypt)
            {
                originalPath = path;
                tempFileGUID = FileEncryption.GUIDGenerate() + ".tmp";
                FileEncryption.DesDecryptFile(originalPath, SettingsContent.tempFilePath + "\\" + tempFileGUID, SettingsContent.encryptKey);
                this.sPath = SettingsContent.tempFilePath + "\\" + tempFileGUID;
            }
        }
        public SettingsContent.FileType GetFileType()
        {
            SettingsContent.FileType retValue = SettingsContent.FileType.UNKNOW;
            try
            {
                string dataValue = ReadValue("file_summary", "filetype");
                if (dataValue == string.Empty || Convert.ToInt32(dataValue) > (int)SettingsContent.FileType.UNKNOW)
                {
                    return retValue;
                }

                retValue = (SettingsContent.FileType)Convert.ToInt32(dataValue);
                return retValue;
            }
            catch (Exception) { };
            return retValue;
        }
        public void Dispose()
        {
            if (fileEncrypt)
            {
                try
                {
                    File.Delete(originalPath);
                }
                catch (Exception) { };
                try
                {
                    FileEncryption.DesEncryptFile(SettingsContent.tempFilePath + "\\" + tempFileGUID, originalPath, SettingsContent.encryptKey);
                }
                catch (Exception) { };
                try
                {
                    File.Delete(SettingsContent.tempFilePath + "\\" + tempFileGUID);
                }
                catch (Exception) { };
            }
            fileEncrypt = false;
            this.sPath = null;
        }
        public void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, sPath);
        }
        public string ReadValue(string section, string key)
        {
            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, sPath);
            return temp.ToString();
        }
        public List<int> ReadVectorValue(string section, string key)
        {
            string vectorStr = ReadValue(section, key);
            List<int> vector = new List<int>(Array.ConvertAll<string, int>(vectorStr.Split(','), s => Int32.Parse(s)));
            return vector;
        }
        public void WriteVectorValue(string section, string key, List<int> value)
        {
            string vectorStr = string.Empty;
            for (int i = 0; i < value.Count - 1; i++)
            {
                vectorStr += value[i].ToString() + ',';
            }

            vectorStr += value[value.Count - 1].ToString();
            WriteValue(section, key, vectorStr);
        }
        public bool CreateNewFile(SettingsContent.FileType fileType, string fileName)
        {
            bool retValue = false;
            try
            {
                string fileExtName = fileType == SettingsContent.FileType.PROJECT ? SettingsContent.projectFileExtName : SettingsContent.universalFileExtName;
                string fileFullPath = Path.GetFullPath(fileName + fileExtName);
                string tempFilePath = Path.GetFullPath(SettingsContent.tempFilePath + "\\" + FileEncryption.GUIDGenerate() + ".tmp");
                if (File.Exists(fileFullPath))
                {
                    File.Delete(fileFullPath);
                }

                File.AppendAllText(tempFilePath, string.Empty);
                if (File.Exists(tempFilePath) && !File.Exists(fileFullPath))
                {
                    SetSettingsFilePath(tempFilePath);
                    WriteValue("file_summary", "encrypt", "1");
                    WriteValue("file_summary", "filetype", ((int)fileType).ToString());
                    Dispose();
                    FileEncryption.DesEncryptFile(tempFilePath, fileFullPath, SettingsContent.encryptKey);
                    retValue = true;
                }
            }
            catch (Exception) { };
            return retValue;
        }
    }
}
