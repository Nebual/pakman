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
				bool downloaded = (game.filename.Length > 0 && F.archiveExists(game.filename));
				bool installed = Directory.Exists(game.name);
				string archiveSize = downloaded ? FileUtil.SizeSuffix(game.archive_size) : "";
				string gameSize = downloaded ? FileUtil.SizeSuffix(game.extracted_size) : "";
				itemList.Rows.Add(new object[] { downloaded, installed, game.name, archiveSize, gameSize });
			}

			/*State state = JsonConvert.DeserializeObject<State>(File.ReadAllText("state.json"));
			foreach (ListViewItem item in installCheckList.Items) {
				item.Checked = state.games[item.Text].ticked;
			}*/
		}

		private void apply_Click(object sender, EventArgs e) {
			foreach (DataGridViewRow row in itemList.Rows) {
				if (row.Cells[2].Value == null) continue;

				GameMapping game = gameLookup[(string)row.Cells[2].Value];
				log("Processing " + game.name);

				if ((bool)row.Cells[0].Value) {
					F.downloadArchive(game.filename);
				}

				if ((bool)row.Cells[1].Value) {
					if (!Directory.Exists(game.installfolder)) F.downloadArchive(game.filename);

					long size;
					if ((size = F.extractArchive(game.filename, game.installfolder)) > 0) {
						game.extracted_size = size;
						row.Cells[4].Value = FileUtil.SizeSuffix(game.extracted_size);
						setDirty(true);
					}
				}
				else {
					F.deleteGame(game.installfolder);
				}

				if (!(bool)row.Cells[0].Value) {
					F.deleteArchive(game.filename);
				}
			}
		}

		public void log(string text, string suffix = "\r\n") {
			logBox.AppendText(text + suffix);
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

		GameMapping activeGame;
		public GameMapping selectedGame() {
			if (itemList.SelectedRows[0].Cells[2].Value != null) {
				activeGame = gameLookup[(string)itemList.SelectedRows[0].Cells[2].Value];
			}
			return activeGame;
		}

		private void itemList_SelectionChanged(object sender, EventArgs e) {
			updateDescription(selectedGame());
		}

		private void updateDescription(GameMapping game) {
			nameBox.Text = game.name;
			archiveBox.Text = game.filename;
			saveFolderTextBox.Text = game.savefolder;
			installFolderTextBox.Text = game.installfolder;
		}

		private void addNewItemButton_Click(object sender, EventArgs e) {
			gameLookup.ToList().ForEach(x => Console.WriteLine(x.Key.ToString() + ": " + x.Value.ToString()));
			if (!gameLookup.ContainsKey("New")) {
				GameMapping game = new GameMapping("New");
				mappings.games.Add(game);
				gameLookup[game.name] = game;
				itemList.Rows.Add(new object[] { false, false, game.name, "", "" });
				setDirty(true);
			}
		}

		private void saveMappings_Click(object sender, EventArgs e) {
			File.WriteAllText("mappings.json", JsonConvert.SerializeObject(mappings, Formatting.Indented));
			setDirty(false);
		}

		private void nameBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.name != nameBox.Text) {
				gameLookup.Remove(game.name);
				gameLookup[nameBox.Text] = game;
				game.name = nameBox.Text;
				itemList.SelectedRows[0].Cells[2].Value = nameBox.Text;
				setDirty(true);
			}
		}

		private void archiveBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.filename != archiveBox.Text) {
				game.filename = archiveBox.Text;
				setDirty(true);
			}
		}

		private void installFolderTextBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.installfolder != installFolderTextBox.Text) {
				game.installfolder = installFolderTextBox.Text;
				setDirty(true);
			}
		}

		private void saveFolderTextBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.savefolder != saveFolderTextBox.Text) {
				game.savefolder = saveFolderTextBox.Text;
				setDirty(true);
			}
		}

		private void targetExeTextBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.targetexe != targetExeTextBox.Text) {
				game.targetexe = targetExeTextBox.Text;
				setDirty(true);
			}
		}

		private void buildArchiveButton_Click(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.name == "New" || game.filename.Length == 0) {
				log("Error: Fill out game name and archive name.");
				return;
			}
			if(folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
				F.createArchive(game.filename, folderBrowserDialog1.SelectedPath);
				if (F.archiveExists(game.filename)) {
					game.archive_size = 0;  long _ = game.archive_size; // recalc archive size
					itemList.SelectedRows[0].Cells[0].Value = true;
					F.uploadArchive(game.filename);
				}
			}
		}
	}
}
