namespace RAGLINKFileEditor.UserInterface
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelMain = new System.Windows.Forms.ToolStripLabel();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemFind = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZoomPlus = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemZoomDown = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxEditor = new FastColoredTextBoxNS.FastColoredTextBox();
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemFind = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSelAll = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxAddText = new System.Windows.Forms.TextBox();
            this.buttonAddText = new System.Windows.Forms.Button();
            this.timerEvent = new System.Windows.Forms.Timer(this.components);
            this.FormThemeApplier = new WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender(this.components);
            this.DockPanelTheme = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.toolStripMain.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxEditor)).BeginInit();
            this.contextMenuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelMain});
            this.toolStripMain.Location = new System.Drawing.Point(0, 612);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(933, 25);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripLabelMain
            // 
            this.toolStripLabelMain.Name = "toolStripLabelMain";
            this.toolStripLabelMain.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabelMain.Text = "就绪.";
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.menuEdit,
            this.查看ToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(933, 25);
            this.menuStripMain.TabIndex = 1;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNew,
            this.menuItemOpen,
            this.menuItemSaveFile,
            this.menuItemSaveAs,
            this.toolStripMenuItem1,
            this.menuItemQuit});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(F)";
            // 
            // menuItemNew
            // 
            this.menuItemNew.Image = global::RAGLINKFileEditor.Properties.Resources._new;
            this.menuItemNew.Name = "menuItemNew";
            this.menuItemNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuItemNew.Size = new System.Drawing.Size(190, 22);
            this.menuItemNew.Text = "新建(N)";
            this.menuItemNew.Click += new System.EventHandler(this.MenuItemNew_Click);
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Image = global::RAGLINKFileEditor.Properties.Resources.open;
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuItemOpen.Size = new System.Drawing.Size(190, 22);
            this.menuItemOpen.Text = "打开(O)...";
            this.menuItemOpen.Click += new System.EventHandler(this.MenuItemOpen_Click);
            // 
            // menuItemSaveFile
            // 
            this.menuItemSaveFile.Image = global::RAGLINKFileEditor.Properties.Resources.save;
            this.menuItemSaveFile.Name = "menuItemSaveFile";
            this.menuItemSaveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItemSaveFile.Size = new System.Drawing.Size(190, 22);
            this.menuItemSaveFile.Text = "保存(S)";
            this.menuItemSaveFile.Click += new System.EventHandler(this.MenuItemSaveFile_Click);
            // 
            // menuItemSaveAs
            // 
            this.menuItemSaveAs.Name = "menuItemSaveAs";
            this.menuItemSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.menuItemSaveAs.Size = new System.Drawing.Size(190, 22);
            this.menuItemSaveAs.Text = "另存为";
            this.menuItemSaveAs.Click += new System.EventHandler(this.MenuItemSaveAs_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(187, 6);
            // 
            // menuItemQuit
            // 
            this.menuItemQuit.Name = "menuItemQuit";
            this.menuItemQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuItemQuit.Size = new System.Drawing.Size(190, 22);
            this.menuItemQuit.Text = "退出(X)";
            this.menuItemQuit.Click += new System.EventHandler(this.MenuItemQuit_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemUndo,
            this.menuItemRedo,
            this.toolStripMenuItem2,
            this.menuItemCopy,
            this.menuItemPaste,
            this.menuItemCut,
            this.menuItemDelete,
            this.toolStripMenuItem3,
            this.menuItemFind,
            this.menuItemReplace,
            this.toolStripMenuItem4,
            this.menuItemSelectAll});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(59, 21);
            this.menuEdit.Text = "编辑(E)";
            // 
            // menuItemUndo
            // 
            this.menuItemUndo.Image = global::RAGLINKFileEditor.Properties.Resources.undo;
            this.menuItemUndo.Name = "menuItemUndo";
            this.menuItemUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuItemUndo.Size = new System.Drawing.Size(180, 22);
            this.menuItemUndo.Text = "撤销(U)";
            this.menuItemUndo.Click += new System.EventHandler(this.MenuItemUndo_Click);
            // 
            // menuItemRedo
            // 
            this.menuItemRedo.Image = global::RAGLINKFileEditor.Properties.Resources.redo;
            this.menuItemRedo.Name = "menuItemRedo";
            this.menuItemRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.menuItemRedo.Size = new System.Drawing.Size(180, 22);
            this.menuItemRedo.Text = "重做(K)";
            this.menuItemRedo.Click += new System.EventHandler(this.MenuItemRedo_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // menuItemCopy
            // 
            this.menuItemCopy.Image = global::RAGLINKFileEditor.Properties.Resources.copy;
            this.menuItemCopy.Name = "menuItemCopy";
            this.menuItemCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuItemCopy.Size = new System.Drawing.Size(180, 22);
            this.menuItemCopy.Text = "复制(C)";
            this.menuItemCopy.Click += new System.EventHandler(this.MenuItemCopy_Click);
            // 
            // menuItemPaste
            // 
            this.menuItemPaste.Image = global::RAGLINKFileEditor.Properties.Resources.paste;
            this.menuItemPaste.Name = "menuItemPaste";
            this.menuItemPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuItemPaste.Size = new System.Drawing.Size(180, 22);
            this.menuItemPaste.Text = "粘贴(P)";
            this.menuItemPaste.Click += new System.EventHandler(this.MenuItemPaste_Click);
            // 
            // menuItemCut
            // 
            this.menuItemCut.Image = global::RAGLINKFileEditor.Properties.Resources.cut;
            this.menuItemCut.Name = "menuItemCut";
            this.menuItemCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuItemCut.Size = new System.Drawing.Size(180, 22);
            this.menuItemCut.Text = "剪切(T)";
            this.menuItemCut.Click += new System.EventHandler(this.MenuItemCut_Click);
            // 
            // menuItemDelete
            // 
            this.menuItemDelete.Name = "menuItemDelete";
            this.menuItemDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.menuItemDelete.Size = new System.Drawing.Size(180, 22);
            this.menuItemDelete.Text = "删除(D)";
            this.menuItemDelete.Click += new System.EventHandler(this.MenuItemDelete_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // menuItemFind
            // 
            this.menuItemFind.Image = global::RAGLINKFileEditor.Properties.Resources.find;
            this.menuItemFind.Name = "menuItemFind";
            this.menuItemFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menuItemFind.Size = new System.Drawing.Size(180, 22);
            this.menuItemFind.Text = "查找(S)";
            this.menuItemFind.Click += new System.EventHandler(this.MenuItemFind_Click);
            // 
            // menuItemReplace
            // 
            this.menuItemReplace.Name = "menuItemReplace";
            this.menuItemReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.menuItemReplace.Size = new System.Drawing.Size(180, 22);
            this.menuItemReplace.Text = "替换(R)";
            this.menuItemReplace.Click += new System.EventHandler(this.MenuItemReplace_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(177, 6);
            // 
            // menuItemSelectAll
            // 
            this.menuItemSelectAll.Name = "menuItemSelectAll";
            this.menuItemSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuItemSelectAll.Size = new System.Drawing.Size(180, 22);
            this.menuItemSelectAll.Text = "全选(A)";
            this.menuItemSelectAll.Click += new System.EventHandler(this.MenuItemSelectAll_Click);
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemZoomPlus,
            this.menuItemZoomDown});
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.查看ToolStripMenuItem.Text = "查看(V)";
            // 
            // menuItemZoomPlus
            // 
            this.menuItemZoomPlus.Name = "menuItemZoomPlus";
            this.menuItemZoomPlus.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up)));
            this.menuItemZoomPlus.Size = new System.Drawing.Size(183, 22);
            this.menuItemZoomPlus.Text = "放大(I)";
            this.menuItemZoomPlus.Click += new System.EventHandler(this.MenuItemZoomPlus_Click);
            // 
            // menuItemZoomDown
            // 
            this.menuItemZoomDown.Name = "menuItemZoomDown";
            this.menuItemZoomDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)));
            this.menuItemZoomDown.Size = new System.Drawing.Size(183, 22);
            this.menuItemZoomDown.Text = "缩小(O)";
            this.menuItemZoomDown.Click += new System.EventHandler(this.MenuItemZoomDown_Click);
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAbout});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(H)";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.menuItemAbout.Size = new System.Drawing.Size(180, 22);
            this.menuItemAbout.Text = "关于...";
            this.menuItemAbout.Click += new System.EventHandler(this.MenuItemAbout_Click);
            // 
            // textBoxEditor
            // 
            this.textBoxEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEditor.AutoCompleteBrackets = true;
            this.textBoxEditor.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.textBoxEditor.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.textBoxEditor.AutoScrollMinSize = new System.Drawing.Size(31, 22);
            this.textBoxEditor.BackBrush = null;
            this.textBoxEditor.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.textBoxEditor.CharCnWidth = 21;
            this.textBoxEditor.CharHeight = 22;
            this.textBoxEditor.CharWidth = 10;
            this.textBoxEditor.ContextMenuStrip = this.contextMenuStripMain;
            this.textBoxEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxEditor.Font = new System.Drawing.Font("Courier New", 12F);
            this.textBoxEditor.IsReplaceMode = false;
            this.textBoxEditor.LeftBracket = '[';
            this.textBoxEditor.LineInterval = 4;
            this.textBoxEditor.Location = new System.Drawing.Point(0, 25);
            this.textBoxEditor.Name = "textBoxEditor";
            this.textBoxEditor.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxEditor.RightBracket = ']';
            this.textBoxEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.textBoxEditor.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textBoxEditor.ServiceColors")));
            this.textBoxEditor.Size = new System.Drawing.Size(933, 559);
            this.textBoxEditor.TabIndex = 2;
            this.textBoxEditor.Zoom = 100;
            this.textBoxEditor.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.TextBoxEditor_TextChanged);
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemPaste,
            this.toolStripMenuItemCut,
            this.toolStripMenuItemDel,
            this.toolStripSeparator2,
            this.toolStripMenuItemFind,
            this.toolStripMenuItemReplace,
            this.toolStripSeparator3,
            this.toolStripMenuItemSelAll});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(118, 170);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Image = global::RAGLINKFileEditor.Properties.Resources.copy;
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemCopy.Text = "复制(C)";
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.MenuItemCopy_Click);
            // 
            // toolStripMenuItemPaste
            // 
            this.toolStripMenuItemPaste.Image = global::RAGLINKFileEditor.Properties.Resources.paste;
            this.toolStripMenuItemPaste.Name = "toolStripMenuItemPaste";
            this.toolStripMenuItemPaste.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemPaste.Text = "粘贴(P)";
            this.toolStripMenuItemPaste.Click += new System.EventHandler(this.MenuItemPaste_Click);
            // 
            // toolStripMenuItemCut
            // 
            this.toolStripMenuItemCut.Image = global::RAGLINKFileEditor.Properties.Resources.cut;
            this.toolStripMenuItemCut.Name = "toolStripMenuItemCut";
            this.toolStripMenuItemCut.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemCut.Text = "剪切(T)";
            this.toolStripMenuItemCut.Click += new System.EventHandler(this.MenuItemCut_Click);
            // 
            // toolStripMenuItemDel
            // 
            this.toolStripMenuItemDel.Name = "toolStripMenuItemDel";
            this.toolStripMenuItemDel.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemDel.Text = "删除(D)";
            this.toolStripMenuItemDel.Click += new System.EventHandler(this.MenuItemDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(114, 6);
            // 
            // toolStripMenuItemFind
            // 
            this.toolStripMenuItemFind.Image = global::RAGLINKFileEditor.Properties.Resources.find;
            this.toolStripMenuItemFind.Name = "toolStripMenuItemFind";
            this.toolStripMenuItemFind.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemFind.Text = "查找(S)";
            this.toolStripMenuItemFind.Click += new System.EventHandler(this.MenuItemFind_Click);
            // 
            // toolStripMenuItemReplace
            // 
            this.toolStripMenuItemReplace.Name = "toolStripMenuItemReplace";
            this.toolStripMenuItemReplace.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemReplace.Text = "替换(R)";
            this.toolStripMenuItemReplace.Click += new System.EventHandler(this.MenuItemReplace_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(114, 6);
            // 
            // toolStripMenuItemSelAll
            // 
            this.toolStripMenuItemSelAll.Name = "toolStripMenuItemSelAll";
            this.toolStripMenuItemSelAll.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemSelAll.Text = "全选(A)";
            this.toolStripMenuItemSelAll.Click += new System.EventHandler(this.MenuItemSelectAll_Click);
            // 
            // textBoxAddText
            // 
            this.textBoxAddText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAddText.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxAddText.Location = new System.Drawing.Point(3, 588);
            this.textBoxAddText.Name = "textBoxAddText";
            this.textBoxAddText.Size = new System.Drawing.Size(849, 23);
            this.textBoxAddText.TabIndex = 3;
            // 
            // buttonAddText
            // 
            this.buttonAddText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddText.Location = new System.Drawing.Point(855, 588);
            this.buttonAddText.Name = "buttonAddText";
            this.buttonAddText.Size = new System.Drawing.Size(75, 23);
            this.buttonAddText.TabIndex = 4;
            this.buttonAddText.Text = "插入";
            this.buttonAddText.UseVisualStyleBackColor = true;
            this.buttonAddText.Click += new System.EventHandler(this.ButtonAddText_Click);
            // 
            // timerEvent
            // 
            this.timerEvent.Enabled = true;
            this.timerEvent.Interval = 500;
            this.timerEvent.Tick += new System.EventHandler(this.TimerEvent_Tick);
            // 
            // FormThemeApplier
            // 
            this.FormThemeApplier.DefaultRenderer = null;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 637);
            this.Controls.Add(this.buttonAddText);
            this.Controls.Add(this.textBoxAddText);
            this.Controls.Add(this.textBoxEditor);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStripMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RAGLINK 文件编辑器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxEditor)).EndInit();
            this.contextMenuStripMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripLabel toolStripLabelMain;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemNew;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItemQuit;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuItemUndo;
        private System.Windows.Forms.ToolStripMenuItem menuItemRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem menuItemPaste;
        private System.Windows.Forms.ToolStripMenuItem menuItemCut;
        private System.Windows.Forms.ToolStripMenuItem menuItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem menuItemFind;
        private System.Windows.Forms.ToolStripMenuItem menuItemReplace;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem menuItemSelectAll;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemZoomPlus;
        private System.Windows.Forms.ToolStripMenuItem menuItemZoomDown;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private FastColoredTextBoxNS.FastColoredTextBox textBoxEditor;
        private System.Windows.Forms.TextBox textBoxAddText;
        private System.Windows.Forms.Button buttonAddText;
        private System.Windows.Forms.Timer timerEvent;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPaste;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCut;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFind;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReplace;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSelAll;
        private WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender FormThemeApplier;
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme DockPanelTheme;
    }
}

