using System.Collections.Generic;

namespace RAGLINKFileEditor.FileIO
{
    static public class FileInfo
    {
        static public bool isFileOpened = false;
        static public bool isFileEdited = false;
        static public string filePath = string.Empty;
        static public string fileName = string.Empty;
        static public string fileConvertedPath = string.Empty;
        static public RAGLINKCommons.RAGLINKPlatform.SettingsContent.FileType fileType;
        static public List<string> argsFileNameList = new List<string>();
    }
}
