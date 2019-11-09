using System;
using System.Collections.Generic;
using System.IO;

namespace RAGLINKFileConvert
{
    class Program
    {
        static public List<string> fileNameList = new List<string>();
        static void Main(string[] args)
        {
            RAGLINKCommons.RPlatform.SettingsContent.UpdateSettingsPath();
            Console.Title = "RAGLINK File Convert";
            if (args.Length > 0)
            {
                fileNameList.Clear();
                fileNameList.AddRange(args);
                for (int i = 0; i < fileNameList.Count; i++)
                {
                    Console.Write("Processing: " + fileNameList[i] + "\n");
                    try
                    {
                        RAGLINKCommons.RPlatform.SettingsFileIO settingsFileIO = new RAGLINKCommons.RPlatform.SettingsFileIO();
                        settingsFileIO.SetSettingsFilePath(fileNameList[i]);
                        if ((int)settingsFileIO.GetFileType() < (int)RAGLINKCommons.RPlatform.SettingsContent.FileType.UNKNOW && !settingsFileIO.fileEncrypt)
                        {
                            settingsFileIO.WriteValue("file_summary", "encrypt", "1");
                            string tempFilePath = RAGLINKCommons.RPlatform.SettingsContent.tempFilePath + "\\" + RAGLINKCommons.RPlatform.FileEncryption.GUIDGenerate() + ".tmp";
                            RAGLINKCommons.RPlatform.FileEncryption.DesEncryptFile(fileNameList[i], tempFilePath, RAGLINKCommons.RPlatform.SettingsContent.encryptKey);
                            File.Delete(fileNameList[i]);
                            File.Copy(tempFilePath, fileNameList[i]);
                            File.Delete(tempFilePath);
                            Console.Write("Done.\n");
                        }
                        else
                        {
                            Console.Write("Type Error.\n");
                        }
                    }
                    catch (Exception)
                    {
                        Console.Write("Error.\n");
                    };
                }
                Console.ReadLine();
            }
        }
    }
}
