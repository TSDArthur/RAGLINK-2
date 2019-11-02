using OpenBve.RAGLINKPlatform;
using OpenBveApi.Interface;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace OpenBve
{
	internal partial class formMain : Form
	{
		private int selectedPlanID = -1;
		private bool startButtonCondition = true;
		private RAGLINKCommons.RAGLINKPlatform.GraphicOptionsManager.GraphicOptionsValue optionsValueLoaded;
		private RAGLINKCommons.RAGLINKPlatform.GraphicOptionsManager.GraphicOptionsValue optionsValueCurrent;
		private void UpdatePlanFileList()
		{
			RAGLINKCommons.RAGLINKPlatform.ProjectsManager.UpdateProjectItem();
			listViewPlanFile.Items.Clear();
			this.listViewPlanFile.BeginUpdate();
			for (int i = 0; i < RAGLINKCommons.RAGLINKPlatform.ProjectsManager.projectList.projectCount; i++)
			{
				ListViewItem itemAdded = new ListViewItem();
				itemAdded.Text = RAGLINKCommons.RAGLINKPlatform.ProjectsManager.projectList.projectName[i] +
					(RAGLINKCommons.RAGLINKPlatform.ProjectsManager.projectList.projectDebug[i] ? "[调试模式]" : "[运转模式]");
				itemAdded.SubItems.Add(RAGLINKCommons.RAGLINKPlatform.ProjectsManager.projectList.projectID[i]);
				itemAdded.SubItems.Add(RAGLINKCommons.RAGLINKPlatform.ProjectsManager.projectList.projectDescribe[i]);
				listViewPlanFile.Items.Add(itemAdded);
			}
			this.listViewPlanFile.EndUpdate();
		}
		private int GetPlanListSelectPlanID(string dataValue)
		{
			int retValue = -1;
			try
			{
				retValue = RAGLINKCommons.RAGLINKPlatform.ProjectsManager.projectList.projectID.IndexOf(dataValue);
			}
			catch (Exception) { };
			return retValue;
		}
		private void StartArgsEvent(string[] Args)
		{
			try
			{
				for (int i = 1; i < Args.Length; i++)
				{
					if (Args[i] == RAGLINKCommons.RAGLINKPlatform.SettingsContent.simulatorUIMode)
					{
						this.Show();
						return;
					}
					else if (Args[i] == RAGLINKCommons.RAGLINKPlatform.SettingsContent.simualatorCompilerMode)
					{
						if (i >= Args.Length - 1) break;
						else
						{
							string projectFilePath = Args[i + 1];
							if (!File.Exists(projectFilePath)) break;
							int projectIndex = RAGLINKCommons.RAGLINKPlatform.ProjectsManager.projectList.projectFilePath.IndexOf(projectFilePath);
							string projectGUID = RAGLINKCommons.RAGLINKPlatform.ProjectsManager.projectList.projectID[projectIndex];
							if (projectGUID == string.Empty) break;
							LaunchProject(projectGUID);
							return;
						}
					}
				}
			}
			catch (Exception) { };
			this.Hide();
			MessageBox.Show("请使用正确命令行启动模拟器！\nUI模式：-ui\n编译器模式：-o %ProjectFileName%", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			formCloseTrig = true;
			System.Environment.Exit(0);
		}
		private void OnLoadEvents()
		{
			TrainMethods.EventsRegister();
			DataManagerClient.EventsRegister();
			RAGLINKCommons.RAGLINKPlatform.ProjectLoader.TrainDataLoaderEvent += LoadTrain;
			RAGLINKCommons.RAGLINKPlatform.ProjectLoader.RouteDataLoaderEvent += LoadRoute;
			RAGLINKCommons.RAGLINKPlatform.ProjectLoader.GraphicOptionsLoaderEvent += ApplyGraphicOptions;
			RAGLINKCommons.RAGLINKPlatform.ProjectLoader.StartSimulatorEvent += StartSimulator;
			UpdatePlanFileList();
			labelVersion.Text = "API 版本：" + RAGLINKCommons.RAGLINKPlatform.SettingsContent.simulatorVersion.ToString();
			RAGLINKCommons.RAGLINKPlatform.PackagesManager.UpdatePackageList();
		}
		private bool LoadRoute(string routeFile)
		{
			bool retValue = false;
			try
			{
				if (!File.Exists(routeFile)) return retValue;
				Result.RouteFile = routeFile;
				ShowRouteDelegate showRouteDelegate = new ShowRouteDelegate(ShowRoute);
				Invoke(showRouteDelegate, false);
				retValue = true;
			}
			catch (Exception) { };
			return retValue;
		}
		private bool LoadTrain(string trainFile)
		{
			bool retValue = false;
			try
			{
				if (!File.Exists(trainFile)) return retValue;
				Result.TrainFolder = Path.GetDirectoryName(trainFile);
				ShowTrainDelegate showTrainDelegate = new ShowTrainDelegate(ShowTrain);
				Invoke(showTrainDelegate, false);
				retValue = true;
			}
			catch (Exception) { };
			return retValue;
		}
		private void ReviewProject(int reviewMode, string projectGUID)
		{
			if (reviewMode == 0)
			{
				labelReviewLoading.Visible = true;
				labelReviewLoading.Text = "未选中行车计划，无预览。";
				labelOptionsLoading.Visible = true;
				labelOptionsLoading.Text = "未选中行车计划，无设置项。";
				labelRespackName.Visible = false;
				buttonStart.Enabled = false & startButtonCondition;
			}
			else if (reviewMode == 1)
			{
				RAGLINKCommons.RAGLINKPlatform.ProjectLoader.ReviewProjectResources(projectGUID);
				labelReviewLoading.Visible = true;
				labelReviewLoading.Text = "加载预览中...";
				labelOptionsLoading.Visible = true;
				labelOptionsLoading.Text = "加载设置项...";
			}
			else if (reviewMode == 2)
			{
				LoadOptionsOnForm();
				labelRespackName.Text = "加载的资源包：" + RAGLINKCommons.RAGLINKPlatform.PackagesManager.packageInfo.packageName;
				labelRespackName.Visible = true;
				buttonStart.Enabled = true & startButtonCondition;
				labelReviewLoading.Visible = false;
				labelOptionsLoading.Visible = false;
			}
		}
		private void LoadOptionsOnForm()
		{
			optionsValueLoaded = RAGLINKCommons.RAGLINKPlatform.GraphicOptionsManager.graphicOptionsValue;
			optionsValueCurrent = (RAGLINKCommons.RAGLINKPlatform.GraphicOptionsManager.GraphicOptionsValue)optionsValueLoaded.Clone();
			checkBoxFullScreen.Checked = optionsValueLoaded.enableFullScreen;
			checkBoxWindowsState.Checked = !optionsValueLoaded.enableFullScreen;
			checkBoxMotionBlur.Checked = optionsValueLoaded.enableMotionBlur;
			checkBoxVSync.Checked = optionsValueLoaded.enableVSync;
			textBoxWidth.Text = optionsValueLoaded.simulatorWidth.ToString();
			textBoxHeight.Text = optionsValueLoaded.simulatorHeight.ToString();
			textBoxViewingDis.Text = optionsValueLoaded.viewingDistance.ToString();
			switch (optionsValueLoaded.antiAliasingLevel)
			{
				case 1:
					{
						buttonAntiLevel1.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
						buttonAntiLevel2.BackColor = System.Drawing.Color.White;
						buttonAntiLevel3.BackColor = System.Drawing.Color.White;
						buttonAntiLevel4.BackColor = System.Drawing.Color.White;
						break;
					}
				case 2:
					{
						buttonAntiLevel1.BackColor = System.Drawing.Color.White;
						buttonAntiLevel2.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
						buttonAntiLevel3.BackColor = System.Drawing.Color.White;
						buttonAntiLevel4.BackColor = System.Drawing.Color.White;
						break;
					}
				case 3:
					{
						buttonAntiLevel1.BackColor = System.Drawing.Color.White;
						buttonAntiLevel2.BackColor = System.Drawing.Color.White;
						buttonAntiLevel3.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
						buttonAntiLevel4.BackColor = System.Drawing.Color.White;
						break;
					}
				case 4:
					{
						buttonAntiLevel1.BackColor = System.Drawing.Color.White;
						buttonAntiLevel2.BackColor = System.Drawing.Color.White;
						buttonAntiLevel3.BackColor = System.Drawing.Color.White;
						buttonAntiLevel4.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
						break;
					}
			}
			switch (optionsValueLoaded.transparencyLevel)
			{
				case 1:
					{
						buttonTransLevel1.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
						buttonTransLevel2.BackColor = System.Drawing.Color.White;
						buttonTransLevel3.BackColor = System.Drawing.Color.White;
						break;
					}
				case 2:
					{
						buttonTransLevel1.BackColor = System.Drawing.Color.White;
						buttonTransLevel2.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
						buttonTransLevel3.BackColor = System.Drawing.Color.White;
						break;
					}
				case 3:
					{
						buttonTransLevel1.BackColor = System.Drawing.Color.White;
						buttonTransLevel2.BackColor = System.Drawing.Color.White;
						buttonTransLevel3.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
						break;
					}
			}
			ApplyGraphicOptions(optionsValueLoaded);
			buttonSaveOptions.Enabled = false;
		}
		private bool ApplyGraphicOptions(RAGLINKCommons.RAGLINKPlatform.GraphicOptionsManager.GraphicOptionsValue optionsData)
		{
			bool retValue = false;
			try
			{
				Interface.CurrentOptions.FullscreenMode = optionsData.enableFullScreen;
				if (optionsData.enableFullScreen)
				{
					Interface.CurrentOptions.FullscreenHeight = optionsData.simulatorHeight;
					Interface.CurrentOptions.FullscreenWidth = optionsData.simulatorWidth;
				}
				else
				{
					Interface.CurrentOptions.WindowHeight = optionsData.simulatorHeight;
					Interface.CurrentOptions.WindowWidth = optionsData.simulatorWidth;
				}
				Interface.CurrentOptions.VerticalSynchronization = optionsData.enableVSync;
				Interface.CurrentOptions.MotionBlur = optionsData.enableMotionBlur ? Interface.MotionBlurMode.High : Interface.MotionBlurMode.None;
				Interface.CurrentOptions.ViewingDistance = optionsData.viewingDistance;
				switch (optionsData.transparencyLevel)
				{
					case 1:
						{
							Interface.CurrentOptions.TransparencyMode = OpenBveApi.Graphics.TransparencyMode.Quality;
							break;
						}
					case 2:
						{
							Interface.CurrentOptions.TransparencyMode = OpenBveApi.Graphics.TransparencyMode.Intermediate;
							break;
						}
					case 3:
						{
							Interface.CurrentOptions.TransparencyMode = OpenBveApi.Graphics.TransparencyMode.Performance;
							break;
						}
				}
				Interface.CurrentOptions.AntiAliasingLevel = optionsData.antiAliasingLevel;
				retValue = true;
			}
			catch (Exception) { };
			return retValue;
		}

		private void ButtonStartStateSet(bool dataValue)
		{
			tabControlMain.Enabled = dataValue;
			listViewPlanFile.Enabled = dataValue;
			buttonStart.Enabled = dataValue;
			startButtonCondition = dataValue;
		}
		private void LaunchProject(string projectGUID)
		{
			ButtonStartStateSet(false);
			RAGLINKCommons.RAGLINKPlatform.UserInterfaceSwap.projectGUID = projectGUID;
			new System.Threading.Thread((System.Threading.ThreadStart)delegate
			{
				RAGLINKCommons.RAGLINKPlatform.formSummaryInit formSummaryInit = new RAGLINKCommons.RAGLINKPlatform.formSummaryInit();
				formSummaryInit.ShowDialog();
			}).Start();
		}
	}
}
