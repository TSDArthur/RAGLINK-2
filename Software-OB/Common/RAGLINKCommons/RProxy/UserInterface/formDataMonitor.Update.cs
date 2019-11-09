using System;
using System.Windows.Forms;

namespace RAGLINKCommons.RProxy
{
    public partial class FormDataMonitor : Form
    {
        static public int HMIDataIndex = -1;
        private void ListViewFirstUpdate()
        {
            try
            {
                listViewDataMonitor.BeginUpdate();
                listViewDataMonitor.Items.Clear();
                ListViewGroup listViewGroupNormal = new ListViewGroup();
                listViewGroupNormal.Header = "常规数据";
                listViewDataMonitor.Groups.Add(listViewGroupNormal);
                for (int i = 0; i < RPlatform.DataManager.processData.GetTrainDataCount(); i++)
                {
                    if (i == (int)RPlatform.DataManager.TrainDataMap.HMI_INSTRUCTIONS)
                    {
                        HMIDataIndex = i;
                        continue;
                    }
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Text = i.ToString();
                    listViewItem.SubItems.Add(Enum.GetName(typeof(RPlatform.DataManager.TrainDataMap), i));
                    listViewItem.SubItems.Add(RPlatform.DataManager.processData.trainDataType[i].ToString());
                    listViewItem.SubItems.Add(RPlatform.DataManager.processData.trainDataClassify[i].ToString());
                    listViewItem.SubItems.Add(RPlatform.DataManager.processData.trainData[i].ToString());
                    listViewDataMonitor.Groups[0].Items.Add(listViewItem);
                    listViewDataMonitor.Items.Add(listViewItem);
                }
                if (HMIDataIndex != -1)
                {
                    ListViewGroup listViewGroupHMI = new ListViewGroup();
                    listViewGroupHMI.Header = "HMI数据";
                    listViewDataMonitor.Groups.Add(listViewGroupHMI);
                    for (int i = 0; i < ((RPlatform.DataManager.HMIInstructions)RPlatform.DataManager.processData.trainData[HMIDataIndex]).GetDataCount(); i++)
                    {
                        ListViewItem listViewItem = new ListViewItem();
                        listViewItem.Text = i.ToString();
                        listViewItem.SubItems.Add(((RPlatform.DataManager.HMIInstructions)RPlatform.DataManager.processData.trainData[HMIDataIndex]).HMIDataMap[i].ToString());
                        listViewItem.SubItems.Add(RPlatform.DataManager.processData.trainDataType[HMIDataIndex].ToString());
                        listViewItem.SubItems.Add(RPlatform.DataManager.processData.trainDataClassify[HMIDataIndex].ToString());
                        listViewItem.SubItems.Add(((RPlatform.DataManager.HMIInstructions)RPlatform.DataManager.processData.trainData[HMIDataIndex]).HMIData[i]);
                        listViewDataMonitor.Groups[1].Items.Add(listViewItem);
                        listViewDataMonitor.Items.Add(listViewItem);
                    }
                }
                listViewDataMonitor.EndUpdate();
            }
            catch (Exception) { };
        }
        private void UpdateDataMonitor()
        {
            try
            {
                //listViewDataMonitor.BeginUpdate();
                for (int i = 0; i < listViewDataMonitor.Groups[0].Items.Count; i++)
                {
                    int dataID = Int32.Parse(listViewDataMonitor.Groups[0].Items[i].Text);
                    if (listViewDataMonitor.Groups[0].Items[i].SubItems[4].Text != RPlatform.DataManager.processData.trainData[dataID].ToString())
                    {
                        listViewDataMonitor.Groups[0].Items[i].SubItems[4].Text = RPlatform.DataManager.processData.trainData[dataID].ToString();
                    }
                }
                if (HMIDataIndex != -1)
                {
                    for (int i = 0; i < listViewDataMonitor.Groups[1].Items.Count; i++)
                    {
                        int dataID = Int32.Parse(listViewDataMonitor.Groups[1].Items[i].Text);
                        if (listViewDataMonitor.Groups[1].Items[i].SubItems[4].Text != ((RPlatform.DataManager.HMIInstructions)RPlatform.DataManager.processData.trainData[HMIDataIndex]).HMIData[dataID])
                        {
                            listViewDataMonitor.Groups[1].Items[i].SubItems[4].Text = ((RPlatform.DataManager.HMIInstructions)RPlatform.DataManager.processData.trainData[HMIDataIndex]).HMIData[dataID];
                        }
                    }
                }
                //listViewDataMonitor.EndUpdate();
            }
            catch (Exception) { };
        }
    }
}
