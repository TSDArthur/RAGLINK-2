using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace RAGLINKCommons.RStatistics
{
    public class SAGATManager
    {
        public class QAManagerData
        {
            public enum QuestionMode
            {
                TEXT_INPUT = 0,
                SELECT = 1
            };

            public enum TextboxType
            {
                NUMBER = 0,
                STRING = 1
            };

            public class QuestionAnswer
            {
                public class QuestionAnswerItem
                {
                    public bool isAnswered;
                    public int questionTimer;
                    public int answerTime;
                    public string questionTitle;
                    public string questionDesc;
                    public QuestionMode questionMode;
                    public TextboxType textboxType;
                    public string answerText;
                    public int selectionCount;
                    public List<string> selectionText;
                    public int answerSelectID;
                    public string submitButtonText;
                    public string correctValue;

                    public QuestionAnswerItem()
                    {
                        isAnswered = false;
                        questionTimer = 0;
                        answerTime = 0;
                        questionTitle = string.Empty;
                        questionDesc = string.Empty;
                        questionMode = QuestionMode.TEXT_INPUT;
                        textboxType = TextboxType.STRING;
                        answerText = string.Empty;
                        selectionCount = 0;
                        selectionText = new List<string>();
                        submitButtonText = string.Empty;
                        correctValue = string.Empty;
                        answerSelectID = -1;
                    }
                }

                public bool isDisplayed;
                public double pauseDistance;
                public DateTime pauseTime;
                public int questionCount;
                public List<QuestionAnswerItem> questionAnswerItems;

                public QuestionAnswer()
                {
                    isDisplayed = false;
                    pauseDistance = 0;
                    questionCount = 0;
                    pauseTime = DateTime.MinValue;
                    questionAnswerItems = new List<QuestionAnswerItem>();
                }
            };

            public bool questionAnswerEnable;
            public int questionAnswerCount;
            public string questionAnswerSavePath;
            public List<QuestionAnswer> questionAnswers;

            public QAManagerData()
            {
                questionAnswerEnable = false;
                questionAnswerCount = 0;
                questionAnswerSavePath = string.Empty;
                questionAnswers = new List<QuestionAnswer>();
            }

            public void Clear()
            {
                questionAnswerEnable = false;
                questionAnswerCount = 0;
                questionAnswerSavePath = string.Empty;
                questionAnswers.Clear();
            }
        };

        public enum TimerMode
        {
            WAIT_PAUSE = 0,
            WAIT_ANSWER = 1
        };

        static public TimerMode timerMode = TimerMode.WAIT_ANSWER;
        static public double currentTrackDistanceFromStart = 0;
        static public int currentSecondWaitAnswer = 0;
        static public int currentQAID = -1;
        static public int currentQASubItemID = -1;
        static public int currentAnsweredCount = 0;
        static public QAManagerData.QuestionAnswer currentQuestionAnswer = new QAManagerData.QuestionAnswer();

        public delegate void ShowQAFormHandler(QAManagerData.QuestionAnswer currentQuestionAnswer, int currentQASubItemID);
        static public event ShowQAFormHandler ShowQAFormEvent;
        public delegate QAManagerData.QuestionAnswer CloseQAFormHandler();
        static public event CloseQAFormHandler CloseQAFormEvent;
        public delegate void ShowNextQuestionHandler(int currentQASubItemID);
        static public event ShowNextQuestionHandler ShowNextQuestionEvent;

        static private int triggerClientID = -1;
        static private RProxy.SimWorldTrigger.TriggerClient TriggerClient = new RProxy.SimWorldTrigger.TriggerClient();

        static public void AttachToTrigger()
        {
            RProxy.SimWorldTrigger.DeleteTriggerClienr(TriggerClient);
            TriggerClient = new RProxy.SimWorldTrigger.TriggerClient(100, TriggerEvent);
            triggerClientID = RProxy.SimWorldTrigger.AddTriggerClient(TriggerClient);
        }

        static public void EnableTriggerClient()
        {
            ResetSAGATManager();
            if (RPlatform.ProjectsManager.projectInfo.QAData.questionAnswerEnable)
            {
                RProxy.SimWorldTrigger.SetClientState(TriggerClient, false);
            }
        }

        static public void TriggerEvent(double currentTimesCount, double currentTrackDistance)
        {
            if (timerMode == TimerMode.WAIT_PAUSE)
            {
                currentTrackDistanceFromStart = currentTrackDistance;
                CheckWaitPause();
            }
            else if (timerMode == TimerMode.WAIT_ANSWER)
            {
                currentSecondWaitAnswer++;
                CheckWaitAnswer();
            }
        }

        static public void ResetSAGATManager()
        {
            currentTrackDistanceFromStart = 0;
            currentSecondWaitAnswer = 0;
            timerMode = TimerMode.WAIT_PAUSE;
        }

        static public void PauseWaitPauseTimer()
        {
            timerMode = TimerMode.WAIT_ANSWER;
            RProxy.SimCoreClient.PauseSimulator(true);
            currentSecondWaitAnswer = 0;
        }

        static public void ContinueWaitPauseTimer()
        {
            timerMode = TimerMode.WAIT_PAUSE;
            RProxy.SimCoreClient.PauseSimulator(false);
            currentSecondWaitAnswer = 0;
        }

        static public void CheckWaitPause()
        {
            if (timerMode != TimerMode.WAIT_PAUSE)
            {
                return;
            }

            foreach (QAManagerData.QuestionAnswer questionAnswer in RPlatform.ProjectsManager.projectInfo.QAData.questionAnswers)
            {
                if (!questionAnswer.isDisplayed && currentTrackDistanceFromStart >= questionAnswer.pauseDistance)
                {
                    currentQAID = RPlatform.ProjectsManager.projectInfo.QAData.questionAnswers.IndexOf(questionAnswer);
                    RPlatform.ProjectsManager.projectInfo.QAData.questionAnswers[currentQAID].isDisplayed = true;
                    PauseWaitPauseTimer();
                    //DoEvents
                    if (questionAnswer.questionCount > 0)
                    {
                        currentQuestionAnswer = questionAnswer;
                        currentQASubItemID = 0;
                        new Thread((ThreadStart)delegate
                        {
                            Application.Run(new UserInterface.FromSAGAT());
                        }).Start();
                        ShowQAFormEvent(currentQuestionAnswer, currentQASubItemID);
                    }
                    else
                    {
                        ContinueWaitPauseTimer();
                        return;
                    }
                    break;
                }
            }
        }

        static public void CheckWaitAnswer()
        {
            if (timerMode != TimerMode.WAIT_ANSWER)
            {
                return;
            }

            if (currentQASubItemID != -1 && currentSecondWaitAnswer / 10 >= currentQuestionAnswer.questionAnswerItems[currentQASubItemID].questionTimer)
            {
                if (currentQASubItemID >= currentQuestionAnswer.questionCount - 1)
                {
                    currentAnsweredCount++;
                    SaveAnswer(CloseQAFormEvent());
                    ContinueWaitPauseTimer();
                    currentQASubItemID = -1;
                    currentQuestionAnswer = new QAManagerData.QuestionAnswer();
                    return;
                }
                else
                {
                    //DoEvents
                    currentQASubItemID++;
                    currentSecondWaitAnswer = 0;
                    ShowNextQuestionEvent(currentQASubItemID);
                }
            }
        }

        static public int GetCurrentWaitAnswerTime()
        {
            int retValue = 0;
            retValue = currentSecondWaitAnswer / 10;
            return retValue;
        }

        static public int GetQACount()
        {
            int retValue = 0;
            try
            {
                retValue = RPlatform.ProjectsManager.projectInfo.QAData.questionAnswerCount;
            }
            catch (Exception) { };
            return retValue;
        }

        static public TimerMode GetCurrentTimerMode()
        {
            TimerMode retValue = TimerMode.WAIT_PAUSE;
            try
            {
                retValue = timerMode;
            }
            catch (Exception) { };
            return retValue;
        }

        static public int GetAnsweredCount()
        {
            int retValue = 0;
            try
            {
                retValue = currentAnsweredCount;
            }
            catch (Exception) { };
            return retValue;
        }

        static public QAManagerData.QuestionAnswer GetNextQuestion()
        {
            QAManagerData.QuestionAnswer retValue = new QAManagerData.QuestionAnswer();
            try
            {
                if (timerMode == TimerMode.WAIT_PAUSE)
                {
                    double distanceToQuestion = Double.MaxValue;
                    foreach (QAManagerData.QuestionAnswer questionAnswer in RPlatform.ProjectsManager.projectInfo.QAData.questionAnswers)
                    {
                        if (!questionAnswer.isDisplayed && questionAnswer.pauseDistance - currentTrackDistanceFromStart >= 0 &&
                            distanceToQuestion > questionAnswer.pauseDistance - currentTrackDistanceFromStart)
                        {
                            distanceToQuestion = currentTrackDistanceFromStart - questionAnswer.pauseDistance;
                            retValue = questionAnswer;
                        }
                    }
                }
            }
            catch (Exception) { };
            return retValue;
        }

        static public QAManagerData.QuestionAnswer GetCurrentQuestion()
        {
            QAManagerData.QuestionAnswer retValue = new QAManagerData.QuestionAnswer();
            try
            {
                if (timerMode == TimerMode.WAIT_ANSWER)
                {
                    retValue = currentQuestionAnswer;
                }
            }
            catch (Exception) { };
            return retValue;
        }

        static public QAManagerData.QuestionAnswer.QuestionAnswerItem GetCurrentQuestionItem()
        {
            QAManagerData.QuestionAnswer.QuestionAnswerItem retValue = new QAManagerData.QuestionAnswer.QuestionAnswerItem();
            try
            {
                if (timerMode == TimerMode.WAIT_ANSWER)
                {
                    retValue = currentQuestionAnswer.questionAnswerItems[currentQASubItemID];
                }
            }
            catch (Exception) { };
            return retValue;
        }

        static public void WriteExcel(ExcelPackage package)
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Answer" + currentQAID.ToString());
            package.Save();
        }

        static public void SaveAnswer(QAManagerData.QuestionAnswer questionAnswer)
        {
            RPlatform.ProjectsManager.projectInfo.QAData.questionAnswers[currentQAID] = questionAnswer;
            string timeStamp = RProxy.SimCoreClient.GetSimulatorLaunchTimeString('_');
            string fileName = "SAGAT_" + timeStamp + ".xlsx";
            string fileFullPath = RPlatform.ProjectsManager.projectInfo.QAData.questionAnswerSavePath;
            string fileFullName = Path.GetFullPath(fileFullPath + "\\" + fileName);
            //Create file
            try
            {
                FileInfo fileInfo = new FileInfo(fileFullName);
                if (!fileInfo.Exists)
                {
                    Directory.CreateDirectory(fileFullPath);
                    using (ExcelPackage package = new ExcelPackage(new FileStream(fileFullName, FileMode.Create)))
                    {
                        WriteExcel(package);
                    }
                    fileInfo = new FileInfo(fileFullName);
                }
                else
                {
                    using (ExcelPackage package = new ExcelPackage(fileInfo))
                    {
                        WriteExcel(package);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("SAGAT系统错误。\n详情：导出报表文件失败！", "SAGAT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }
    }
}
