using Newtonsoft.Json;
using SevenZip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakMan
{

	public partial class MainForm : Form
	{
		public MainForm() {
			InitializeComponent();
		}

		FileUtil F;
		bool dirty;
		Mapping mappings;
		IDictionary<string, GameMapping> gameLookup = new Dictionary<string, GameMapping>();

		private void MainForm_Load(object sender, EventArgs e) {
			F = new FileUtil(this);

			mappings = JsonConvert.DeserializeObject<Mapping>(File.ReadAllText("mappings.json"));
			foreach (GameMapping game in mappings.games) {
				gameLookup[game.name] = game;
				ListViewItem item = new ListViewItem(new string[] { game.name, "" });
				if (game.installfolder.Length > 0) item.Checked = Directory.Exists(game.installfolder);
				installCheckList.Items.Add(item);
			}

			/*State state = JsonConvert.DeserializeObject<State>(File.ReadAllText("state.json"));
			foreach (ListViewItem item in installCheckList.Items) {
				item.Checked = state.games[item.Text].ticked;
			}*/
		}

		private void apply_Click(object sender, EventArgs e) {
			foreach (ListViewItem row in installCheckList.CheckedItems) {
				string s = row.Text;
				log("Processing " + s);
				F.downloadArchive(gameLookup[s].filename);
				F.extractArchive(gameLookup[s].filename, gameLookup[s].installfolder);
			}
		}

		public void log(string text) {
			logBox.Text += text + "\r\n";
			logBox.ScrollToCaret();
		}

		private void setDirty(bool flag) {
			if (flag != dirty) {
				dirty = flag;
				if (dirty) {
					saveMappings.Text = "* " + saveMappings.Text;
				}
				else {
					saveMappings.Text = saveMappings.Text.TrimStart(new char[]{'*',' '});
				}
			}
		}

		private void installCheckList_SelectedIndexChanged(object sender, EventArgs e) {
			updateDescription(gameLookup[installCheckList.SelectedItems[0].Text]);
		}

		private void updateDescription(GameMapping game) {
			nameBox.Text = game.name;
			archiveBox.Text = game.filename;
			saveFolderTextBox.Text = game.savefolder;
			installFolderTextBox.Text = game.installfolder;
		}

		private void addNewItemButton_Click(object sender, EventArgs e) {
			if (!gameLookup.ContainsKey("New")) {
				GameMapping game = new GameMapping("New");
				mappings.games.Add(game);
				gameLookup[game.name] = game;
				installCheckList.Items.Add(new ListViewItem(new string[] { game.name, "" })).Selected = true;
				setDirty(true);
			}
		}

		private void saveMappings_Click(object sender, EventArgs e) {
			File.WriteAllText("mappings.json", JsonConvert.SerializeObject(mappings, Formatting.Indented));
			setDirty(false);
		}

		private void nameBox_TextChanged(object sender, EventArgs e) {
			string originalName = installCheckList.SelectedItems[0].SubItems[0].Text;
			if (originalName != nameBox.Text) {
				GameMapping game = gameLookup[originalName];
				game.name = nameBox.Text;
				gameLookup.Remove(originalName);
				gameLookup[nameBox.Text] = game;
				installCheckList.SelectedItems[0].SubItems[0].Text = nameBox.Text;
				setDirty(true);
			}
		}

		private void archiveBox_TextChanged(object sender, EventArgs e) {
			string originalName = installCheckList.SelectedItems[0].SubItems[0].Text;
			GameMapping game = gameLookup[originalName];
			if (game.filename != archiveBox.Text) {
				game.filename = archiveBox.Text;
				setDirty(true);
			}
		}

		private void installFolderTextBox_TextChanged(object sender, EventArgs e) {
			string originalName = installCheckList.SelectedItems[0].SubItems[0].Text;
			GameMapping game = gameLookup[originalName];
			if (game.installfolder != installFolderTextBox.Text) {
				game.installfolder = installFolderTextBox.Text;
				setDirty(true);
			}
		}

		private void saveFolderTextBox_TextChanged(object sender, EventArgs e) {
			string originalName = installCheckList.SelectedItems[0].SubItems[0].Text;
			GameMapping game = gameLookup[originalName];
			if (game.savefolder != saveFolderTextBox.Text) {
				game.savefolder = saveFolderTextBox.Text;
				setDirty(true);
			}
		}
	}
}
