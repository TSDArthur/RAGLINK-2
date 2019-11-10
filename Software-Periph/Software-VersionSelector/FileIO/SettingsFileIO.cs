using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RStarter.FileIO
{
    public class SettingsFileIO
    {
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);
        private string sPath = null;
        public void SetSettingsFilePath(string path)
        {
            this.sPath = path;
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
    }
}
