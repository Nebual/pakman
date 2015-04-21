using Microsoft.Win32;
using Newtonsoft.Json;
using SevenZip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 * 
 * == Todo ==
 * Dependencies (do the multiple, hierarchial approach with recursion when ticking boxes)
 * Game Saves (have a wildcard?)
 */

namespace PakMan {

	public partial class MainForm : Form {

		public MainForm() {
			addDLLReflection();
			InitializeComponent();
			string v = checkForApplicationUpdates(true);
			this.Text = this.ProductName + " v" + v;
		}

		Games mappings;
		public UserSettings settings;
		public Button saveMappingsPublic;
		IDictionary<string, Game> gameLookup = new Dictionary<string, Game>();

		private void MainForm_Load(object sender, EventArgs e) {
			saveMappingsPublic = saveMappings;

			FileUtil.context = this;
			Game.context = this;
			settings = UserSettings.open();
			FileUtil.init();


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
			mappings = Games.open();
			foreach (Game game in mappings.games) {
				gameLookup[game.name] = game;
				bool downloaded = (game.archive_filename.Length > 0 && FileUtil.archiveExists(game.archive_filename));
				bool installed = Directory.Exists(game.installfolder);
				string archiveSize = downloaded ? FileUtil.SizeSuffix(game.archive_size) : "";
				string gameSize = downloaded ? FileUtil.SizeSuffix(game.extracted_size) : "";
				itemList.Rows.Add(new object[] { downloaded, installed, game.name, game.description, archiveSize, gameSize });
			}
			dirty = false;
		}
		private void revertButton_Click(object sender, EventArgs e) {
			loadMapping();
		}

		private void apply_Click(object sender, EventArgs e) {
			log("");

			SteamShortcuts steamShortcuts = new SteamShortcuts(settings.steamShortcutsPath, settings.steamGridPath, this.log);

			foreach (DataGridViewRow row in itemList.Rows) {
				if (row.Cells[2].Value == null) continue;

				Game game = gameLookup[(string)row.Cells[2].Value];
				logContext = game.name;

				if ((bool)row.Cells[0].Value) {
					FileUtil.downloadArchive(game.archive_filename);
				}

				if ((bool)row.Cells[1].Value) {
					FileUtil.downloadArchive(game.archive_filename);

					if (!game.exists()) {
						if (game.install()) {
							row.Cells[5].Value = FileUtil.SizeSuffix(game.extracted_size);
							steamShortcuts.addGame(game);
						}
					}
				}
				else {
					game.uninstall();
					steamShortcuts.removeGame(game);
				}

				if (!(bool)row.Cells[0].Value) {
					FileUtil.deleteArchive(game.archive_filename);
				}
			}
			logContext = "";

			if (steamShortcuts.flush()) log("Updated Steam's shortcuts.vdf file. Please restart Steam.");
			settings.save();
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

		Game activeGame;
		public Game selectedGame() {
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

		private void updateDescription(Game game) {
			nameBox.Text = game.name;
			archiveBox.Text = game.archive_filename;
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
				Game game = new Game("New");
				mappings.games.Add(game);
				gameLookup[game.name] = game;
				itemList.Rows.Add(new object[] { false, false, game.name, "", "" });
				dirty = true;
			}
		}

		private void saveMappings_Click(object sender, EventArgs e) {
			File.WriteAllText("mappings.json", JsonConvert.SerializeObject(mappings, Formatting.Indented));
			dirty = false;
		}
		private bool _dirty;
		public bool dirty {
			get { return _dirty; }
			set {
				if (value != _dirty) {
					_dirty = value;
					saveMappings.Text = (_dirty ? "* " : "") + saveMappings.Text.TrimStart(new char[] { '*', ' ' });
				}
			}
		}

		private void nameBox_TextChanged(object sender, EventArgs e) {
			Game game = selectedGame();
			if (game.name != nameBox.Text) {
				gameLookup.Remove(game.name);
				gameLookup[nameBox.Text] = game;
				game.name = nameBox.Text;
				itemList.SelectedRows[0].Cells[2].Value = nameBox.Text;
				dirty = true;
			}
		}

		private void archiveBox_TextChanged(object sender, EventArgs e) {
			Game game = selectedGame();
			if (game.archive_filename != archiveBox.Text) {
				game.archive_filename = archiveBox.Text;
				dirty = true;
			}
		}

		private void installFolderTextBox_TextChanged(object sender, EventArgs e) {
			Game game = selectedGame();
			if (game.installfolder != installFolderTextBox.Text) {
				game.installfolder = installFolderTextBox.Text;
				dirty = true;
			}
		}

		private void saveFolderTextBox_TextChanged(object sender, EventArgs e) {
			Game game = selectedGame();
			if (game.savefolder != saveFolderTextBox.Text) {
				game.savefolder = saveFolderTextBox.Text;
				dirty = true;
			}
		}

		private void targetExeTextBox_TextChanged(object sender, EventArgs e) {
			Game game = selectedGame();
			if (game.targetexe != targetExeTextBox.Text) {
				game.targetexe = targetExeTextBox.Text;
				dirty = true;
			}
		}

		private void dependsTextBox_TextChanged(object sender, EventArgs e) {
			Game game = selectedGame();
			if (game.dependencies != dependsTextBox.Text) {
				game.dependencies = dependsTextBox.Text;
				dirty = true;
			}
		}

		private void descriptionTextBox_TextChanged(object sender, EventArgs e) {
			Game game = selectedGame();
			if (game.description != descriptionTextBox.Text) {
				game.description = descriptionTextBox.Text;
				itemList.SelectedRows[0].Cells[3].Value = descriptionTextBox.Text;
				dirty = true;
			}
		}

		private void buildArchiveButton_Click(object sender, EventArgs e) {
			Game game = selectedGame();
			if (game.name == "New") {
				log("Error: Fill out game name!");
				return;
			}
			if (game.archive_filename.Length == 0) {
				game.archive_filename = game.name + ".7z";
			}
			if(folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
				FileUtil.createArchive(game.archive_filename, folderBrowserDialog1.SelectedPath);
				if (FileUtil.archiveExists(game.archive_filename)) {
					game.detection_filename = FileUtil.archiveGetFilenames(game.archive_filename)[0];
					game.archive_size = 0; itemList.SelectedRows[0].Cells[4].Value = FileUtil.SizeSuffix(game.archive_size); // recalc archive size
					itemList.SelectedRows[0].Cells[0].Value = true;
					FileUtil.uploadArchive(game.archive_filename);
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
				Game game = selectedGame();

				Image b = Image.FromStream(openFileDialog1.OpenFile());
				b.Save(FileUtil.getCacheFolder(game.name + ".png"));
				game.image = b;
				imageButton.AutoScaleImage = b;

				if (settings.steamID != 0) { new VDFGame(game).copyImageToGrid(settings.steamGridPath); }
				FileUtil.uploadArchive(game.name + ".png");
			}
		}

		private void purgeMappingsButton_Click(object sender, EventArgs e) {
			if (MessageBox.Show("Overwrite local mappings.json database with a freshly downloaded copy?", "Purge mappings.json", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK) {
				File.Delete("mappings.json");
				loadMapping();
			}
		}

		private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e) {
			checkForApplicationUpdates();
		}
	}
}
