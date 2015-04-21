using Microsoft.Win32;
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
/*
 * 
 * == Todo ==
 * Implement picture uploads
 * Tie in pictures to Steam Bigpicture
 * Dependencies (do the multiple, hierarchial approach with recursion when ticking boxes)
 * Implement better "its installed" detection; only remove files found in the relevent archive (install log in user.json??)
 */

namespace PakMan {

	public partial class MainForm : Form {

		public MainForm() {
			InitializeComponent();
		}

		FileUtil F;
		Mapping mappings;
		public UserSettings settings;
		public Button saveMappingsPublic;
		IDictionary<string, GameMapping> gameLookup = new Dictionary<string, GameMapping>();

		private void MainForm_Load(object sender, EventArgs e) {
			saveMappingsPublic = saveMappings;

			settings = UserSettings.open();
			F = new FileUtil(this);

			RegistryKey regKey;
			if ((regKey = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam")) != null) {
				string installpath = regKey.GetValue("SteamPath").ToString().Replace("/","\\");
				if (settings.steamFolder != installpath) {
					log("Found Steam Folder: " + installpath);
					settings.steamFolder = installpath;
					settings.save();
				}
			}
			if (settings.steamID == 0 && (regKey = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam")) != null) {
				string nickname = regKey.GetValue("LastGameNameUsed").ToString();
				Int32 res;
				if ((res = FileUtil.getSteamIDFromVanity(nickname)) > 0) {
					log("Guessing steamid of: " + res + ", based on nickname: " + nickname);
					settings.steamID = res;
					settings.save();
				}
			}

			loadMapping();
		}

		private void loadMapping() {
			itemList.Rows.Clear();
			gameLookup.Clear();
			mappings = JsonConvert.DeserializeObject<Mapping>(File.ReadAllText("mappings.json"));
			foreach (GameMapping game in mappings.games) {
				gameLookup[game.name] = game;
				bool downloaded = (game.filename.Length > 0 && F.archiveExists(game.filename));
				bool installed = Directory.Exists(game.installfolder);
				string archiveSize = downloaded ? FileUtil.SizeSuffix(game.archive_size) : "";
				string gameSize = downloaded ? FileUtil.SizeSuffix(game.extracted_size) : "";
				itemList.Rows.Add(new object[] { downloaded, installed, game.name, archiveSize, gameSize });
			}
			F.dirty = false;
		}
		private void revertButton_Click(object sender, EventArgs e) {
			loadMapping();
		}

		private void apply_Click(object sender, EventArgs e) {
			log("");

			SteamShortcuts steamShortcuts = new SteamShortcuts(settings.steamShortcutsPath, settings.steamGridPath, this.log);

			foreach (DataGridViewRow row in itemList.Rows) {
				if (row.Cells[2].Value == null) continue;

				GameMapping game = gameLookup[(string)row.Cells[2].Value];
				logContext = game.name;

				if ((bool)row.Cells[0].Value) {
					F.downloadArchive(game.filename);
				}

				if ((bool)row.Cells[1].Value) {
					if (!Directory.Exists(game.installfolder)) F.downloadArchive(game.filename);

					long size;
					if ((size = F.extractArchive(game.filename, game.installfolder)) > 0) {
						game.extracted_size = size;
						row.Cells[4].Value = FileUtil.SizeSuffix(game.extracted_size);
						steamShortcuts.addGame(game);
					}
				}
				else {
					F.deleteGame(game.installfolder);
					steamShortcuts.removeGame(game);
				}

				if (!(bool)row.Cells[0].Value) {
					F.deleteArchive(game.filename);
				}
			}
			logContext = "";

			if (steamShortcuts.flush()) log("Updated Steam's shortcuts.vdf file. Please restart Steam.");
			log("** Processing Complete **");
		}

		private string logContext = "";
		public void log(string text, string suffix = "\r\n") {
			if (logContext.Length > 2) {
				logBox.AppendText("Processing " + logContext + ":\r\n");
				logContext = "x";
			}
			if (logContext == "x" && logBox.Text.LastOrDefault() == '\n') text = "\t" + text;
			logBox.AppendText(text + suffix);
		}

		GameMapping activeGame;
		public GameMapping selectedGame() {
			try {
				activeGame = gameLookup[(string)itemList.SelectedRows[0].Cells[2].Value];
			}
			catch (NullReferenceException) { }
			catch (IndexOutOfRangeException) { }
			catch (ArgumentOutOfRangeException) { }
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
			targetExeTextBox.Text = game.targetexe;
			imageButton.AutoScaleImage = game.image != null ? game.image : FileUtil.selectImageText;
			
			dependsTextBox.Text = game.dependencies;
			descriptionTextBox.Text = game.description;
		}

		private void addNewItemButton_Click(object sender, EventArgs e) {
			gameLookup.ToList().ForEach(x => Console.WriteLine(x.Key.ToString() + ": " + x.Value.ToString()));
			if (!gameLookup.ContainsKey("New")) {
				GameMapping game = new GameMapping("New");
				mappings.games.Add(game);
				gameLookup[game.name] = game;
				itemList.Rows.Add(new object[] { false, false, game.name, "", "" });
				F.dirty = true;
			}
		}

		private void saveMappings_Click(object sender, EventArgs e) {
			File.WriteAllText("mappings.json", JsonConvert.SerializeObject(mappings, Formatting.Indented));
			F.dirty = false;
		}

		private void nameBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.name != nameBox.Text) {
				gameLookup.Remove(game.name);
				gameLookup[nameBox.Text] = game;
				game.name = nameBox.Text;
				itemList.SelectedRows[0].Cells[2].Value = nameBox.Text;
				F.dirty = true;
			}
		}

