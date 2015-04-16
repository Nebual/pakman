namespace PakMan
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
			this.button1 = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.logBox = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.installCheckList = new System.Windows.Forms.ListView();
			this.names = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(3, 300);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Revert";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.applyButton.Location = new System.Drawing.Point(84, 300);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(75, 23);
			this.applyButton.TabIndex = 2;
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
			this.logBox.Size = new System.Drawing.Size(544, 129);
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
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.logBox);
			this.splitContainer1.Size = new System.Drawing.Size(544, 459);
			this.splitContainer1.SplitterDistance = 326;
			this.splitContainer1.TabIndex = 5;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.installCheckList);
			this.splitContainer2.Panel1.Controls.Add(this.applyButton);
			this.splitContainer2.Panel1.Controls.Add(this.button1);
			// 
			// splitContainer2.Panel2
			// 
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
			this.splitContainer2.Size = new System.Drawing.Size(544, 326);
			this.splitContainer2.SplitterDistance = 330;
			this.splitContainer2.TabIndex = 3;
			// 
			// installCheckList
			// 
			this.installCheckList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.installCheckList.CheckBoxes = true;
			this.installCheckList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.names,
            this.size});
			this.installCheckList.FullRowSelect = true;
			this.installCheckList.GridLines = true;
			this.installCheckList.Location = new System.Drawing.Point(3, 3);
			this.installCheckList.MultiSelect = false;
			this.installCheckList.Name = "installCheckList";
			this.installCheckList.Size = new System.Drawing.Size(324, 291);
			this.installCheckList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.installCheckList.TabIndex = 1;
			this.installCheckList.UseCompatibleStateImageBehavior = false;
			this.installCheckList.View = System.Windows.Forms.View.Details;
			this.installCheckList.SelectedIndexChanged += new System.EventHandler(this.installCheckList_SelectedIndexChanged);
			// 
			// names
			// 
			this.names.Text = "Name";
			this.names.Width = 120;
			// 
			// size
			// 
			this.size.Text = "Size??";
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
			this.saveFolderTextBox.Size = new System.Drawing.Size(134, 20);
			this.saveFolderTextBox.TabIndex = 8;
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
			this.installFolderTextBox.Size = new System.Drawing.Size(134, 20);
			this.installFolderTextBox.TabIndex = 6;
			this.installFolderTextBox.TextChanged += new System.EventHandler(this.installFolderTextBox_TextChanged);
			// 
			// saveMappings
			// 
			this.saveMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.saveMappings.Location = new System.Drawing.Point(4, 300);
			this.saveMappings.Name = "saveMappings";
			this.saveMappings.Size = new System.Drawing.Size(122, 22);
			this.saveMappings.TabIndex = 5;
			this.saveMappings.Text = "Save mappings.json";
			this.saveMappings.UseVisualStyleBackColor = true;
			this.saveMappings.Click += new System.EventHandler(this.saveMappings_Click);
			// 
			// addNewItemButton
			// 
			this.addNewItemButton.Location = new System.Drawing.Point(4, 4);
			this.addNewItemButton.Name = "addNewItemButton";
			this.addNewItemButton.Size = new System.Drawing.Size(22, 22);
			this.addNewItemButton.TabIndex = 4;
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
			this.archiveBox.Size = new System.Drawing.Size(134, 20);
			this.archiveBox.TabIndex = 2;
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
			this.nameBox.Size = new System.Drawing.Size(134, 20);
			this.nameBox.TabIndex = 0;
			this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(544, 459);
			this.Controls.Add(this.splitContainer1);
			this.Name = "MainForm";
			this.Text = "PakMan";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button applyButton;
		private System.Windows.Forms.TextBox logBox;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ListView installCheckList;
		private System.Windows.Forms.ColumnHeader names;
		private System.Windows.Forms.ColumnHeader size;
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
    }
}

