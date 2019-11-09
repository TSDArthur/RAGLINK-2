using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RAGLINKCommons.RPlatform
{
    public class FileEncryption
    {
        public static string DesEncrypt(string strText, string strEncrKey)
        {
            string retValue = string.Empty;
            try
            {
                byte[] byKey = null;
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                retValue = Convert.ToBase64String(ms.ToArray());
                return retValue;
            }
            catch (Exception) { };
            return retValue;
        }
        public static string DesDecrypt(string strText, string sDecrKey)
        {
            string retValue = string.Empty;
            try
            {
                byte[] byKey = null;
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                byte[] inputByteArray = new Byte[strText.Length];
                byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = new UTF8Encoding();
                retValue = encoding.GetString(ms.ToArray());
                return retValue;
            }
            catch (Exception) { };
            return retValue;
        }
        public static bool DesEncryptFile(string m_InFilePath, string m_OutFilePath, string strEncrKey)
        {
            bool retValue = false;
            try
            {
                byte[] byKey = null;
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                FileStream fin = new FileStream(m_InFilePath, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(m_OutFilePath, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);
                byte[] bin = new byte[100];
                long rdlen = 0;
                long totlen = fin.Length;
                int len;
                DES des = new DESCryptoServiceProvider();
                CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, 100);
                    encStream.Write(bin, 0, len);
                    rdlen = rdlen + len;
                }
                encStream.Close();
                fout.Close();
                fin.Close();
                retValue = true;
                return retValue;
            }
            catch (Exception) { };
            return retValue;
        }
        public static bool DesDecryptFile(string m_InFilePath, string m_OutFilePath, string sDecrKey)
        {
            bool retValue = false;
            try
            {
                byte[] byKey = null;
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                FileStream fin = new FileStream(m_InFilePath, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(m_OutFilePath, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);
                byte[] bin = new byte[100];
                long rdlen = 0;
                long totlen = fin.Length;
                int len;
                DES des = new DESCryptoServiceProvider();
                CryptoStream encStream = new CryptoStream(fout, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, 100);
                    encStream.Write(bin, 0, len);
                    rdlen = rdlen + len;
                }
                encStream.Close();
                fout.Close();
                fin.Close();
                retValue = true;
            }
            catch (Exception) { };
            return retValue;
        }
        static public string GUIDGenerate()
        {
            string retValue = string.Empty;
            try
            {
                retValue = Guid.NewGuid().ToString();
            }
            catch (Exception) { };
            return retValue;
        }
    }
}
