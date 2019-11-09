using OpenBveApi;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OpenBve
{
	internal partial class FormMain : Form
	{
		static public bool formCloseTrig = false;
		public delegate void ShowRouteDelegate(bool UserSelectedEncoding);
		public delegate void ShowTrainDelegate(bool UserSelectedEncoding);
		// =====
		// start
		// =====

		// start
		private readonly object StartGame = new Object();

		private void StartSimulator()
		{
			if (Result.RouteFile != null & Result.TrainFolder != null)
			{
				if (System.IO.File.Exists(Result.RouteFile) & System.IO.Directory.Exists(Result.TrainFolder))
				{
					Result.Start = true;
					formCloseTrig = true;
					Sounds.Deinitialize();
					if (routeWorkerThread != null)
					{
						routeWorkerThread.Dispose();
					}
					//HACK: Call Application.DoEvents() to force the message pump to process all pending messages when the form closes
					//This fixes the main form failing to close on Linux
					Application.DoEvents();
					this.Close();
				}
			}
			else
			{
				System.Media.SystemSounds.Exclamation.Play();
			}
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			tabControlMain.SelectedIndex = 0;
			LaunchProject(listViewPlanFile.Items[selectedPlanID].SubItems[1].Text);
		}


		// =========
		// functions
		// =========

		private BackgroundWorker routeWorkerThread;

		private void routeWorkerThread_doWork(object sender, DoWorkEventArgs e)
		{
			if (string.IsNullOrEmpty(Result.RouteFile))
			{
				return;
			}
			Game.Reset(false);
			bool IsRW = string.Equals(System.IO.Path.GetExtension(Result.RouteFile), ".rw", StringComparison.OrdinalIgnoreCase);
			CsvRwRouteParser.ParseRoute(Result.RouteFile, IsRW, Result.RouteEncoding, null, null, null, true);
		}

		private void routeWorkerThread_completed(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				TryLoadImage(pictureboxRouteImage, "route_error.png");
				textboxRouteDescription.Text = e.Error.Message;
				pictureboxRouteMap.Image = null;
				pictureboxRouteGradient.Image = null;
				Result.ErrorFile = Result.RouteFile;
				Result.RouteFile = null;
				if (routeWorkerThread != null)
				{
					routeWorkerThread.Dispose();
				}

				this.Cursor = System.Windows.Forms.Cursors.Default;
				return;
			}
			try
			{
				lock (Illustrations.Locker)
				{
					pictureboxRouteMap.BorderStyle = BorderStyle.None;
					pictureboxRouteGradient.BorderStyle = BorderStyle.None;
					pictureboxRouteMap.Image = Illustrations.CreateRouteMap(pictureboxRouteMap.Width, pictureboxRouteMap.Height, false);
					pictureboxRouteGradient.Image = Illustrations.CreateRouteGradientProfile(pictureboxRouteGradient.Width,
						pictureboxRouteGradient.Height, false);
					pictureboxRouteMap.BorderStyle = BorderStyle.FixedSingle;
					pictureboxRouteGradient.BorderStyle = BorderStyle.FixedSingle;
				}
				// image
				if (Game.RouteImage.Length != 0)
				{
					TryLoadImage(pictureboxRouteImage, Game.RouteImage);
				}
				else
				{
					string[] f = new string[] { ".png", ".bmp", ".gif", ".tiff", ".tif", ".jpeg", ".jpg" };
					int i;
					for (i = 0; i < f.Length; i++)
					{
						string g = OpenBveApi.Path.CombineFile(System.IO.Path.GetDirectoryName(Result.RouteFile),
							System.IO.Path.GetFileNameWithoutExtension(Result.RouteFile) + f[i]);
						if (System.IO.File.Exists(g))
						{
							try
							{
								using (var fs = new FileStream(g, FileMode.Open, FileAccess.Read))
								{
									pictureboxRouteImage.Image = new Bitmap(fs);
								}
							}
							catch
							{
								pictureboxRouteImage.Image = null;
							}
							break;
						}
					}
					if (i == f.Length)
					{
						TryLoadImage(pictureboxRouteImage, "route_unknown.png");
					}
				}

				// description
				string Description = Game.RouteComment.ConvertNewlinesToCrLf();
				if (Description.Length != 0)
				{
					textboxRouteDescription.Text = Description;
				}
				else
				{
					textboxRouteDescription.Text = System.IO.Path.GetFileNameWithoutExtension(Result.RouteFile);
				}
				Result.ErrorFile = null;
			}
			catch (Exception ex)
			{
				TryLoadImage(pictureboxRouteImage, "route_error.png");
				textboxRouteDescription.Text = ex.Message;
				pictureboxRouteMap.Image = null;
				pictureboxRouteGradient.Image = null;
				Result.ErrorFile = Result.RouteFile;
				Result.RouteFile = null;
			}

			this.Cursor = System.Windows.Forms.Cursors.Default;
			//Deliberately select the tab when the process is complete
			//This hopefully fixes another instance of the 'grey tabs' bug

			//For RAGLINK Package Manager
			ReviewProject(2, string.Empty);
			RAGLINKCommons.RPlatform.PackagesManager.SetRouteDetailData(pictureboxRouteMap.Image, pictureboxRouteGradient.Image,
				System.IO.Path.GetFileNameWithoutExtension(Result.RouteFile), textboxRouteDescription.Text);
		}


		// show route
		private void ShowRoute(bool UserSelectedEncoding)
		{
			if (routeWorkerThread == null)
			{
				return;
			}
			if (Result.RouteFile != null && !routeWorkerThread.IsBusy)
			{
				this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
				// determine encoding
				if (!UserSelectedEncoding)
				{
					Result.RouteEncoding = System.Text.Encoding.Default;
					switch (TextEncoding.GetEncodingFromFile(Result.RouteFile))
					{
						case TextEncoding.Encoding.Utf7:
							Result.RouteEncoding = System.Text.Encoding.UTF7;
							break;
						case TextEncoding.Encoding.Utf8:
							Result.RouteEncoding = System.Text.Encoding.UTF8;
							break;
						case TextEncoding.Encoding.Utf16Le:
							Result.RouteEncoding = System.Text.Encoding.Unicode;
							break;
						case TextEncoding.Encoding.Utf16Be:
							Result.RouteEncoding = System.Text.Encoding.BigEndianUnicode;
							break;
						case TextEncoding.Encoding.Utf32Le:
							Result.RouteEncoding = System.Text.Encoding.UTF32;
							break;
						case TextEncoding.Encoding.Utf32Be:
							Result.RouteEncoding = System.Text.Encoding.GetEncoding(12001);
							break;
						case TextEncoding.Encoding.Shift_JIS:
							Result.RouteEncoding = System.Text.Encoding.GetEncoding(932);
							break;
						case TextEncoding.Encoding.Windows1252:
							Result.RouteEncoding = System.Text.Encoding.GetEncoding(1252);
							break;
						case TextEncoding.Encoding.Big5:
							Result.RouteEncoding = System.Text.Encoding.GetEncoding(950);
							break;
						case TextEncoding.Encoding.EUC_KR:
							Result.RouteEncoding = System.Text.Encoding.GetEncoding(949);
							break;
					}

					int i;
					for (i = 0; i < Interface.CurrentOptions.RouteEncodings.Length; i++)
					{
						if (Interface.CurrentOptions.RouteEncodings[i].Value == Result.RouteFile)
						{
							int j;
							for (j = 1; j < EncodingCodepages.Length; j++)
							{
								if (EncodingCodepages[j] == Interface.CurrentOptions.RouteEncodings[i].Codepage)
								{
									Result.RouteEncoding = System.Text.Encoding.GetEncoding(EncodingCodepages[j]);
									break;
								}
							}
							if (j == EncodingCodepages.Length)
							{
								Result.RouteEncoding = System.Text.Encoding.UTF8;
							}
							break;
						}
					}
				}
				if (!routeWorkerThread.IsBusy)
				{
					//HACK: If clicking very rapidly or holding down an arrow
					//		we can sometimes try to spawn two worker threads
					routeWorkerThread.RunWorkerAsync();
				}
			}
		}

		// show train
		private void ShowTrain(bool UserSelectedEncoding)
		{
			if (!UserSelectedEncoding)
			{
				Result.TrainEncoding = System.Text.Encoding.Default;
				switch (TextEncoding.GetEncodingFromFile(Result.TrainFolder, "train.txt"))
				{
					case TextEncoding.Encoding.Utf8:
						Result.TrainEncoding = System.Text.Encoding.UTF8;
						break;
					case TextEncoding.Encoding.Utf16Le:
						Result.TrainEncoding = System.Text.Encoding.Unicode;
						break;
					case TextEncoding.Encoding.Utf16Be:
						Result.TrainEncoding = System.Text.Encoding.BigEndianUnicode;
						break;
					case TextEncoding.Encoding.Utf32Le:
						Result.TrainEncoding = System.Text.Encoding.UTF32;
						break;
					case TextEncoding.Encoding.Utf32Be:
						Result.TrainEncoding = System.Text.Encoding.GetEncoding(12001);
						break;
					case TextEncoding.Encoding.Shift_JIS:
						Result.TrainEncoding = System.Text.Encoding.GetEncoding(932);
						break;
					case TextEncoding.Encoding.Windows1252:
						Result.TrainEncoding = System.Text.Encoding.GetEncoding(1252);
						break;
					case TextEncoding.Encoding.Big5:
						Result.TrainEncoding = System.Text.Encoding.GetEncoding(950);
						break;
					case TextEncoding.Encoding.EUC_KR:
						Result.TrainEncoding = System.Text.Encoding.GetEncoding(950);
						break;
				}
				int i;
				for (i = 0; i < Interface.CurrentOptions.TrainEncodings.Length; i++)
				{
					if (Interface.CurrentOptions.TrainEncodings[i].Value == Result.TrainFolder)
					{
						int j;
						for (j = 1; j < EncodingCodepages.Length; j++)
						{
							if (EncodingCodepages[j] == Interface.CurrentOptions.TrainEncodings[i].Codepage)
							{
								Result.TrainEncoding = System.Text.Encoding.GetEncoding(EncodingCodepages[j]);
								break;
							}
						}
						if (j == EncodingCodepages.Length)
						{
							Result.TrainEncoding = System.Text.Encoding.UTF8;
						}
						break;
					}
				}
			}
			{
				// train image
				string File = OpenBveApi.Path.CombineFile(Result.TrainFolder, "train.png");
				if (!System.IO.File.Exists(File))
				{
					File = OpenBveApi.Path.CombineFile(Result.TrainFolder, "train.bmp");
				}
				if (System.IO.File.Exists(File))
				{
					TryLoadImage(pictureboxTrainImage, File);
				}
				else
				{
					TryLoadImage(pictureboxTrainImage, "train_unknown.png");
				}
			}
			{
				// train description
				string File = OpenBveApi.Path.CombineFile(Result.TrainFolder, "train.txt");
				if (System.IO.File.Exists(File))
				{
					try
					{
						string trainText = System.IO.File.ReadAllText(File, Result.TrainEncoding);
						trainText = trainText.ConvertNewlinesToCrLf();
						textboxTrainDescription.Text = trainText;
					}
					catch
					{
						textboxTrainDescription.Text = System.IO.Path.GetFileName(Result.TrainFolder);
					}
				}
				else
				{
					textboxTrainDescription.Text = System.IO.Path.GetFileName(Result.TrainFolder);
				}
			}

			// for RAGLINK Package Manager
			RAGLINKCommons.RPlatform.PackagesManager.SetTrainDetailData(System.IO.Path.GetFileNameWithoutExtension(Result.TrainFolder), textboxTrainDescription.Text);
		}
	}
}