		private void archiveBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.filename != archiveBox.Text) {
				game.filename = archiveBox.Text;
				F.dirty = true;
			}
		}

		private void installFolderTextBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.installfolder != installFolderTextBox.Text) {
				game.installfolder = installFolderTextBox.Text;
				F.dirty = true;
			}
		}

		private void saveFolderTextBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.savefolder != saveFolderTextBox.Text) {
				game.savefolder = saveFolderTextBox.Text;
				F.dirty = true;
			}
		}

		private void targetExeTextBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.targetexe != targetExeTextBox.Text) {
				game.targetexe = targetExeTextBox.Text;
				F.dirty = true;
			}
		}

		private void dependsTextBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.dependencies != dependsTextBox.Text) {
				game.dependencies = dependsTextBox.Text;
				F.dirty = true;
			}
		}

		private void descriptionTextBox_TextChanged(object sender, EventArgs e) {
			GameMapping game = selectedGame();
			if (game.description != descriptionTextBox.Text) {
				game.description = descriptionTextBox.Text;
				F.dirty = true;
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
					game.archive_size = 0; itemList.SelectedRows[0].Cells[3].Value = FileUtil.SizeSuffix(game.archive_size); // recalc archive size
					itemList.SelectedRows[0].Cells[0].Value = true;
					F.uploadArchive(game.filename);
				}
			}
		}

		private void setSteamUsernameToolStripMenuItem_Click(object sender, EventArgs e) {
			SteamIDHolder holder = new SteamIDHolder();
			new TextPrompt("Enter your Steam Vanity URL (name)", holder).ShowDialog();
			if (holder.steamID > 0) {
				log("Found SteamID " + holder.steamID);
				settings.steamID = holder.steamID;
				settings.save();
			}
		}

		private void imageButton_Click(object sender, EventArgs e) {
			if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				GameMapping game = selectedGame();

				Image b = Image.FromStream(openFileDialog1.OpenFile());
				b.Save(Path.Combine(F.cacheFolder, game.name + ".png"));
				game.image = b;
				imageButton.AutoScaleImage = b;

				if (settings.steamID != 0) { new VDFGame(game).copyImageToGrid(settings.steamGridPath); }
				F.uploadArchive(game.name + ".png");
			}
		}
	}
}
