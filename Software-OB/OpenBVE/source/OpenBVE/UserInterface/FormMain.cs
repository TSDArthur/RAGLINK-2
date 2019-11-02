using OpenBve.RAGLINKPlatform;
using OpenBveApi.Interface;
using RAGLINKCommons;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OpenBve
{
	internal partial class formMain : Form
	{
		private formMain()
		{
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;
			this.Text = Translations.GetInterfaceString("program_title");
		}

		public sealed override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// show main dialog
		internal struct MainDialogResult
		{
			/// <summary>Whether to start the simulation</summary>
			internal bool Start;
			/// <summary>The absolute on-disk path of the route file to start the simulation with</summary>
			internal string RouteFile;
			/// <summary>The last file an error was encountered on (Used for changing character encodings)</summary>
			internal string ErrorFile;
			/// <summary>The text encoding of the selected route file</summary>
			internal System.Text.Encoding RouteEncoding;
			/// <summary>The absolute on-disk path of the train folder to start the simulation with</summary>
			internal string TrainFolder;
			/// <summary>The text encoding of the selected train</summary>
			internal System.Text.Encoding TrainEncoding;
			internal string InitialStation;
			internal double StartTime;
			internal bool AIDriver;
			internal bool FullScreen;
			internal int Width;
			internal int Height;
		}
		internal static MainDialogResult ShowMainDialog(MainDialogResult initial)
		{
			using (formMain Dialog = new formMain())
			{
				Dialog.Result = initial;
				new System.Threading.Thread((System.Threading.ThreadStart)delegate
				{
					Application.Run(Dialog);
				}).Start();
				while (!formCloseTrig) ;
				MainDialogResult result = Dialog.Result;
				try
				{
					//Dispose of the worker thread when closing the form
					//If it's still running, it attempts to update a non-existant form and crashes nastily
					Dialog.routeWorkerThread.Dispose();
					Dialog.Dispose();
				}
				catch (Exception) { };
				return result;
			}
		}

		// members
		private MainDialogResult Result;
		private int[] EncodingCodepages = new int[0];

		// ====
		// form
		// ====

		// load
		private void formMain_Load(object sender, EventArgs e)
		{
			// ===
			// original part
			// ===
			this.MinimumSize = this.Size;
			if (Interface.CurrentOptions.MainMenuWidth == -1 & Interface.CurrentOptions.MainMenuHeight == -1)
			{
				this.WindowState = FormWindowState.Maximized;
			}
			else if (Interface.CurrentOptions.MainMenuWidth > 0 & Interface.CurrentOptions.MainMenuHeight > 0)
			{
				//this.Size = new Size(Interface.CurrentOptions.MainMenuWidth, Interface.CurrentOptions.MainMenuHeight);
				this.CenterToScreen();
			}
			System.Globalization.CultureInfo Culture = System.Globalization.CultureInfo.InvariantCulture;
			// form icon
			try
			{
				string File = OpenBveApi.Path.CombineFile(Program.FileSystem.GetDataFolder(), "icon.ico");
				this.Icon = new Icon(File);
			}
			catch { }
			// icons and images
			string MenuFolder = Program.FileSystem.GetDataFolder("Menu");
			Image ParentIcon = LoadImage(MenuFolder, "icon_parent.png");
			Image FolderIcon = LoadImage(MenuFolder, "icon_folder.png");
			Image RouteIcon = LoadImage(MenuFolder, "icon_route.png");
			Image TrainIcon = LoadImage(MenuFolder, "icon_train.png");
			pictureboxRouteImage.ErrorImage = LoadImage(Program.FileSystem.GetDataFolder("Menu"), "error_route.png");
			pictureboxTrainImage.ErrorImage = LoadImage(Program.FileSystem.GetDataFolder("Menu"), "error_train.png");
#pragma warning restore 0219
			// encodings
			{
				System.Text.EncodingInfo[] Info = System.Text.Encoding.GetEncodings();
				EncodingCodepages = new int[Info.Length + 1];
				string[] EncodingDescriptions = new string[Info.Length + 1];
				EncodingCodepages[0] = System.Text.Encoding.UTF8.CodePage;
				EncodingDescriptions[0] = "(UTF-8)";
				for (int i = 0; i < Info.Length; i++)
				{
					EncodingCodepages[i + 1] = Info[i].CodePage;
					try
					{
						EncodingDescriptions[i + 1] = Info[i].DisplayName + " - " + Info[i].CodePage.ToString(Culture);
					}
					catch
					{
						EncodingDescriptions[i + 1] = Info[i].Name;
					}
				}
				Array.Sort<string, int>(EncodingDescriptions, EncodingCodepages, 1, Info.Length);
			}
			routeWorkerThread = new BackgroundWorker();
			routeWorkerThread.DoWork += routeWorkerThread_doWork;
			routeWorkerThread.RunWorkerCompleted += routeWorkerThread_completed;
			// language
			Translations.CurrentLanguageCode = Interface.CurrentOptions.LanguageCode;
			Translations.SetInGameLanguage(Translations.CurrentLanguageCode);
		}

		// =========
		// functions
		// =========

		/// <summary>Attempts to load an image into memory using the OpenBVE path resolution API</summary>
		private Image LoadImage(string Folder, string Title)
		{
			try
			{
				string File = OpenBveApi.Path.CombineFile(Folder, Title);
				if (System.IO.File.Exists(File))
				{
					try
					{
						return Image.FromFile(File);
					}
					catch (Exception) { };
				}
				return null;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>Attempts to load an image into a picture box using the OpenBVE path resolution API</summary>
		private void TryLoadImage(PictureBox Box, string File)
		{
			try
			{
				if (!System.IO.File.Exists(File))
				{
					string Folder = Program.FileSystem.GetDataFolder("Menu");
					File = OpenBveApi.Path.CombineFile(Folder, File);
				}
				if (System.IO.File.Exists(File))
				{
					System.IO.FileInfo f = new System.IO.FileInfo(File);
					if (f.Length == 0)
					{
						Box.Image = Box.ErrorImage;
						return;
					}
					try
					{
						Box.Image = Image.FromFile(File);
						return;
					}
					catch
					{
						Box.Image = Box.ErrorImage;
						return;
					}
				}
				Box.Image = Box.ErrorImage;
			}
			catch
			{
				Box.Image = Box.ErrorImage;
			}
		}

		private void ListViewPlanFile_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if (e.Item.Checked)
			{
				for (int i = 0; i < e.Item.Index; i++) this.listViewPlanFile.Items[i].Checked = false;
				for (int i = e.Item.Index + 1; i < this.listViewPlanFile.Items.Count; i++) this.listViewPlanFile.Items[i].Checked = false;
				selectedPlanID = e.Item.Index;
				ReviewProject(1, listViewPlanFile.Items[selectedPlanID].SubItems[1].Text);
			}
			else
			{
				for (int i = 0; i < this.listViewPlanFile.Items.Count; i++)
				{
					if (this.listViewPlanFile.Items[i].Checked) return;
					ReviewProject(0, string.Empty);
				}
				selectedPlanID = -1;
			}
		}

		private void ListViewPlanFile_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (listViewPlanFile.Sorting == SortOrder.Ascending) listViewPlanFile.Sorting = SortOrder.Descending;
			else listViewPlanFile.Sorting = SortOrder.Ascending;
			listViewPlanFile.Sort();
		}

		private void CheckBoxFullScreen_CheckedChanged(object sender, EventArgs e)
		{
			checkBoxWindowsState.Checked = !checkBoxFullScreen.Checked;
			if (optionsValueCurrent == null) return;
			buttonSaveOptions.Enabled = true;
			optionsValueCurrent.enableFullScreen = checkBoxFullScreen.Checked;
		}

		private void CheckBoxWindowsState_CheckedChanged(object sender, EventArgs e)
		{
			checkBoxFullScreen.Checked = !checkBoxWindowsState.Checked;
			if (optionsValueCurrent == null) return;
			buttonSaveOptions.Enabled = true;
			optionsValueCurrent.enableFullScreen = !checkBoxWindowsState.Checked;
		}

		private void TextBoxWidth_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8))
			{
				e.Handled = true;
			}
		}

		private void TextBoxHeight_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8))
			{
				e.Handled = true;
			}
		}

		private void TextBoxViewingDis_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8))
			{
				e.Handled = true;
			}
		}

		private void TextBoxWidth_TextChanged(object sender, EventArgs e)
		{
			if (textBoxWidth.Text == string.Empty || Int32.Parse(textBoxWidth.Text) < 0 || Int32.Parse(textBoxWidth.Text) > optionsValueCurrent.maxSimulatorWidth)
			{
				textBoxWidth.BackColor = Color.LightCoral;
				buttonSaveOptions.Enabled = false;
			}
			else
			{
				textBoxWidth.BackColor = Color.White;
				if (optionsValueCurrent == null) return;
				buttonSaveOptions.Enabled = true;
				optionsValueCurrent.simulatorWidth = Int32.Parse(textBoxWidth.Text);
			}
		}

		private void TextBoxHeight_TextChanged(object sender, EventArgs e)
		{
			if (textBoxHeight.Text == string.Empty || Int32.Parse(textBoxHeight.Text) < 0 || Int32.Parse(textBoxHeight.Text) > optionsValueCurrent.maxSimulatorHeight)
			{
				textBoxHeight.BackColor = Color.LightCoral;
				buttonSaveOptions.Enabled = false;
			}
			else
			{
				textBoxHeight.BackColor = Color.White;
				if (optionsValueCurrent == null) return;
				buttonSaveOptions.Enabled = true;
				optionsValueCurrent.simulatorHeight = Int32.Parse(textBoxHeight.Text);
			}
		}

		private void TextBoxViewingDis_TextChanged(object sender, EventArgs e)
		{
			if (textBoxViewingDis.Text == string.Empty || Int32.Parse(textBoxViewingDis.Text) < 0 || Int32.Parse(textBoxViewingDis.Text) > optionsValueCurrent.maxViewingDistance)
			{
				textBoxViewingDis.BackColor = Color.LightCoral;
				buttonSaveOptions.Enabled = false;
			}
			else
			{
				textBoxViewingDis.BackColor = Color.White;
				if (optionsValueCurrent == null) return;
				buttonSaveOptions.Enabled = true;
				optionsValueCurrent.viewingDistance = Int32.Parse(textBoxViewingDis.Text);
			}
		}

		private void CheckBoxMotionBlur_CheckedChanged(object sender, EventArgs e)
		{
			if (optionsValueCurrent == null) return;
			buttonSaveOptions.Enabled = true;
			optionsValueCurrent.enableMotionBlur = checkBoxMotionBlur.Checked;
		}

		private void CheckBoxVSync_CheckedChanged(object sender, EventArgs e)
		{
			if (optionsValueCurrent == null) return;
			buttonSaveOptions.Enabled = true;
			optionsValueCurrent.enableVSync = checkBoxVSync.Checked;
		}

		private void ButtonAntiLevel1_Click(object sender, EventArgs e)
		{
			buttonAntiLevel1.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
			buttonAntiLevel2.BackColor = System.Drawing.Color.White;
			buttonAntiLevel3.BackColor = System.Drawing.Color.White;
			buttonAntiLevel4.BackColor = System.Drawing.Color.White;
			if (optionsValueCurrent == null) return;
			buttonSaveOptions.Enabled = true;
			optionsValueCurrent.antiAliasingLevel = 1;
		}

		private void ButtonAntiLevel2_Click(object sender, EventArgs e)
		{
			buttonAntiLevel1.BackColor = System.Drawing.Color.White;
			buttonAntiLevel2.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
			buttonAntiLevel3.BackColor = System.Drawing.Color.White;
			buttonAntiLevel4.BackColor = System.Drawing.Color.White;
			if (optionsValueCurrent == null) return;
			buttonSaveOptions.Enabled = true;
			optionsValueCurrent.antiAliasingLevel = 2;
		}

		private void ButtonAntiLevel3_Click(object sender, EventArgs e)
		{
			buttonAntiLevel1.BackColor = System.Drawing.Color.White;
			buttonAntiLevel2.BackColor = System.Drawing.Color.White;
			buttonAntiLevel3.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
			buttonAntiLevel4.BackColor = System.Drawing.Color.White;
			if (optionsValueCurrent == null) return;
			buttonSaveOptions.Enabled = true;
			optionsValueCurrent.antiAliasingLevel = 3;
		}

		private void ButtonAntiLevel4_Click(object sender, EventArgs e)
		{
			buttonAntiLevel1.BackColor = System.Drawing.Color.White;
			buttonAntiLevel2.BackColor = System.Drawing.Color.White;
			buttonAntiLevel3.BackColor = System.Drawing.Color.White;
			buttonAntiLevel4.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
			if (optionsValueCurrent == null) return;
			buttonSaveOptions.Enabled = true;
			optionsValueCurrent.antiAliasingLevel = 4;
		}

		private void ButtonTransLevel1_Click(object sender, EventArgs e)
		{
			buttonTransLevel1.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
			buttonTransLevel2.BackColor = System.Drawing.Color.White;
			buttonTransLevel3.BackColor = System.Drawing.Color.White;
			if (optionsValueCurrent == null) return;
			buttonSaveOptions.Enabled = true;
			optionsValueCurrent.transparencyLevel = 1;
		}

		private void ButtonTransLevel2_Click(object sender, EventArgs e)
		{
			buttonTransLevel1.BackColor = System.Drawing.Color.White;
			buttonTransLevel2.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
			buttonTransLevel3.BackColor = System.Drawing.Color.White;
			if (optionsValueCurrent == null) return;
			buttonSaveOptions.Enabled = true;
			optionsValueCurrent.transparencyLevel = 2;
		}

		private void ButtonTransLevel3_Click(object sender, EventArgs e)
		{
			buttonTransLevel1.BackColor = System.Drawing.Color.White;
			buttonTransLevel2.BackColor = System.Drawing.Color.White;
			buttonTransLevel3.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
			if (optionsValueCurrent == null) return;
			buttonSaveOptions.Enabled = true;
			optionsValueCurrent.transparencyLevel = 3;
		}

		private void ButtonSaveOptions_Click(object sender, EventArgs e)
		{
			optionsValueLoaded = (RAGLINKCommons.RAGLINKPlatform.GraphicOptionsManager.GraphicOptionsValue)optionsValueCurrent.Clone();
			RAGLINKCommons.RAGLINKPlatform.GraphicOptionsManager.graphicOptionsValue = optionsValueLoaded;
			RAGLINKCommons.RAGLINKPlatform.GraphicOptionsManager.SaveGraphicOptionsFile(optionsValueLoaded);
			ApplyGraphicOptions(optionsValueLoaded);
			buttonSaveOptions.Enabled = false;
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				if (e.CloseReason == CloseReason.UserClosing)
					formCloseTrig = true;
			}
			catch (Exception) { };
		}
		private void FormMain_Shown(object sender, EventArgs e)
		{
			// ===
			// RAGLINK Part
			// ===
			OnLoadEvents();
			StartArgsEvent(System.Environment.GetCommandLineArgs());
			RAGLINKCommons.RAGLINKPlatform.formSummary.FormMainStartButtonEvent += ButtonStartStateSet;
		}

		private void formMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !listViewPlanFile.Enabled;
		}
	}
}
