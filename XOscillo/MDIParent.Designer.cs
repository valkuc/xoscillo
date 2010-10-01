namespace XOscillo
{
	partial class MDIParent
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent));
         this.menuStrip = new System.Windows.Forms.MenuStrip();
         this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
         this.newParallax = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
         this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
         this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.printSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
         this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
         this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
         this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
         this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.timeMenu = new System.Windows.Forms.ToolStripMenuItem();
         this.decodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.decodeBeeperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.decodeFrequencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
         this.newWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.arrangeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.statusStrip = new System.Windows.Forms.StatusStrip();
         this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
         this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
         this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
         this.menuStrip.SuspendLayout();
         this.statusStrip.SuspendLayout();
         this.SuspendLayout();
         // 
         // menuStrip
         // 
         this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu,
            this.timeMenu,
            this.windowsMenu,
            this.aboutToolStripMenuItem1});
         this.menuStrip.Location = new System.Drawing.Point(0, 0);
         this.menuStrip.MdiWindowListItem = this.windowsMenu;
         this.menuStrip.Name = "menuStrip";
         this.menuStrip.Size = new System.Drawing.Size(650, 24);
         this.menuStrip.TabIndex = 0;
         this.menuStrip.Text = "MenuStrip";
         // 
         // fileMenu
         // 
         this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newParallax,
            this.toolStripMenuItem1,
            this.openToolStripMenuItem,
            this.toolStripSeparator3,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator4,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.printSetupToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
         this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
         this.fileMenu.Name = "fileMenu";
         this.fileMenu.Size = new System.Drawing.Size(37, 20);
         this.fileMenu.Text = "&File";
         // 
         // newParallax
         // 
         this.newParallax.Image = ((System.Drawing.Image)(resources.GetObject("newParallax.Image")));
         this.newParallax.ImageTransparentColor = System.Drawing.Color.Black;
         this.newParallax.Name = "newParallax";
         this.newParallax.Size = new System.Drawing.Size(146, 22);
         this.newParallax.Text = "New Parallax";
         this.newParallax.Click += new System.EventHandler(this.newParallax_Click);
         // 
         // toolStripMenuItem1
         // 
         this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
         this.toolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Black;
         this.toolStripMenuItem1.Name = "toolStripMenuItem1";
         this.toolStripMenuItem1.Size = new System.Drawing.Size(146, 22);
         this.toolStripMenuItem1.Text = "New Arduino";
         this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
         // 
         // openToolStripMenuItem
         // 
         this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
         this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
         this.openToolStripMenuItem.Name = "openToolStripMenuItem";
         this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
         this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
         this.openToolStripMenuItem.Text = "&Open";
         this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenFile);
         // 
         // toolStripSeparator3
         // 
         this.toolStripSeparator3.Name = "toolStripSeparator3";
         this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
         // 
         // saveToolStripMenuItem
         // 
         this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
         this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
         this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
         this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
         this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
         this.saveToolStripMenuItem.Text = "&Save";
         // 
         // saveAsToolStripMenuItem
         // 
         this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
         this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
         this.saveAsToolStripMenuItem.Text = "Save &As";
         this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
         // 
         // toolStripSeparator4
         // 
         this.toolStripSeparator4.Name = "toolStripSeparator4";
         this.toolStripSeparator4.Size = new System.Drawing.Size(143, 6);
         // 
         // printToolStripMenuItem
         // 
         this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
         this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
         this.printToolStripMenuItem.Name = "printToolStripMenuItem";
         this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
         this.printToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
         this.printToolStripMenuItem.Text = "&Print";
         // 
         // printPreviewToolStripMenuItem
         // 
         this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
         this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
         this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
         this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
         this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
         // 
         // printSetupToolStripMenuItem
         // 
         this.printSetupToolStripMenuItem.Name = "printSetupToolStripMenuItem";
         this.printSetupToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
         this.printSetupToolStripMenuItem.Text = "Print Setup";
         // 
         // toolStripSeparator5
         // 
         this.toolStripSeparator5.Name = "toolStripSeparator5";
         this.toolStripSeparator5.Size = new System.Drawing.Size(143, 6);
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
         this.exitToolStripMenuItem.Text = "E&xit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
         // 
         // editMenu
         // 
         this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator6,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator7,
            this.selectAllToolStripMenuItem});
         this.editMenu.Name = "editMenu";
         this.editMenu.Size = new System.Drawing.Size(39, 20);
         this.editMenu.Text = "&Edit";
         // 
         // undoToolStripMenuItem
         // 
         this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
         this.undoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
         this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
         this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
         this.undoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
         this.undoToolStripMenuItem.Text = "&Undo";
         // 
         // redoToolStripMenuItem
         // 
         this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
         this.redoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
         this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
         this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
         this.redoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
         this.redoToolStripMenuItem.Text = "&Redo";
         // 
         // toolStripSeparator6
         // 
         this.toolStripSeparator6.Name = "toolStripSeparator6";
         this.toolStripSeparator6.Size = new System.Drawing.Size(161, 6);
         // 
         // cutToolStripMenuItem
         // 
         this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
         this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
         this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
         this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
         this.cutToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
         this.cutToolStripMenuItem.Text = "Cu&t";
         this.cutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
         // 
         // copyToolStripMenuItem
         // 
         this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
         this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
         this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
         this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
         this.copyToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
         this.copyToolStripMenuItem.Text = "&Copy";
         this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
         // 
         // pasteToolStripMenuItem
         // 
         this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
         this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
         this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
         this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
         this.pasteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
         this.pasteToolStripMenuItem.Text = "&Paste";
         this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
         // 
         // toolStripSeparator7
         // 
         this.toolStripSeparator7.Name = "toolStripSeparator7";
         this.toolStripSeparator7.Size = new System.Drawing.Size(161, 6);
         // 
         // selectAllToolStripMenuItem
         // 
         this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
         this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
         this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
         this.selectAllToolStripMenuItem.Text = "Select &All";
         // 
         // viewMenu
         // 
         this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.statusBarToolStripMenuItem});
         this.viewMenu.Name = "viewMenu";
         this.viewMenu.Size = new System.Drawing.Size(44, 20);
         this.viewMenu.Text = "&View";
         // 
         // toolBarToolStripMenuItem
         // 
         this.toolBarToolStripMenuItem.Checked = true;
         this.toolBarToolStripMenuItem.CheckOnClick = true;
         this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
         this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
         this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
         this.toolBarToolStripMenuItem.Text = "&Toolbar";
         this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
         // 
         // statusBarToolStripMenuItem
         // 
         this.statusBarToolStripMenuItem.Checked = true;
         this.statusBarToolStripMenuItem.CheckOnClick = true;
         this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
         this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
         this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
         this.statusBarToolStripMenuItem.Text = "&Status Bar";
         this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.StatusBarToolStripMenuItem_Click);
         // 
         // timeMenu
         // 
         this.timeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.decodeToolStripMenuItem,
            this.decodeBeeperToolStripMenuItem,
            this.decodeFrequencyToolStripMenuItem});
         this.timeMenu.Name = "timeMenu";
         this.timeMenu.Size = new System.Drawing.Size(48, 20);
         this.timeMenu.Text = "Tools";
         // 
         // decodeToolStripMenuItem
         // 
         this.decodeToolStripMenuItem.Name = "decodeToolStripMenuItem";
         this.decodeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
         this.decodeToolStripMenuItem.Text = "Decode FSK";
         this.decodeToolStripMenuItem.Click += new System.EventHandler(this.decodeToolStripMenuItem_Click);
         // 
         // decodeBeeperToolStripMenuItem
         // 
         this.decodeBeeperToolStripMenuItem.Name = "decodeBeeperToolStripMenuItem";
         this.decodeBeeperToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
         this.decodeBeeperToolStripMenuItem.Text = "Decode Beeper";
         this.decodeBeeperToolStripMenuItem.Click += new System.EventHandler(this.decodeBeeperToolStripMenuItem_Click);
         // 
         // decodeFrequencyToolStripMenuItem
         // 
         this.decodeFrequencyToolStripMenuItem.Name = "decodeFrequencyToolStripMenuItem";
         this.decodeFrequencyToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
         this.decodeFrequencyToolStripMenuItem.Text = "Decode Frequency";
         this.decodeFrequencyToolStripMenuItem.Click += new System.EventHandler(this.decodeFrequencyToolStripMenuItem_Click);
         // 
         // windowsMenu
         // 
         this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWindowToolStripMenuItem,
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.arrangeIconsToolStripMenuItem});
         this.windowsMenu.Name = "windowsMenu";
         this.windowsMenu.Size = new System.Drawing.Size(68, 20);
         this.windowsMenu.Text = "&Windows";
         // 
         // newWindowToolStripMenuItem
         // 
         this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
         this.newWindowToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
         this.newWindowToolStripMenuItem.Text = "&New Window";
         this.newWindowToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
         // 
         // cascadeToolStripMenuItem
         // 
         this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
         this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
         this.cascadeToolStripMenuItem.Text = "&Cascade";
         this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
         // 
         // tileVerticalToolStripMenuItem
         // 
         this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
         this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
         this.tileVerticalToolStripMenuItem.Text = "Tile &Vertical";
         this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
         // 
         // tileHorizontalToolStripMenuItem
         // 
         this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
         this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
         this.tileHorizontalToolStripMenuItem.Text = "Tile &Horizontal";
         this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
         // 
         // closeAllToolStripMenuItem
         // 
         this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
         this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
         this.closeAllToolStripMenuItem.Text = "C&lose All";
         this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
         // 
         // arrangeIconsToolStripMenuItem
         // 
         this.arrangeIconsToolStripMenuItem.Name = "arrangeIconsToolStripMenuItem";
         this.arrangeIconsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
         this.arrangeIconsToolStripMenuItem.Text = "&Arrange Icons";
         this.arrangeIconsToolStripMenuItem.Click += new System.EventHandler(this.ArrangeIconsToolStripMenuItem_Click);
         // 
         // statusStrip
         // 
         this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
         this.statusStrip.Location = new System.Drawing.Point(0, 300);
         this.statusStrip.Name = "statusStrip";
         this.statusStrip.Size = new System.Drawing.Size(650, 22);
         this.statusStrip.TabIndex = 2;
         this.statusStrip.Text = "StatusStrip";
         // 
         // toolStripStatusLabel
         // 
         this.toolStripStatusLabel.Name = "toolStripStatusLabel";
         this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
         this.toolStripStatusLabel.Text = "Status";
         // 
         // aboutToolStripMenuItem1
         // 
         this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
         this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
         this.aboutToolStripMenuItem1.Text = "About";
         this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
         // 
         // MDIParent
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(650, 322);
         this.Controls.Add(this.statusStrip);
         this.Controls.Add(this.menuStrip);
         this.IsMdiContainer = true;
         this.MainMenuStrip = this.menuStrip;
         this.Name = "MDIParent";
         this.Text = "XOscillo";
         this.menuStrip.ResumeLayout(false);
         this.menuStrip.PerformLayout();
         this.statusStrip.ResumeLayout(false);
         this.statusStrip.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

		}
		#endregion


      private System.Windows.Forms.MenuStrip menuStrip;
      private System.Windows.Forms.StatusStrip statusStrip;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
      private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
      private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editMenu;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewMenu;
		private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem windowsMenu;
		private System.Windows.Forms.ToolStripMenuItem newWindowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem arrangeIconsToolStripMenuItem;
		private System.Windows.Forms.ToolTip ToolTip;
      private System.Windows.Forms.ToolStripMenuItem timeMenu;
      private System.Windows.Forms.ToolStripMenuItem decodeToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem decodeBeeperToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem decodeFrequencyToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem fileMenu;
      private System.Windows.Forms.ToolStripMenuItem newParallax;
      private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
      private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
      private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
      private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem printSetupToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
	}
}



