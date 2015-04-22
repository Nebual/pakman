using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakMan {
	class SteamShortcuts {

		private bool dirty = false;
		private string shortcutsFilePath;
		private string gridFilePath;

		IDictionary<string, VDFGame> games;

		public SteamShortcuts(string shortcutsFilePath, string gridFilePath, Action<string, string> log) {
			this.shortcutsFilePath = shortcutsFilePath;
			this.gridFilePath = gridFilePath;

			if (!File.Exists(shortcutsFilePath)) {
				log("Error: Cannot find Steam userdata folder; have you setup your username in Options?", "\r\n");
				return;
			}

			games = readShortcutVDF();
		}

		private IDictionary<string, VDFGame> readShortcutVDF() {
			string file = File.ReadAllText(shortcutsFilePath);
			file = Regex.Replace(file, @"\x01", "<tag>");
			file = Regex.Replace(file, @"\x00", "\n");
			file = Regex.Replace(file, @"\b", "");

			string[] games = Regex.Split(file, "\n");
			VDFGame game = new VDFGame();
			IDictionary<string, VDFGame> gamelist = new Dictionary<string, VDFGame>();
			string go = "";

			for (int line = 0, length = games.Length; line < length; ++line) {
				if (go == "") {
					if (string.Equals(games[line], "<tag>AppName", StringComparison.CurrentCultureIgnoreCase))
						go = "AppName";
					if (string.Equals(games[line], "<tag>Exe", StringComparison.CurrentCultureIgnoreCase))
						go = "Exe";
					if (string.Equals(games[line], "<tag>StartDir", StringComparison.CurrentCultureIgnoreCase))
						go = "StartDir";
					continue;
				}

				if (go == "AppName") {
					game.name = games[line];
					go = "";
				}
				else if (go == "Exe") {
					game.exe = games[line];//.Replace("\"", "");
					go = "";
				}
				else if (go == "StartDir") {
					game.startdir = games[line];//.Replace("\"", "");
					gamelist.Add(game.name, game);
					game = new VDFGame();
					go = "";
				}
			}

			return gamelist;
		}

		private void writeShortcutVDF() {
			int i = 0;
			string outbuf = "\0shortcuts\0";
			foreach (VDFGame game in games.Values) {
				outbuf += "\0" + (i++) + "\0"
					+ "\x01" + "appname\0" + game.name + "\0"
					+ "\x01" + "exe\0" + game.exe + "\0"
					+ "\x01" + "StartDir\0" + game.startdir + "\0"
					+ "\x01" + "icon\0\0" + "\x01" + "ShortcutPath\0\0\x02" + "hidden\0\0\0\0\0\0tags\0"
					+ "\x08\x08";
			}
			outbuf += "\x08\x08";
			File.WriteAllText(shortcutsFilePath, outbuf);
		}

		public void addGame(Game game) {
			if (games == null) return;
			if (!games.ContainsKey(game.name) && game.targetexe.Length > 0) {
				games.Add(game.name, new VDFGame(game));

				if (game.image != null) {
					games[game.name].copyImageToGrid(gridFilePath);
				}
				dirty = true;
			}
		}

		public void removeGame(Game game) {
			if (games == null) return;
			if (games.ContainsKey(game.name)) {
				games.Remove(game.name);
				dirty = true;
			}
		}

		public bool flush() {
			if (games == null) return false;
			if (dirty) {
				writeShortcutVDF();
				dirty = false;
				//if (MessageBox.Show("Steam Shortcuts updated; restart steam?", "Restart Steam?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
				if (MainForm.context.settings.autoRestartSteam) {
					restartSteam();
				}
				return true;
			}
			return false;
		}

		public static void restartSteam() {
			var processes = Process.GetProcessesByName("steam");
			if (processes.Length > 0) {
				var process = processes[0];
				var path = process.MainModule.FileName;
				process.Kill();
				Process.Start(path);
			}
		}
	}

	public class VDFGame {
		public string name;
		public string exe;
		public string startdir;
		private string _gameid;
		public string gameid {
			get {
				if (_gameid != null && _gameid.Length > 0) return _gameid;
				// Calculate the bullshit goddamn stupid gameid Steam uses for custom BigPicture banner image filenames
				UInt64 hash = (UInt64)CRC32.Compute(exe + name);
				_gameid = ((hash | 0x80000000) << 32 | 0x02000000).ToString();
				return _gameid;
			}
		}

		public VDFGame() { }
		public VDFGame(Game game) {
			name = game.name;
			startdir = MainForm.context.settings.getGamesFolder(game.installfolder);
			exe = Path.Combine(MainForm.context.settings.getGamesFolder(game.installfolder), game.targetexe);
		}
		public override string ToString() {
			return name + " - " + exe + " - " + startdir;
		}

		public void copyImageToGrid(string steamGridPath) {
			File.Copy(FileUtil.getCacheFolder(name + ".png"), Path.Combine(steamGridPath, gameid + ".png"), true);
		}
	}
}
