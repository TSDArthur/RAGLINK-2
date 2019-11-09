using RAGLINKCommons.RProxy;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace RAGLINKCommons.RStatistics.UserInterface
{
    public partial class FromSAGAT : Form
    {
        public FromSAGAT()
        {
            InitializeComponent();
        }

        private SAGATManager.QAManagerData.QuestionAnswer questionAnswer = new SAGATManager.QAManagerData.QuestionAnswer();
        private int QASubItemID = 0;
        private bool trigToClose = false;

        private void RegisterEvents()
        {
            SAGATManager.CloseQAFormEvent += CloseEvent;
            SAGATManager.ShowQAFormEvent += ShowQAFormEvent;
            SAGATManager.ShowNextQuestionEvent += ShowNextQuestionEvent;
        }

        SAGATManager.QAManagerData.QuestionAnswer CloseEvent()
        {
            trigToClose = true;
            this.Close();
            questionAnswer.isDisplayed = true;
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "HH:mm:ss";
            questionAnswer.pauseTime = Convert.ToDateTime(RPlatform.DataManager.GetTrainData((int)RPlatform.DataManager.TrainDataMap.CURRENT_TIME).ToString(), dtFormat);
            return questionAnswer;
        }

        void ShowQAFormEvent(SAGATManager.QAManagerData.QuestionAnswer currentQuestionAnswer, int currentQASubItemID)
        {
            questionAnswer = currentQuestionAnswer;
            QASubItemID = currentQASubItemID;
        }

        void ShowNextQuestionEvent(int currentQASubItemID)
        {
            QASubItemID = currentQASubItemID;
        }

        void ModifyPosition()
        {
            Point simulatorWindowPosition = new Point(0, 0);
            simulatorWindowPosition = SimCoreClient.GetSimulatorWindowPosition("RAGLINK+");
            Point setWindowPosition = new Point(simulatorWindowPosition.X + (RPlatform.GraphicOptionsManager.graphicOptionsValue.simulatorWidth - this.Width) / 2,
                simulatorWindowPosition.Y + (RPlatform.GraphicOptionsManager.graphicOptionsValue.simulatorHeight - this.Height) / 2 + 30);
            this.Location = setWindowPosition;
            this.TopMost = true;
        }

        private void formQA_Load(object sender, EventArgs e)
        {
            RegisterEvents();
            ModifyPosition();
        }

        private void formQA_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !trigToClose;
        }
    }
}
