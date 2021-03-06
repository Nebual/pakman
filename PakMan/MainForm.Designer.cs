﻿namespace PakMan
{
    partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.revertButton = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.logBox = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.steamRestartTickbox = new System.Windows.Forms.CheckBox();
			this.itemList = new System.Windows.Forms.DataGridView();
			this.D = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.I = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.gameName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.archiveSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gameSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uploadGamesButton = new System.Windows.Forms.Button();
			this.purgeMappingsButton = new System.Windows.Forms.Button();
			this.descriptionTextBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.imageButton = new PakMan.AutoScaleButton();
			this.dependsTextBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.buildArchiveButton = new System.Windows.Forms.Button();
			this.targetExeTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.saveFolderTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.installFolderTextBox = new System.Windows.Forms.TextBox();
			this.saveMappings = new System.Windows.Forms.Button();
			this.addNewItemButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.archiveBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setSteamUsernameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setInstallDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.itemList)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// revertButton
			// 
			this.revertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.revertButton.Location = new System.Drawing.Point(3, 340);
			this.revertButton.Name = "revertButton";
			this.revertButton.Size = new System.Drawing.Size(75, 23);
			this.revertButton.TabIndex = 20;
			this.revertButton.Text = "Revert";
			this.revertButton.UseVisualStyleBackColor = true;
			this.revertButton.Click += new System.EventHandler(this.revertButton_Click);
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.applyButton.Location = new System.Drawing.Point(84, 340);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(75, 23);
			this.applyButton.TabIndex = 21;
			this.applyButton.Text = "Apply";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new System.EventHandler(this.apply_Click);
			// 
			// logBox
			// 
			this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logBox.Location = new System.Drawing.Point(0, 0);
			this.logBox.Multiline = true;
			this.logBox.Name = "logBox";
			this.logBox.ReadOnly = true;
			this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.logBox.Size = new System.Drawing.Size(815, 157);
			this.logBox.TabIndex = 3;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.logBox);
			this.splitContainer1.Size = new System.Drawing.Size(815, 553);
			this.splitContainer1.SplitterDistance = 392;
			this.splitContainer1.TabIndex = 5;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer2.Location = new System.Drawing.Point(0, 24);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.steamRestartTickbox);
			this.splitContainer2.Panel1.Controls.Add(this.itemList);
			this.splitContainer2.Panel1.Controls.Add(this.applyButton);
			this.splitContainer2.Panel1.Controls.Add(this.revertButton);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.uploadGamesButton);
			this.splitContainer2.Panel2.Controls.Add(this.purgeMappingsButton);
			this.splitContainer2.Panel2.Controls.Add(this.descriptionTextBox);
			this.splitContainer2.Panel2.Controls.Add(this.label7);
			this.splitContainer2.Panel2.Controls.Add(this.imageButton);
			this.splitContainer2.Panel2.Controls.Add(this.dependsTextBox);
			this.splitContainer2.Panel2.Controls.Add(this.label6);
			this.splitContainer2.Panel2.Controls.Add(this.buildArchiveButton);
			this.splitContainer2.Panel2.Controls.Add(this.targetExeTextBox);
			this.splitContainer2.Panel2.Controls.Add(this.label5);
			this.splitContainer2.Panel2.Controls.Add(this.label4);
			this.splitContainer2.Panel2.Controls.Add(this.saveFolderTextBox);
			this.splitContainer2.Panel2.Controls.Add(this.label3);
			this.splitContainer2.Panel2.Controls.Add(this.installFolderTextBox);
			this.splitContainer2.Panel2.Controls.Add(this.saveMappings);
			this.splitContainer2.Panel2.Controls.Add(this.addNewItemButton);
			this.splitContainer2.Panel2.Controls.Add(this.label2);
			this.splitContainer2.Panel2.Controls.Add(this.archiveBox);
			this.splitContainer2.Panel2.Controls.Add(this.label1);
			this.splitContainer2.Panel2.Controls.Add(this.nameBox);
			this.splitContainer2.Size = new System.Drawing.Size(815, 366);
			this.splitContainer2.SplitterDistance = 523;
			this.splitContainer2.TabIndex = 3;
			// 
			// steamRestartTickbox
			// 
			this.steamRestartTickbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.steamRestartTickbox.AutoSize = true;
			this.steamRestartTickbox.Location = new System.Drawing.Point(402, 346);
			this.steamRestartTickbox.Name = "steamRestartTickbox";
			this.steamRestartTickbox.Size = new System.Drawing.Size(118, 17);
			this.steamRestartTickbox.TabIndex = 557;
			this.steamRestartTickbox.Text = "Auto Restart Steam";
			this.steamRestartTickbox.UseVisualStyleBackColor = true;
			this.steamRestartTickbox.CheckedChanged += new System.EventHandler(this.steamRestartTickbox_CheckedChanged);
			// 
			// itemList
			// 
			this.itemList.AllowUserToAddRows = false;
			this.itemList.AllowUserToDeleteRows = false;
			this.itemList.AllowUserToResizeRows = false;
			this.itemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.itemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.itemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.D,
            this.I,
            this.gameName,
            this.Description,
            this.archiveSize,
            this.gameSize});
			this.itemList.Location = new System.Drawing.Point(4, 4);
			this.itemList.MultiSelect = false;
			this.itemList.Name = "itemList";
			this.itemList.RowHeadersVisible = false;
			this.itemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.itemList.ShowEditingIcon = false;
			this.itemList.Size = new System.Drawing.Size(516, 330);
			this.itemList.TabIndex = 555;
			this.itemList.SelectionChanged += new System.EventHandler(this.itemList_SelectionChanged);
			// 
			// D
			// 
			this.D.HeaderText = "D";
			this.D.Name = "D";
			this.D.Width = 20;
			// 
			// I
			// 
			this.I.HeaderText = "I";
			this.I.Name = "I";
			this.I.Width = 20;
			// 
			// gameName
			// 
			this.gameName.HeaderText = "Name";
			this.gameName.Name = "gameName";
			this.gameName.ReadOnly = true;
			this.gameName.Width = 150;
			// 
			// Description
			// 
			this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Description.HeaderText = "Description";
			this.Description.Name = "Description";
			this.Description.ReadOnly = true;
			// 
			// archiveSize
			// 
			this.archiveSize.HeaderText = "7z Size";
			this.archiveSize.Name = "archiveSize";
			this.archiveSize.ReadOnly = true;
			this.archiveSize.Width = 70;
			// 
			// gameSize
			// 
			this.gameSize.HeaderText = "G Size";
			this.gameSize.Name = "gameSize";
			this.gameSize.ReadOnly = true;
			this.gameSize.Width = 70;
			// 
			// uploadGamesButton
			// 
			this.uploadGamesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.uploadGamesButton.Location = new System.Drawing.Point(167, 339);
			this.uploadGamesButton.Name = "uploadGamesButton";
			this.uploadGamesButton.Size = new System.Drawing.Size(106, 23);
			this.uploadGamesButton.TabIndex = 190;
			this.uploadGamesButton.Text = "Upload games.json";
			this.uploadGamesButton.UseVisualStyleBackColor = true;
			this.uploadGamesButton.Click += new System.EventHandler(this.uploadGamesButton_Click);
			// 
			// purgeMappingsButton
			// 
			this.purgeMappingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.purgeMappingsButton.Location = new System.Drawing.Point(138, 339);
			this.purgeMappingsButton.Name = "purgeMappingsButton";
			this.purgeMappingsButton.Size = new System.Drawing.Size(23, 23);
			this.purgeMappingsButton.TabIndex = 189;
			this.purgeMappingsButton.Text = "X";
			this.purgeMappingsButton.UseVisualStyleBackColor = true;
			this.purgeMappingsButton.Click += new System.EventHandler(this.purgeMappingsButton_Click);
			// 
			// descriptionTextBox
			// 
			this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.descriptionTextBox.Location = new System.Drawing.Point(71, 161);
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.Size = new System.Drawing.Size(202, 20);
			this.descriptionTextBox.TabIndex = 8;
			this.descriptionTextBox.TextChanged += new System.EventHandler(this.descriptionTextBox_TextChanged);
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 164);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(63, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = "Description:";
			// 
			// imageButton
			// 
			this.imageButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.imageButton.AutoScaleBorder = 0;
			this.imageButton.AutoScaleImage = null;
			this.imageButton.Location = new System.Drawing.Point(12, 216);
			this.imageButton.Name = "imageButton";
			this.imageButton.Size = new System.Drawing.Size(261, 118);
			this.imageButton.TabIndex = 10;
			this.imageButton.Text = "Image";
			this.imageButton.UseVisualStyleBackColor = true;
			this.imageButton.Click += new System.EventHandler(this.imageButton_Click);
			// 
			// dependsTextBox
			// 
			this.dependsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dependsTextBox.Location = new System.Drawing.Point(71, 135);
			this.dependsTextBox.Name = "dependsTextBox";
			this.dependsTextBox.Size = new System.Drawing.Size(202, 20);
			this.dependsTextBox.TabIndex = 7;
			this.dependsTextBox.TextChanged += new System.EventHandler(this.dependsTextBox_TextChanged);
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(18, 138);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 13);
			this.label6.TabIndex = 13;
			this.label6.Text = "Depends:";
			// 
			// buildArchiveButton
			// 
			this.buildArchiveButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buildArchiveButton.Cursor = System.Windows.Forms.Cursors.Default;
			this.buildArchiveButton.Location = new System.Drawing.Point(12, 187);
			this.buildArchiveButton.Name = "buildArchiveButton";
			this.buildArchiveButton.Size = new System.Drawing.Size(261, 23);
			this.buildArchiveButton.TabIndex = 9;
			this.buildArchiveButton.Text = "Browse, Compress, and Upload";
			this.buildArchiveButton.UseVisualStyleBackColor = true;
			this.buildArchiveButton.Click += new System.EventHandler(this.buildArchiveButton_Click);
			// 
			// targetExeTextBox
			// 
			this.targetExeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.targetExeTextBox.Location = new System.Drawing.Point(71, 109);
			this.targetExeTextBox.Name = "targetExeTextBox";
			this.targetExeTextBox.Size = new System.Drawing.Size(202, 20);
			this.targetExeTextBox.TabIndex = 6;
			this.targetExeTextBox.TextChanged += new System.EventHandler(this.targetExeTextBox_TextChanged);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(9, 112);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(62, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Target Exe:";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(-1, 86);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Saves Folder:";
			// 
			// saveFolderTextBox
			// 
			this.saveFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.saveFolderTextBox.Location = new System.Drawing.Point(71, 83);
			this.saveFolderTextBox.Name = "saveFolderTextBox";
			this.saveFolderTextBox.Size = new System.Drawing.Size(202, 20);
			this.saveFolderTextBox.TabIndex = 5;
			this.saveFolderTextBox.TextChanged += new System.EventHandler(this.saveFolderTextBox_TextChanged);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(2, 60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(69, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Install Folder:";
			// 
			// installFolderTextBox
			// 
			this.installFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.installFolderTextBox.Location = new System.Drawing.Point(71, 57);
			this.installFolderTextBox.Name = "installFolderTextBox";
			this.installFolderTextBox.Size = new System.Drawing.Size(202, 20);
			this.installFolderTextBox.TabIndex = 4;
			this.installFolderTextBox.TextChanged += new System.EventHandler(this.installFolderTextBox_TextChanged);
			// 
			// saveMappings
			// 
			this.saveMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.saveMappings.Location = new System.Drawing.Point(12, 340);
			this.saveMappings.Name = "saveMappings";
			this.saveMappings.Size = new System.Drawing.Size(120, 22);
			this.saveMappings.TabIndex = 11;
			this.saveMappings.Text = "Save games.json";
			this.saveMappings.UseVisualStyleBackColor = true;
			this.saveMappings.Click += new System.EventHandler(this.saveMappings_Click);
			// 
			// addNewItemButton
			// 
			this.addNewItemButton.Location = new System.Drawing.Point(4, 4);
			this.addNewItemButton.Name = "addNewItemButton";
			this.addNewItemButton.Size = new System.Drawing.Size(22, 22);
			this.addNewItemButton.TabIndex = 1;
			this.addNewItemButton.Text = "+";
			this.addNewItemButton.UseVisualStyleBackColor = true;
			this.addNewItemButton.Click += new System.EventHandler(this.addNewItemButton_Click);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(25, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Archive:";
			// 
			// archiveBox
			// 
			this.archiveBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.archiveBox.Location = new System.Drawing.Point(71, 31);
			this.archiveBox.Name = "archiveBox";
			this.archiveBox.Size = new System.Drawing.Size(202, 20);
			this.archiveBox.TabIndex = 3;
			this.archiveBox.TextChanged += new System.EventHandler(this.archiveBox_TextChanged);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(33, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Name:";
			// 
			// nameBox
			// 
			this.nameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameBox.Location = new System.Drawing.Point(71, 5);
			this.nameBox.Name = "nameBox";
			this.nameBox.Size = new System.Drawing.Size(202, 20);
			this.nameBox.TabIndex = 2;
			this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(815, 24);
			this.menuStrip1.TabIndex = 4;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setSteamUsernameToolStripMenuItem,
            this.setInstallDirectoryToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// setSteamUsernameToolStripMenuItem
			// 
			this.setSteamUsernameToolStripMenuItem.Name = "setSteamUsernameToolStripMenuItem";
			this.setSteamUsernameToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.setSteamUsernameToolStripMenuItem.Text = "Set Steam Username";
			this.setSteamUsernameToolStripMenuItem.Click += new System.EventHandler(this.setSteamUsernameToolStripMenuItem_Click);
			// 
			// setInstallDirectoryToolStripMenuItem
			// 
			this.setInstallDirectoryToolStripMenuItem.Name = "setInstallDirectoryToolStripMenuItem";
			this.setInstallDirectoryToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.setInstallDirectoryToolStripMenuItem.Text = "Set Install Directory";
			this.setInstallDirectoryToolStripMenuItem.Click += new System.EventHandler(this.setInstallDirectoryToolStripMenuItem_Click);
			// 
			// checkForUpdatesToolStripMenuItem
			// 
			this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
			this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
			this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
			// 
			// folderBrowserDialog1
			// 
			this.folderBrowserDialog1.Description = "Select the game\'s folder, to be 7z\'d and uploaded";
			this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.AddExtension = false;
			this.saveFileDialog1.CheckPathExists = false;
			this.saveFileDialog1.FileName = "Open Folder";
			this.saveFileDialog1.OverwritePrompt = false;
			this.saveFileDialog1.ValidateNames = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(815, 553);
			this.Controls.Add(this.splitContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "PakMan";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.itemList)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Button revertButton;
        private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.TextBox logBox;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox archiveBox;
		private System.Windows.Forms.Button addNewItemButton;
		private System.Windows.Forms.Button saveMappings;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox saveFolderTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox installFolderTextBox;
		private System.Windows.Forms.DataGridView itemList;
		private System.Windows.Forms.TextBox targetExeTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buildArchiveButton;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setSteamUsernameToolStripMenuItem;
		private AutoScaleButton imageButton;
		private System.Windows.Forms.TextBox dependsTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TextBox descriptionTextBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DataGridViewCheckBoxColumn D;
		private System.Windows.Forms.DataGridViewCheckBoxColumn I;
		private System.Windows.Forms.DataGridViewTextBoxColumn gameName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Description;
		private System.Windows.Forms.DataGridViewTextBoxColumn archiveSize;
		private System.Windows.Forms.DataGridViewTextBoxColumn gameSize;
		private System.Windows.Forms.Button purgeMappingsButton;
		private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setInstallDirectoryToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Button uploadGamesButton;
		internal System.Windows.Forms.CheckBox steamRestartTickbox;
    }
}

