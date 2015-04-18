namespace PakMan
{
	partial class TextPrompt
	{
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
			this.cancelButton = new System.Windows.Forms.Button();
			this.lookupButton = new System.Windows.Forms.Button();
			this.nameLookupBox = new System.Windows.Forms.TextBox();
			this.lblPrompt = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cancelButton.Location = new System.Drawing.Point(12, 56);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(131, 23);
			this.cancelButton.TabIndex = 0;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// lookupButton
			// 
			this.lookupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lookupButton.Location = new System.Drawing.Point(148, 56);
			this.lookupButton.Name = "lookupButton";
			this.lookupButton.Size = new System.Drawing.Size(123, 23);
			this.lookupButton.TabIndex = 1;
			this.lookupButton.Text = "Lookup";
			this.lookupButton.UseVisualStyleBackColor = true;
			this.lookupButton.Click += new System.EventHandler(this.lookupButton_Click);
			// 
			// nameLookupBox
			// 
			this.nameLookupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.nameLookupBox.Location = new System.Drawing.Point(12, 30);
			this.nameLookupBox.Name = "nameLookupBox";
			this.nameLookupBox.Size = new System.Drawing.Size(259, 20);
			this.nameLookupBox.TabIndex = 2;
			// 
			// lblPrompt
			// 
			this.lblPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblPrompt.AutoSize = true;
			this.lblPrompt.Location = new System.Drawing.Point(12, 14);
			this.lblPrompt.Name = "lblPrompt";
			this.lblPrompt.Size = new System.Drawing.Size(35, 13);
			this.lblPrompt.TabIndex = 3;
			this.lblPrompt.Text = "label1";
			// 
			// TextPrompt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(283, 91);
			this.Controls.Add(this.lblPrompt);
			this.Controls.Add(this.nameLookupBox);
			this.Controls.Add(this.lookupButton);
			this.Controls.Add(this.cancelButton);
			this.Name = "TextPrompt";
			this.Text = "SteamID Lookup";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button lookupButton;
		private System.Windows.Forms.TextBox nameLookupBox;
		private System.Windows.Forms.Label lblPrompt;
	}
}