using SevenZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakMan
{
	class FileUtil
	{
		public string cacheFolder;

		MainForm context;

		public FileUtil(MainForm context) {
			this.context = context;
			cacheFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "pakman", "archive_cache");
			System.IO.Directory.CreateDirectory(cacheFolder);
		}


		public void log(string text) {
			context.log(text);
		}

		public static long GetDirectorySize(string parentDirectory) {
			return new DirectoryInfo(parentDirectory).GetFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length);
		}

		public static void writeCacheFile(string filename, string data) {
			File.WriteAllText(Path.Combine(Application.UserAppDataPath, "archive_cache", filename), data);
		}
		public void downloadArchive(string archiveName, bool overwrite = false) {
			if (archiveName.Length == 0) {
				log("Error Cannot Download: Invalid archiveName");
				return;
			}
			string archivePath = Path.Combine(cacheFolder, archiveName);
			if (overwrite || !File.Exists(archivePath)) {
				log("Downloading " + archiveName);
				using (WebClient Client = new WebClient()) {
					Client.DownloadFile("http://nebtown.info/pakman/" + archiveName, archivePath);
				}
			}
		}
		public void extractArchive(string archiveName, string destinationPath) {
			if (archiveName.Length == 0) {
				log("Error Cannot Extract: Invalid archiveName");
				return;
			}
			if (destinationPath.Length == 0) {
				log("Error Cannot Extract: Invalid destinationPath");
				return;
			}
			string archivePath = Path.Combine(cacheFolder, archiveName);
			log("Extracting " + archiveName + " to " + destinationPath);
			using (SevenZipExtractor extractor = new SevenZipExtractor(archivePath)) {
				extractor.ExtractArchive(destinationPath);
			}
		}
	}

	public class Mapping
	{
		public IList<GameMapping> games { get; set; }
	}

	public class GameMapping
	{
		public GameMapping(string name) {
			this.name = name;
			filename = "";
			savefolder = "";
			installfolder = "";
		}

		public string name { get; set; }
		private string _filename;
		public string filename {
			get { return _filename; }
			set { _filename = value.Replace("/", "\\"); }
		}
		private string _savefolder;
		public string savefolder {
			get { return _savefolder; }
			set { _savefolder = value.Replace("/", "\\").TrimEnd('\\'); if (_savefolder.Length > 0) { _savefolder += "\\"; } }
		}
		private string _installfolder;
		public string installfolder {
			get { return _installfolder; }
			set { _installfolder = value.Replace("/", "\\").TrimEnd('\\'); if (_installfolder.Length > 0) { _installfolder += "\\"; } }
		}
	}

	public class State
	{
		public IDictionary<string, StateMapping> games { get; set; }
	}

	public class StateMapping
	{
		public bool ticked { get; set; }
	}
}
