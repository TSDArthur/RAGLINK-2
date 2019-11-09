namespace OpenBve {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.textboxRouteDescription = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textboxTrainDescription = new System.Windows.Forms.TextBox();
            this.panelStart = new System.Windows.Forms.Panel();
            this.labelVersion = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageStart = new System.Windows.Forms.TabPage();
            this.labelReviewLoading = new System.Windows.Forms.Label();
            this.pictureboxRouteGradient = new System.Windows.Forms.PictureBox();
            this.pictureboxRouteMap = new System.Windows.Forms.PictureBox();
            this.pictureboxRouteImage = new System.Windows.Forms.PictureBox();
            this.pictureboxTrainImage = new System.Windows.Forms.PictureBox();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.labelOptionsLoading = new System.Windows.Forms.Label();
            this.buttonSaveOptions = new System.Windows.Forms.Button();
            this.checkBoxVSync = new System.Windows.Forms.CheckBox();
            this.textBoxViewingDis = new System.Windows.Forms.TextBox();
            this.labelViewingDisTitle = new System.Windows.Forms.Label();
            this.checkBoxMotionBlur = new System.Windows.Forms.CheckBox();
            this.buttonTransLevel3 = new System.Windows.Forms.Button();
            this.buttonTransLevel2 = new System.Windows.Forms.Button();
            this.labelTransLevelTitle = new System.Windows.Forms.Label();
            this.buttonTransLevel1 = new System.Windows.Forms.Button();
            this.buttonAntiLevel4 = new System.Windows.Forms.Button();
            this.buttonAntiLevel3 = new System.Windows.Forms.Button();
            this.buttonAntiLevel2 = new System.Windows.Forms.Button();
            this.labelAntiLevelTitle = new System.Windows.Forms.Label();
            this.buttonAntiLevel1 = new System.Windows.Forms.Button();
            this.labelEffectTitle = new System.Windows.Forms.Label();
            this.labelQualityTitle = new System.Windows.Forms.Label();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.labelHeightTitle = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.labelWidthTitle = new System.Windows.Forms.Label();
            this.checkBoxWindowsState = new System.Windows.Forms.CheckBox();
            this.checkBoxFullScreen = new System.Windows.Forms.CheckBox();
            this.labelGraphicTitle = new System.Windows.Forms.Label();
            this.listViewPlanFile = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderGUID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDescribe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelRespackName = new System.Windows.Forms.Label();
            this.pictureBoxBanner = new System.Windows.Forms.PictureBox();
            this.imageListTabs = new System.Windows.Forms.ImageList(this.components);
            this.panelStart.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxRouteGradient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxRouteMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxRouteImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxTrainImage)).BeginInit();
            this.tabPageOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // textboxRouteDescription
            // 
            this.textboxRouteDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textboxRouteDescription.BackColor = System.Drawing.Color.White;
            this.textboxRouteDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textboxRouteDescription.Location = new System.Drawing.Point(161, 9);
            this.textboxRouteDescription.Multiline = true;
            this.textboxRouteDescription.Name = "textboxRouteDescription";
            this.textboxRouteDescription.ReadOnly = true;
            this.textboxRouteDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxRouteDescription.Size = new System.Drawing.Size(230, 158);
            this.textboxRouteDescription.TabIndex = 0;
            this.textboxRouteDescription.Text = "线路描述";
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonStart.AutoEllipsis = true;
            this.buttonStart.BackColor = System.Drawing.SystemColors.Window;
            this.buttonStart.Enabled = false;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Location = new System.Drawing.Point(537, 705);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(112, 29);
            this.buttonStart.TabIndex = 12;
            this.buttonStart.Text = "启动";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textboxTrainDescription
            // 
            this.textboxTrainDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textboxTrainDescription.BackColor = System.Drawing.SystemColors.Window;
            this.textboxTrainDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textboxTrainDescription.Location = new System.Drawing.Point(161, 169);
            this.textboxTrainDescription.Multiline = true;
            this.textboxTrainDescription.Name = "textboxTrainDescription";
            this.textboxTrainDescription.ReadOnly = true;
            this.textboxTrainDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxTrainDescription.Size = new System.Drawing.Size(230, 158);
            this.textboxTrainDescription.TabIndex = 0;
            this.textboxTrainDescription.Text = "机车车辆描述";
            // 
            // panelStart
            // 
            this.panelStart.BackColor = System.Drawing.SystemColors.Window;
            this.panelStart.Controls.Add(this.labelVersion);
            this.panelStart.Controls.Add(this.tabControlMain);
            this.panelStart.Controls.Add(this.listViewPlanFile);
            this.panelStart.Controls.Add(this.buttonStart);
            this.panelStart.Location = new System.Drawing.Point(0, 44);
            this.panelStart.Name = "panelStart";
            this.panelStart.Size = new System.Drawing.Size(653, 740);
            this.panelStart.TabIndex = 7;
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.SystemColors.Window;
            this.labelVersion.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelVersion.Location = new System.Drawing.Point(5, 714);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(192, 19);
            this.labelVersion.TabIndex = 17;
            this.labelVersion.Text = "版本：1.0";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageStart);
            this.tabControlMain.Controls.Add(this.tabPageOptions);
            this.tabControlMain.ImageList = this.imageListTabs;
            this.tabControlMain.Location = new System.Drawing.Point(3, 3);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(650, 560);
            this.tabControlMain.TabIndex = 14;
            // 
            // tabPageStart
            // 
            this.tabPageStart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageStart.Controls.Add(this.labelReviewLoading);
            this.tabPageStart.Controls.Add(this.pictureboxRouteGradient);
            this.tabPageStart.Controls.Add(this.pictureboxRouteMap);
            this.tabPageStart.Controls.Add(this.pictureboxRouteImage);
            this.tabPageStart.Controls.Add(this.textboxRouteDescription);
            this.tabPageStart.Controls.Add(this.textboxTrainDescription);
            this.tabPageStart.Controls.Add(this.pictureboxTrainImage);
            this.tabPageStart.ImageIndex = 0;
            this.tabPageStart.Location = new System.Drawing.Point(4, 26);
            this.tabPageStart.Name = "tabPageStart";
            this.tabPageStart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStart.Size = new System.Drawing.Size(642, 530);
            this.tabPageStart.TabIndex = 0;
            this.tabPageStart.Text = "起始页";
            // 
            // labelReviewLoading
            // 
            this.labelReviewLoading.BackColor = System.Drawing.SystemColors.Window;
            this.labelReviewLoading.Location = new System.Drawing.Point(0, 0);
            this.labelReviewLoading.Name = "labelReviewLoading";
            this.labelReviewLoading.Size = new System.Drawing.Size(642, 530);
            this.labelReviewLoading.TabIndex = 16;
            this.labelReviewLoading.Text = "未选中行车计划，无预览。";
            this.labelReviewLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureboxRouteGradient
            // 
            this.pictureboxRouteGradient.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureboxRouteGradient.BackColor = System.Drawing.SystemColors.Window;
            this.pictureboxRouteGradient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureboxRouteGradient.Location = new System.Drawing.Point(2, 329);
            this.pictureboxRouteGradient.Name = "pictureboxRouteGradient";
            this.pictureboxRouteGradient.Size = new System.Drawing.Size(636, 199);
            this.pictureboxRouteGradient.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureboxRouteGradient.TabIndex = 1;
            this.pictureboxRouteGradient.TabStop = false;
            // 
            // pictureboxRouteMap
            // 
            this.pictureboxRouteMap.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureboxRouteMap.BackColor = System.Drawing.SystemColors.Window;
            this.pictureboxRouteMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureboxRouteMap.Location = new System.Drawing.Point(392, 9);
            this.pictureboxRouteMap.Name = "pictureboxRouteMap";
            this.pictureboxRouteMap.Size = new System.Drawing.Size(246, 318);
            this.pictureboxRouteMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureboxRouteMap.TabIndex = 0;
            this.pictureboxRouteMap.TabStop = false;
            // 
            // pictureboxRouteImage
            // 
            this.pictureboxRouteImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureboxRouteImage.BackColor = System.Drawing.SystemColors.Window;
            this.pictureboxRouteImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureboxRouteImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureboxRouteImage.Location = new System.Drawing.Point(2, 9);
            this.pictureboxRouteImage.Name = "pictureboxRouteImage";
            this.pictureboxRouteImage.Size = new System.Drawing.Size(158, 158);
            this.pictureboxRouteImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureboxRouteImage.TabIndex = 0;
            this.pictureboxRouteImage.TabStop = false;
            // 
            // pictureboxTrainImage
            // 
            this.pictureboxTrainImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureboxTrainImage.BackColor = System.Drawing.SystemColors.Window;
            this.pictureboxTrainImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureboxTrainImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureboxTrainImage.Location = new System.Drawing.Point(2, 169);
            this.pictureboxTrainImage.Name = "pictureboxTrainImage";
            this.pictureboxTrainImage.Size = new System.Drawing.Size(158, 158);
            this.pictureboxTrainImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureboxTrainImage.TabIndex = 0;
            this.pictureboxTrainImage.TabStop = false;
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageOptions.Controls.Add(this.labelOptionsLoading);
            this.tabPageOptions.Controls.Add(this.buttonSaveOptions);
            this.tabPageOptions.Controls.Add(this.checkBoxVSync);
            this.tabPageOptions.Controls.Add(this.textBoxViewingDis);
            this.tabPageOptions.Controls.Add(this.labelViewingDisTitle);
            this.tabPageOptions.Controls.Add(this.checkBoxMotionBlur);
            this.tabPageOptions.Controls.Add(this.buttonTransLevel3);
            this.tabPageOptions.Controls.Add(this.buttonTransLevel2);
            this.tabPageOptions.Controls.Add(this.labelTransLevelTitle);
            this.tabPageOptions.Controls.Add(this.buttonTransLevel1);
            this.tabPageOptions.Controls.Add(this.buttonAntiLevel4);
            this.tabPageOptions.Controls.Add(this.buttonAntiLevel3);
            this.tabPageOptions.Controls.Add(this.buttonAntiLevel2);
            this.tabPageOptions.Controls.Add(this.labelAntiLevelTitle);
            this.tabPageOptions.Controls.Add(this.buttonAntiLevel1);
            this.tabPageOptions.Controls.Add(this.labelEffectTitle);
            this.tabPageOptions.Controls.Add(this.labelQualityTitle);
            this.tabPageOptions.Controls.Add(this.textBoxHeight);
            this.tabPageOptions.Controls.Add(this.labelHeightTitle);
            this.tabPageOptions.Controls.Add(this.textBoxWidth);
            this.tabPageOptions.Controls.Add(this.labelWidthTitle);
            this.tabPageOptions.Controls.Add(this.checkBoxWindowsState);
            this.tabPageOptions.Controls.Add(this.checkBoxFullScreen);
            this.tabPageOptions.Controls.Add(this.labelGraphicTitle);
            this.tabPageOptions.ImageIndex = 1;
            this.tabPageOptions.Location = new System.Drawing.Point(4, 26);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOptions.Size = new System.Drawing.Size(642, 530);
            this.tabPageOptions.TabIndex = 1;
            this.tabPageOptions.Text = "图形设置";
            // 
            // labelOptionsLoading
            // 
            this.labelOptionsLoading.BackColor = System.Drawing.SystemColors.Window;
            this.labelOptionsLoading.Location = new System.Drawing.Point(0, 0);
            this.labelOptionsLoading.Name = "labelOptionsLoading";
            this.labelOptionsLoading.Size = new System.Drawing.Size(642, 530);
            this.labelOptionsLoading.TabIndex = 17;
            this.labelOptionsLoading.Text = "未选中行车计划。";
            this.labelOptionsLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonSaveOptions
            // 
            this.buttonSaveOptions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSaveOptions.AutoEllipsis = true;
            this.buttonSaveOptions.BackColor = System.Drawing.SystemColors.Window;
            this.buttonSaveOptions.Enabled = false;
            this.buttonSaveOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveOptions.Location = new System.Drawing.Point(3, 491);
            this.buttonSaveOptions.Name = "buttonSaveOptions";
            this.buttonSaveOptions.Size = new System.Drawing.Size(120, 24);
            this.buttonSaveOptions.TabIndex = 15;
            this.buttonSaveOptions.Text = "保存设置";
            this.buttonSaveOptions.UseVisualStyleBackColor = false;
            this.buttonSaveOptions.Click += new System.EventHandler(this.ButtonSaveOptions_Click);
            // 
            // checkBoxVSync
            // 
            this.checkBoxVSync.AutoSize = true;
            this.checkBoxVSync.Location = new System.Drawing.Point(81, 230);
            this.checkBoxVSync.Name = "checkBoxVSync";
            this.checkBoxVSync.Size = new System.Drawing.Size(75, 21);
            this.checkBoxVSync.TabIndex = 39;
            this.checkBoxVSync.Text = "垂直同步";
            this.checkBoxVSync.UseVisualStyleBackColor = true;
            this.checkBoxVSync.CheckedChanged += new System.EventHandler(this.CheckBoxVSync_CheckedChanged);
            // 
            // textBoxViewingDis
            // 
            this.textBoxViewingDis.Location = new System.Drawing.Point(102, 254);
            this.textBoxViewingDis.Name = "textBoxViewingDis";
            this.textBoxViewingDis.ShortcutsEnabled = false;
            this.textBoxViewingDis.Size = new System.Drawing.Size(148, 23);
            this.textBoxViewingDis.TabIndex = 38;
            this.textBoxViewingDis.Text = "800";
            this.textBoxViewingDis.TextChanged += new System.EventHandler(this.TextBoxViewingDis_TextChanged);
            this.textBoxViewingDis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxViewingDis_KeyPress);
            // 
            // labelViewingDisTitle
            // 
            this.labelViewingDisTitle.AutoSize = true;
            this.labelViewingDisTitle.Location = new System.Drawing.Point(1, 257);
            this.labelViewingDisTitle.Name = "labelViewingDisTitle";
            this.labelViewingDisTitle.Size = new System.Drawing.Size(104, 17);
            this.labelViewingDisTitle.TabIndex = 37;
            this.labelViewingDisTitle.Text = "可视距离（米）：";
            // 
            // checkBoxMotionBlur
            // 
            this.checkBoxMotionBlur.AutoSize = true;
            this.checkBoxMotionBlur.Location = new System.Drawing.Point(3, 230);
            this.checkBoxMotionBlur.Name = "checkBoxMotionBlur";
            this.checkBoxMotionBlur.Size = new System.Drawing.Size(75, 21);
            this.checkBoxMotionBlur.TabIndex = 36;
            this.checkBoxMotionBlur.Text = "动态模糊";
            this.checkBoxMotionBlur.UseVisualStyleBackColor = true;
            this.checkBoxMotionBlur.CheckedChanged += new System.EventHandler(this.CheckBoxMotionBlur_CheckedChanged);
            // 
            // buttonTransLevel3
            // 
            this.buttonTransLevel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonTransLevel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTransLevel3.Location = new System.Drawing.Point(141, 169);
            this.buttonTransLevel3.Name = "buttonTransLevel3";
            this.buttonTransLevel3.Size = new System.Drawing.Size(25, 25);
            this.buttonTransLevel3.TabIndex = 34;
            this.buttonTransLevel3.Text = "3";
            this.buttonTransLevel3.UseVisualStyleBackColor = false;
            this.buttonTransLevel3.Click += new System.EventHandler(this.ButtonTransLevel3_Click);
            // 
            // buttonTransLevel2
            // 
            this.buttonTransLevel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTransLevel2.Location = new System.Drawing.Point(114, 169);
            this.buttonTransLevel2.Name = "buttonTransLevel2";
            this.buttonTransLevel2.Size = new System.Drawing.Size(25, 25);
            this.buttonTransLevel2.TabIndex = 33;
            this.buttonTransLevel2.Text = "2";
            this.buttonTransLevel2.UseVisualStyleBackColor = true;
            this.buttonTransLevel2.Click += new System.EventHandler(this.ButtonTransLevel2_Click);
            // 
            // labelTransLevelTitle
            // 
            this.labelTransLevelTitle.AutoSize = true;
            this.labelTransLevelTitle.Location = new System.Drawing.Point(1, 173);
            this.labelTransLevelTitle.Name = "labelTransLevelTitle";
            this.labelTransLevelTitle.Size = new System.Drawing.Size(80, 17);
            this.labelTransLevelTitle.TabIndex = 32;
            this.labelTransLevelTitle.Text = "纹理锐利化：";
            // 
            // buttonTransLevel1
            // 
            this.buttonTransLevel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTransLevel1.Location = new System.Drawing.Point(87, 169);
            this.buttonTransLevel1.Name = "buttonTransLevel1";
            this.buttonTransLevel1.Size = new System.Drawing.Size(25, 25);
            this.buttonTransLevel1.TabIndex = 31;
            this.buttonTransLevel1.Text = "1";
            this.buttonTransLevel1.UseVisualStyleBackColor = true;
            this.buttonTransLevel1.Click += new System.EventHandler(this.ButtonTransLevel1_Click);
            // 
            // buttonAntiLevel4
            // 
            this.buttonAntiLevel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonAntiLevel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAntiLevel4.Location = new System.Drawing.Point(168, 138);
            this.buttonAntiLevel4.Name = "buttonAntiLevel4";
            this.buttonAntiLevel4.Size = new System.Drawing.Size(25, 25);
            this.buttonAntiLevel4.TabIndex = 30;
            this.buttonAntiLevel4.Text = "4";
            this.buttonAntiLevel4.UseVisualStyleBackColor = false;
            this.buttonAntiLevel4.Click += new System.EventHandler(this.ButtonAntiLevel4_Click);
            // 
            // buttonAntiLevel3
            // 
            this.buttonAntiLevel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAntiLevel3.Location = new System.Drawing.Point(141, 138);
            this.buttonAntiLevel3.Name = "buttonAntiLevel3";
            this.buttonAntiLevel3.Size = new System.Drawing.Size(25, 25);
            this.buttonAntiLevel3.TabIndex = 29;
            this.buttonAntiLevel3.Text = "3";
            this.buttonAntiLevel3.UseVisualStyleBackColor = true;
            this.buttonAntiLevel3.Click += new System.EventHandler(this.ButtonAntiLevel3_Click);
            // 
            // buttonAntiLevel2
            // 
            this.buttonAntiLevel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAntiLevel2.Location = new System.Drawing.Point(114, 138);
            this.buttonAntiLevel2.Name = "buttonAntiLevel2";
            this.buttonAntiLevel2.Size = new System.Drawing.Size(25, 25);
            this.buttonAntiLevel2.TabIndex = 28;
            this.buttonAntiLevel2.Text = "2";
            this.buttonAntiLevel2.UseVisualStyleBackColor = true;
            this.buttonAntiLevel2.Click += new System.EventHandler(this.ButtonAntiLevel2_Click);
            // 
            // labelAntiLevelTitle
            // 
            this.labelAntiLevelTitle.AutoSize = true;
            this.labelAntiLevelTitle.Location = new System.Drawing.Point(1, 142);
            this.labelAntiLevelTitle.Name = "labelAntiLevelTitle";
            this.labelAntiLevelTitle.Size = new System.Drawing.Size(80, 17);
            this.labelAntiLevelTitle.TabIndex = 27;
            this.labelAntiLevelTitle.Text = "抗锯齿等级：";
            // 
            // buttonAntiLevel1
            // 
            this.buttonAntiLevel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAntiLevel1.Location = new System.Drawing.Point(87, 138);
            this.buttonAntiLevel1.Name = "buttonAntiLevel1";
            this.buttonAntiLevel1.Size = new System.Drawing.Size(25, 25);
            this.buttonAntiLevel1.TabIndex = 26;
            this.buttonAntiLevel1.Text = "1";
            this.buttonAntiLevel1.UseVisualStyleBackColor = true;
            this.buttonAntiLevel1.Click += new System.EventHandler(this.ButtonAntiLevel1_Click);
            // 
            // labelEffectTitle
            // 
            this.labelEffectTitle.AutoSize = true;
            this.labelEffectTitle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelEffectTitle.Location = new System.Drawing.Point(-1, 206);
            this.labelEffectTitle.Name = "labelEffectTitle";
            this.labelEffectTitle.Size = new System.Drawing.Size(65, 19);
            this.labelEffectTitle.TabIndex = 25;
            this.labelEffectTitle.Text = "效果设置";
            this.labelEffectTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelQualityTitle
            // 
            this.labelQualityTitle.AutoSize = true;
            this.labelQualityTitle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelQualityTitle.Location = new System.Drawing.Point(-1, 113);
            this.labelQualityTitle.Name = "labelQualityTitle";
            this.labelQualityTitle.Size = new System.Drawing.Size(65, 19);
            this.labelQualityTitle.TabIndex = 25;
            this.labelQualityTitle.Text = "画质设置";
            this.labelQualityTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(102, 79);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.ShortcutsEnabled = false;
            this.textBoxHeight.Size = new System.Drawing.Size(148, 23);
            this.textBoxHeight.TabIndex = 24;
            this.textBoxHeight.Text = "800";
            this.textBoxHeight.TextChanged += new System.EventHandler(this.TextBoxHeight_TextChanged);
            this.textBoxHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxHeight_KeyPress);
            // 
            // labelHeightTitle
            // 
            this.labelHeightTitle.AutoSize = true;
            this.labelHeightTitle.Location = new System.Drawing.Point(0, 82);
            this.labelHeightTitle.Name = "labelHeightTitle";
            this.labelHeightTitle.Size = new System.Drawing.Size(104, 17);
            this.labelHeightTitle.TabIndex = 23;
            this.labelHeightTitle.Text = "分辨率（纵向）：";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(102, 51);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.ShortcutsEnabled = false;
            this.textBoxWidth.Size = new System.Drawing.Size(148, 23);
            this.textBoxWidth.TabIndex = 22;
            this.textBoxWidth.Text = "960";
            this.textBoxWidth.TextChanged += new System.EventHandler(this.TextBoxWidth_TextChanged);
            this.textBoxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxWidth_KeyPress);
            // 
            // labelWidthTitle
            // 
            this.labelWidthTitle.AutoSize = true;
            this.labelWidthTitle.Location = new System.Drawing.Point(0, 54);
            this.labelWidthTitle.Name = "labelWidthTitle";
            this.labelWidthTitle.Size = new System.Drawing.Size(104, 17);
            this.labelWidthTitle.TabIndex = 21;
            this.labelWidthTitle.Text = "分辨率（横向）：";
            // 
            // checkBoxWindowsState
            // 
            this.checkBoxWindowsState.AutoSize = true;
            this.checkBoxWindowsState.Location = new System.Drawing.Point(60, 27);
            this.checkBoxWindowsState.Name = "checkBoxWindowsState";
            this.checkBoxWindowsState.Size = new System.Drawing.Size(75, 21);
            this.checkBoxWindowsState.TabIndex = 20;
            this.checkBoxWindowsState.Text = "窗口模式";
            this.checkBoxWindowsState.UseVisualStyleBackColor = true;
            this.checkBoxWindowsState.CheckedChanged += new System.EventHandler(this.CheckBoxWindowsState_CheckedChanged);
            // 
            // checkBoxFullScreen
            // 
            this.checkBoxFullScreen.AutoSize = true;
            this.checkBoxFullScreen.Location = new System.Drawing.Point(3, 27);
            this.checkBoxFullScreen.Name = "checkBoxFullScreen";
            this.checkBoxFullScreen.Size = new System.Drawing.Size(51, 21);
            this.checkBoxFullScreen.TabIndex = 19;
            this.checkBoxFullScreen.Text = "全屏";
            this.checkBoxFullScreen.UseVisualStyleBackColor = true;
            this.checkBoxFullScreen.CheckedChanged += new System.EventHandler(this.CheckBoxFullScreen_CheckedChanged);
            // 
            // labelGraphicTitle
            // 
            this.labelGraphicTitle.AutoSize = true;
            this.labelGraphicTitle.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelGraphicTitle.Location = new System.Drawing.Point(-1, 5);
            this.labelGraphicTitle.Name = "labelGraphicTitle";
            this.labelGraphicTitle.Size = new System.Drawing.Size(65, 19);
            this.labelGraphicTitle.TabIndex = 18;
            this.labelGraphicTitle.Text = "显示设置";
            this.labelGraphicTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listViewPlanFile
            // 
            this.listViewPlanFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listViewPlanFile.BackColor = System.Drawing.SystemColors.Window;
            this.listViewPlanFile.BackgroundImageTiled = true;
            this.listViewPlanFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewPlanFile.CheckBoxes = true;
            this.listViewPlanFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderGUID,
            this.columnHeaderDescribe});
            this.listViewPlanFile.FullRowSelect = true;
            this.listViewPlanFile.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPlanFile.HideSelection = false;
            this.listViewPlanFile.LabelWrap = false;
            this.listViewPlanFile.Location = new System.Drawing.Point(3, 562);
            this.listViewPlanFile.MultiSelect = false;
            this.listViewPlanFile.Name = "listViewPlanFile";
            this.listViewPlanFile.ShowGroups = false;
            this.listViewPlanFile.Size = new System.Drawing.Size(647, 137);
            this.listViewPlanFile.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewPlanFile.TabIndex = 2;
            this.listViewPlanFile.UseCompatibleStateImageBehavior = false;
            this.listViewPlanFile.View = System.Windows.Forms.View.Details;
            this.listViewPlanFile.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewPlanFile_ColumnClick);
            this.listViewPlanFile.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewPlanFile_ItemChecked);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "行车计划名称";
            this.columnHeaderName.Width = 180;
            // 
            // columnHeaderGUID
            // 
            this.columnHeaderGUID.Text = "GUID";
            this.columnHeaderGUID.Width = 250;
            // 
            // columnHeaderDescribe
            // 
            this.columnHeaderDescribe.Text = "简述";
            this.columnHeaderDescribe.Width = 217;
            // 
            // labelRespackName
            // 
            this.labelRespackName.BackColor = System.Drawing.SystemColors.Window;
            this.labelRespackName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelRespackName.Location = new System.Drawing.Point(181, 49);
            this.labelRespackName.Name = "labelRespackName";
            this.labelRespackName.Size = new System.Drawing.Size(469, 19);
            this.labelRespackName.TabIndex = 15;
            this.labelRespackName.Text = "加载的资源包：无";
            this.labelRespackName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBoxBanner
            // 
            this.pictureBoxBanner.Image = global::OpenBve.Properties.Resources.banner;
            this.pictureBoxBanner.Location = new System.Drawing.Point(3, 2);
            this.pictureBoxBanner.Name = "pictureBoxBanner";
            this.pictureBoxBanner.Size = new System.Drawing.Size(650, 38);
            this.pictureBoxBanner.TabIndex = 16;
            this.pictureBoxBanner.TabStop = false;
            // 
            // imageListTabs
            // 
            this.imageListTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTabs.ImageStream")));
            this.imageListTabs.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTabs.Images.SetKeyName(0, "index_page.png");
            this.imageListTabs.Images.SetKeyName(1, "option_page.png");
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(653, 783);
            this.Controls.Add(this.pictureBoxBanner);
            this.Controls.Add(this.labelRespackName);
            this.Controls.Add(this.panelStart);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " RAGLINK+ 2 OB";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.formMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.panelStart.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageStart.ResumeLayout(false);
            this.tabPageStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxRouteGradient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxRouteMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxRouteImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxTrainImage)).EndInit();
            this.tabPageOptions.ResumeLayout(false);
            this.tabPageOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
		private System.Windows.Forms.TextBox textboxRouteDescription;
		private System.Windows.Forms.PictureBox pictureboxRouteImage;
		private System.Windows.Forms.PictureBox pictureboxRouteMap;
		private System.Windows.Forms.PictureBox pictureboxRouteGradient;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.TextBox textboxTrainDescription;
		private System.Windows.Forms.PictureBox pictureboxTrainImage;
		private System.Windows.Forms.Panel panelStart;
		private System.Windows.Forms.ListView listViewPlanFile;
		private System.Windows.Forms.ColumnHeader columnHeaderName;
		private System.Windows.Forms.ColumnHeader columnHeaderGUID;
		private System.Windows.Forms.ColumnHeader columnHeaderDescribe;
		private System.Windows.Forms.TabControl tabControlMain;
		private System.Windows.Forms.TabPage tabPageStart;
		private System.Windows.Forms.TabPage tabPageOptions;
		private System.Windows.Forms.Label labelRespackName;
		private System.Windows.Forms.Label labelReviewLoading;
		private System.Windows.Forms.Label labelOptionsLoading;
		private System.Windows.Forms.CheckBox checkBoxWindowsState;
		private System.Windows.Forms.CheckBox checkBoxFullScreen;
		private System.Windows.Forms.Label labelGraphicTitle;
		private System.Windows.Forms.Button buttonAntiLevel4;
		private System.Windows.Forms.Button buttonAntiLevel3;
		private System.Windows.Forms.Button buttonAntiLevel2;
		private System.Windows.Forms.Label labelAntiLevelTitle;
		private System.Windows.Forms.Button buttonAntiLevel1;
		private System.Windows.Forms.Label labelQualityTitle;
		private System.Windows.Forms.TextBox textBoxHeight;
		private System.Windows.Forms.Label labelHeightTitle;
		private System.Windows.Forms.TextBox textBoxWidth;
		private System.Windows.Forms.Label labelWidthTitle;
		private System.Windows.Forms.TextBox textBoxViewingDis;
		private System.Windows.Forms.Label labelViewingDisTitle;
		private System.Windows.Forms.CheckBox checkBoxMotionBlur;
		private System.Windows.Forms.Button buttonTransLevel3;
		private System.Windows.Forms.Button buttonTransLevel2;
		private System.Windows.Forms.Label labelTransLevelTitle;
		private System.Windows.Forms.Button buttonTransLevel1;
		private System.Windows.Forms.Label labelEffectTitle;
		private System.Windows.Forms.CheckBox checkBoxVSync;
		private System.Windows.Forms.Button buttonSaveOptions;
		private System.Windows.Forms.PictureBox pictureBoxBanner;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.ImageList imageListTabs;
	}
}
