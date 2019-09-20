using RAGLINKCommons;
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
            RAGLINKCommons.RAGLINKPlatform.SettingsContent.UpdateSettingsPath();
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
                        RAGLINKCommons.RAGLINKPlatform.SettingsFileIO settingsFileIO = new RAGLINKCommons.RAGLINKPlatform.SettingsFileIO();
                        settingsFileIO.SetSettingsFilePath(fileNameList[i]);
                        if ((int)settingsFileIO.GetFileType() < (int)RAGLINKCommons.RAGLINKPlatform.SettingsContent.FileType.UNKNOW && !settingsFileIO.fileEncrypt)
                        {
                            settingsFileIO.WriteValue("file_summary", "encrypt", "1");
                            string tempFilePath = RAGLINKCommons.RAGLINKPlatform.SettingsContent.tempFilePath + "\\" + RAGLINKCommons.RAGLINKPlatform.FileEncryption.GUIDGenerate() + ".tmp";
                            RAGLINKCommons.RAGLINKPlatform.FileEncryption.DesEncryptFile(fileNameList[i], tempFilePath, RAGLINKCommons.RAGLINKPlatform.SettingsContent.encryptKey);
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
