using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakMan {

	public partial class TextPrompt : Form {
		public TextPrompt() {
			InitializeComponent();
		}

		SteamIDHolder steamID;

		public TextPrompt(string promptInstructions, SteamIDHolder steamID) {
			InitializeComponent();

			this.steamID = steamID;
			lblPrompt.Text = promptInstructions;
		}

		private void TextPrompt_Load(object sender, EventArgs e) {
			CenterToParent();
		}

		private void lookupButton_Click(object sender, EventArgs e) {
			Int32 res;
			if ((res = FileUtil.getSteamIDFromVanity(nameLookupBox.Text)) > 0) {
				steamID.steamID = res;
				Close();
			}
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			Close();
		}
	}
	public class SteamIDHolder {
		public Int32 steamID = 0;
	}
}
