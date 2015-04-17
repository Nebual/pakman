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

		public bool archiveExists(string archiveName) {
			return File.Exists(Path.Combine(cacheFolder, archiveName));
		}
		public void downloadArchive(string archiveName, bool overwrite = false) {
			if (archiveName.Length == 0) {
				log("Error Cannot Download: Invalid archiveName");
				return;
			}
			if (overwrite || !archiveExists(archiveName)) {
				log("Downloading " + archiveName);
				using (WebClient Client = new WebClient()) {
					Client.DownloadFile("http://nebtown.info/pakman/" + archiveName, Path.Combine(cacheFolder, archiveName));
				}
			}
		}
		public void deleteArchive(string archiveName) {
			if (archiveName.Length == 0) {
				//log("Error Cannot Delete: Invalid archiveName");
				return;
			}
			if (archiveExists(archiveName)) {
				log("Deleting archive " + archiveName);
				File.Delete(Path.Combine(cacheFolder, archiveName));
			}
		}

		public long extractArchive(string archiveName, string destinationPath) {
			if (archiveName.Length == 0) {
				log("Error Cannot Extract: Invalid archiveName");
				return 0;
			}
			if (destinationPath.Length == 0) {
				log("Error Cannot Extract: Invalid destinationPath");
				return 0;
			}
			if (Directory.Exists(destinationPath)) {
				return 0;
			}
			if (!archiveExists(archiveName)) {
				log("Error Cannot Extract: Archive '"+archiveName+"'doesn't exist!");
				return 0;
			}
			Directory.CreateDirectory(destinationPath);
			string archivePath = Path.Combine(cacheFolder, archiveName);
			log("Extracting " + archiveName + " to " + destinationPath);
			using (SevenZipExtractor extractor = new SevenZipExtractor(archivePath)) {
				extractor.ExtractArchive(destinationPath);
				return extractor.UnpackedSize;
			}
		}
		public void deleteGame(string destinationPath) {
			if (destinationPath.Length == 0) return;
			if (Directory.Exists(destinationPath)) {
				Directory.Delete(destinationPath, true);
			}
		}

		public string archiveSize(string archiveName) {
			if (archiveName.Length == 0) return "";
			if (!archiveExists(archiveName)) return "";
			return SizeSuffix(new System.IO.FileInfo(Path.Combine(cacheFolder, archiveName)).Length);
		}

		static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
		public static string SizeSuffix(Int64 value) {
			if (value < 0) { return "-" + SizeSuffix(-value); }
			if (value == 0) { return "0.0 bytes"; }

			int mag = (int)Math.Log(value, 1024);
			decimal adjustedSize = (decimal)value / (1L << (mag * 10));

			return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
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
			get {
				if (_installfolder.Length > 0) return _installfolder;
				return name + Path.DirectorySeparatorChar;
			}
			set { _installfolder = value.Replace("/", "\\").TrimEnd('\\'); if (_installfolder.Length > 0) { _installfolder += "\\"; } }
		}
		public long extracted_size { get; set; }
		public string targetexe { get; set; }
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
