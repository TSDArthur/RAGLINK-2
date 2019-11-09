using System;
using System.Windows.Forms;

namespace RAGLINKFileEditor.UserInterface
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            FileIO.FileInfo.argsFileNameList.Clear();
            FileIO.FileInfo.argsFileNameList.AddRange(args);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
