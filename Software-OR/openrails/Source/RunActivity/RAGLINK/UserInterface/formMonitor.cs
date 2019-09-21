using Orts.RAGLINKProxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orts.RAGLINK.UserInterface
{
    public partial class formMonitor : Form
    {
        public formMonitor()
        {
            InitializeComponent();
        }

        private void FormMonitor_Load(object sender, EventArgs e)
        {
            this.Left = 1;
        }

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                labelSpeed.Text = TrainMethods.GetCurrentTrainSpeed().ToString();
            }
            catch (Exception) { };
        }
    }
}
