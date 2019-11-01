using System;
using System.Collections.Generic;
using System.IO;

namespace RAGLINKCommons.RAGLINKPlatform
{
    public class BoardsManager
    {
        public class BoardList
        {
            public int boardCount;
            public List<int> boardID;
            public List<string> boardName;
            public List<string> boardFilePath;
            public BoardList()
            {
                boardID = new List<int>();
                boardName = new List<string>();
                boardFilePath = new List<string>();
            }
        };
        public class BoardInfo
        {
            public bool boardFileLoaded;
            public string boardFilePath;
            //Area board_info
            public int boardID;
            public string boardName;
            public string boardSupplier;
            public string boardDescribe;
            public string boardWebsite;
            public Version boardSupprtVersion;
            public int boardIOCount;
            public List<int> boardAnalogInput;
            public List<int> boardAnalogOutput;
            public List<int> boardDigitalIO;
            //Area board_init
            public int DEVICE_WITHOUT_INIT;
            public int DEVICE_BUTTON_AUTOLOCK;
            public int DEVICE_BUTTON_AUTORESET;
            public int DEVICE_DIGITAL_INPUT;
            public int DEVICE_DIGITAL_OUTPUT;
            public int DEVICE_ANALOG_INPUT;
            public int DEVICE_ANALOG_OUTPUT;
            //Area communication
            public int boardBaud;
            //Area protocol
            public int COMM_START_SYMBOL;
            public int COMM_SPEC_SYMBOL;
            public int COMM_SET_SYMBOL;
            public int COMM_END_SYMBOL;
            public int COMM_RESET_SPEC_NUM;
            public int COMM_CONNECT_SPEC_NUM;
            public int COMM_TRANSEND_SPEC_NUM;
            public int COMM_CONTINUE_SPEC_NUM;
            public int COMM_CLEAR_DEVICES_STATE_NUM;
            public int COMM_PACK_MAX_SIZE;
            public BoardInfo()
            {
                boardFileLoaded = false;
                boardAnalogInput = new List<int>();
                boardAnalogOutput = new List<int>();
                boardDigitalIO = new List<int>();
            }
        };
        static public BoardList boardList = new BoardList();
        static public BoardInfo boardInfo = new BoardInfo();
        static private string boardInfoSection = "board_info";
        static private string boardInitSection = "board_init";
        static private string boardCommunicationSection = "communication";
        static private string boardProtocol = "protocol";
        static public void ResetBoardManager()
        {
            boardList = new BoardList();
            boardInfo = new BoardInfo();
        }
        static public bool UpdateBoardItems()
        {
            bool retValue = false;
            try
            {
                //Clear boardList
                boardList.boardCount = 0;
                boardList.boardFilePath.Clear();
                boardList.boardID.Clear();
                boardList.boardName.Clear();
                //Update
                SettingsContent.UpdateSettingsPath();
                DirectoryInfo boardPath = new DirectoryInfo(SettingsContent.boardPath);
                FileInfo[] boardFiles = boardPath.GetFiles();
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                foreach (FileInfo fileName in boardFiles)
                {
                    if (fileName.Extension == SettingsContent.universalFileExtName)
                    {
                        settingsFileIO.SetSettingsFilePath(fileName.FullName);
                        if (settingsFileIO.GetFileType() == SettingsContent.FileType.BOARD)
                        {
                            boardList.boardCount++;
                            boardList.boardFilePath.Add(fileName.FullName);
                            boardList.boardID.Add(Int32.Parse(settingsFileIO.ReadValue(boardInfoSection, "board_id")));
                            boardList.boardName.Add(settingsFileIO.ReadValue(boardInfoSection, "board_name"));
                        }
                        settingsFileIO.Dispose();
                    }
                }
                retValue = true;
            }
            catch (Exception) { };
            return retValue;
        }
        static public BoardInfo ReadBoardFileInfo(string boardFilePath)
        {
            BoardInfo boardFileInfo = new BoardInfo();
            try
            {
                boardFileInfo.boardFileLoaded = true;
                boardFileInfo.boardFilePath = boardFilePath;
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                settingsFileIO.SetSettingsFilePath(boardFilePath);
                //Load data
                boardFileInfo.boardID = Int32.Parse(settingsFileIO.ReadValue(boardInfoSection, "board_id"));
                boardFileInfo.boardName = settingsFileIO.ReadValue(boardInfoSection, "board_name");
                boardFileInfo.boardSupplier = settingsFileIO.ReadValue(boardInfoSection, "board_supplier");
                boardFileInfo.boardDescribe = settingsFileIO.ReadValue(boardInfoSection, "board_describe");
                boardFileInfo.boardWebsite = settingsFileIO.ReadValue(boardInfoSection, "board_website");
                boardFileInfo.boardSupprtVersion = new Version(settingsFileIO.ReadValue(boardInfoSection, "board_support_version"));
                boardFileInfo.boardIOCount = Int32.Parse(settingsFileIO.ReadValue(boardInfoSection, "board_io_count"));
                boardFileInfo.boardAnalogInput = settingsFileIO.ReadVectorValue(boardInfoSection, "board_analog_input");
                boardFileInfo.boardAnalogOutput = settingsFileIO.ReadVectorValue(boardInfoSection, "board_analog_output");
                boardFileInfo.boardDigitalIO = settingsFileIO.ReadVectorValue(boardInfoSection, "board_digital_io");
                //Load board init
                boardFileInfo.DEVICE_WITHOUT_INIT = Int32.Parse(settingsFileIO.ReadValue(boardInitSection, "DEVICE_WITHOUT_INIT"));
                boardFileInfo.DEVICE_BUTTON_AUTOLOCK = Int32.Parse(settingsFileIO.ReadValue(boardInitSection, "DEVICE_BUTTON_AUTOLOCK"));
                boardFileInfo.DEVICE_BUTTON_AUTORESET = Int32.Parse(settingsFileIO.ReadValue(boardInitSection, "DEVICE_BUTTON_AUTORESET"));
                boardFileInfo.DEVICE_DIGITAL_INPUT = Int32.Parse(settingsFileIO.ReadValue(boardInitSection, "DEVICE_DIGITAL_INPUT"));
                boardFileInfo.DEVICE_DIGITAL_OUTPUT = Int32.Parse(settingsFileIO.ReadValue(boardInitSection, "DEVICE_DIGITAL_OUTPUT"));
                boardFileInfo.DEVICE_ANALOG_INPUT = Int32.Parse(settingsFileIO.ReadValue(boardInitSection, "DEVICE_ANALOG_INPUT"));
                boardFileInfo.DEVICE_ANALOG_OUTPUT = Int32.Parse(settingsFileIO.ReadValue(boardInitSection, "DEVICE_ANALOG_OUTPUT"));
                //Load communication
                boardFileInfo.boardBaud = Int32.Parse(settingsFileIO.ReadValue(boardCommunicationSection, "board_baud"));
                //Load procotol
                boardFileInfo.COMM_START_SYMBOL = Int32.Parse(settingsFileIO.ReadValue(boardProtocol, "COMM_START_SYMBOL"));
                boardFileInfo.COMM_SPEC_SYMBOL = Int32.Parse(settingsFileIO.ReadValue(boardProtocol, "COMM_SPEC_SYMBOL"));
                boardFileInfo.COMM_SET_SYMBOL = Int32.Parse(settingsFileIO.ReadValue(boardProtocol, "COMM_SET_SYMBOL"));
                boardFileInfo.COMM_END_SYMBOL = Int32.Parse(settingsFileIO.ReadValue(boardProtocol, "COMM_END_SYMBOL"));
                boardFileInfo.COMM_RESET_SPEC_NUM = Int32.Parse(settingsFileIO.ReadValue(boardProtocol, "COMM_RESET_SPEC_NUM"));
                boardFileInfo.COMM_CONNECT_SPEC_NUM = Int32.Parse(settingsFileIO.ReadValue(boardProtocol, "COMM_CONNECT_SPEC_NUM"));
                boardFileInfo.COMM_TRANSEND_SPEC_NUM = Int32.Parse(settingsFileIO.ReadValue(boardProtocol, "COMM_TRANSEND_SPEC_NUM"));
                boardFileInfo.COMM_CONTINUE_SPEC_NUM = Int32.Parse(settingsFileIO.ReadValue(boardProtocol, "COMM_CONTINUE_SPEC_NUM"));
                boardFileInfo.COMM_CLEAR_DEVICES_STATE_NUM = Int32.Parse(settingsFileIO.ReadValue(boardProtocol, "COMM_CLEAR_DEVICES_STATE_NUM"));
                boardFileInfo.COMM_PACK_MAX_SIZE = Int32.Parse(settingsFileIO.ReadValue(boardProtocol, "COMM_PACK_MAX_SIZE"));
                //Close file
                settingsFileIO.Dispose();
            }
            catch (Exception) { };
            return boardFileInfo;
        }
        static public bool LoadBoardFile(string boardFilePath)
        {
            bool retValue = false;
            try
            {
                boardInfo = ReadBoardFileInfo(boardFilePath);
                retValue = true;
            }
            catch (Exception) { };
            return retValue;
        }
        static public BoardInfo CreateNewBoardFile(string filePath, string fileName)
        {
            BoardInfo retValue = new BoardInfo();
            try
            {
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                if (settingsFileIO.CreateNewFile(SettingsContent.FileType.BOARD, filePath + "\\" + fileName))
                {
                    retValue.boardFileLoaded = true;
                    retValue.boardFilePath = Path.GetFullPath(filePath + "\\" + fileName + SettingsContent.universalFileExtName);
                }
            }
            catch (Exception) { };
            return retValue;
        }
        static public bool SaveBoardFile(BoardInfo currentBoardInfo)
        {
            bool retValue = false;
            try
            {
                if (!currentBoardInfo.boardFileLoaded) return retValue;
                SettingsFileIO settingsFileIO = new SettingsFileIO();
                settingsFileIO.SetSettingsFilePath(currentBoardInfo.boardFilePath);
                //Load data
                settingsFileIO.WriteValue(boardInfoSection, "board_id", currentBoardInfo.boardID.ToString());
                settingsFileIO.WriteValue(boardInfoSection, "board_name", currentBoardInfo.boardName.ToString());
                settingsFileIO.WriteValue(boardInfoSection, "board_supplier", currentBoardInfo.boardSupplier.ToString());
                settingsFileIO.WriteValue(boardInfoSection, "board_describe", currentBoardInfo.boardDescribe.ToString());
                settingsFileIO.WriteValue(boardInfoSection, "board_website", currentBoardInfo.boardWebsite.ToString());
                settingsFileIO.WriteValue(boardInfoSection, "board_support_version", currentBoardInfo.boardSupprtVersion.ToString());
                settingsFileIO.WriteValue(boardInfoSection, "board_io_count", currentBoardInfo.boardIOCount.ToString());
                settingsFileIO.WriteVectorValue(boardInfoSection, "board_analog_input", currentBoardInfo.boardAnalogInput);
                settingsFileIO.WriteVectorValue(boardInfoSection, "board_analog_output", currentBoardInfo.boardAnalogOutput);
                settingsFileIO.WriteVectorValue(boardInfoSection, "board_digital_io", currentBoardInfo.boardDigitalIO);
                //Load board init
                settingsFileIO.WriteValue(boardInitSection, "DEVICE_WITHOUT_INIT", currentBoardInfo.DEVICE_WITHOUT_INIT.ToString());
                settingsFileIO.WriteValue(boardInitSection, "DEVICE_BUTTON_AUTOLOCK", currentBoardInfo.DEVICE_BUTTON_AUTOLOCK.ToString());
                settingsFileIO.WriteValue(boardInitSection, "DEVICE_BUTTON_AUTORESET", currentBoardInfo.DEVICE_BUTTON_AUTORESET.ToString());
                settingsFileIO.WriteValue(boardInitSection, "DEVICE_DIGITAL_INPUT", currentBoardInfo.DEVICE_DIGITAL_INPUT.ToString());
                settingsFileIO.WriteValue(boardInitSection, "DEVICE_DIGITAL_OUTPUT", currentBoardInfo.DEVICE_DIGITAL_OUTPUT.ToString());
                settingsFileIO.WriteValue(boardInitSection, "DEVICE_ANALOG_INPUT", currentBoardInfo.DEVICE_ANALOG_INPUT.ToString());
                settingsFileIO.WriteValue(boardInitSection, "DEVICE_ANALOG_OUTPUT", currentBoardInfo.DEVICE_ANALOG_OUTPUT.ToString());
                //Load communication
                settingsFileIO.WriteValue(boardCommunicationSection, "board_baud", currentBoardInfo.boardBaud.ToString());
                //Load procotol
                settingsFileIO.WriteValue(boardProtocol, "COMM_START_SYMBOL", currentBoardInfo.COMM_START_SYMBOL.ToString());
                settingsFileIO.WriteValue(boardProtocol, "COMM_SPEC_SYMBOL", currentBoardInfo.COMM_SPEC_SYMBOL.ToString());
                settingsFileIO.WriteValue(boardProtocol, "COMM_SET_SYMBOL", currentBoardInfo.COMM_SET_SYMBOL.ToString());
                settingsFileIO.WriteValue(boardProtocol, "COMM_END_SYMBOL", currentBoardInfo.COMM_END_SYMBOL.ToString());
                settingsFileIO.WriteValue(boardProtocol, "COMM_RESET_SPEC_NUM", currentBoardInfo.COMM_RESET_SPEC_NUM.ToString());
                settingsFileIO.WriteValue(boardProtocol, "COMM_CONNECT_SPEC_NUM", currentBoardInfo.COMM_CONNECT_SPEC_NUM.ToString());
                settingsFileIO.WriteValue(boardProtocol, "COMM_TRANSEND_SPEC_NUM", currentBoardInfo.COMM_TRANSEND_SPEC_NUM.ToString());
                settingsFileIO.WriteValue(boardProtocol, "COMM_CONTINUE_SPEC_NUM", currentBoardInfo.COMM_CONTINUE_SPEC_NUM.ToString());
                settingsFileIO.WriteValue(boardProtocol, "COMM_CLEAR_DEVICES_STATE_NUM", currentBoardInfo.COMM_CLEAR_DEVICES_STATE_NUM.ToString());
                settingsFileIO.WriteValue(boardProtocol, "COMM_PACK_MAX_SIZE", currentBoardInfo.COMM_PACK_MAX_SIZE.ToString());
                settingsFileIO.Dispose();
            }
            catch (Exception) { };
            return retValue;
        }
    }
}
