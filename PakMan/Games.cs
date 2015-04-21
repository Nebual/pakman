using Newtonsoft.Json;
using SevenZip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PakMan {

	public class Games {
		public IList<Game> games { get; set; }

		[JsonIgnore]
		public static string GamesMappingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "pakman", "games.json");

		public static Games open() {
			if (!File.Exists(GamesMappingFile)) FileUtil.download("games.json", GamesMappingFile);
			return JsonConvert.DeserializeObject<Games>(File.ReadAllText(GamesMappingFile));
		}
		public void save() {
			File.WriteAllText(GamesMappingFile, JsonConvert.SerializeObject(this, Formatting.Indented));
		}
	}

	public class Game {
		[JsonIgnore]
		public static MainForm context;


		public Game() {
			this.name = "?";
			this.archive_filename = "";
			this.detection_filename = "";
			this.savefolder = "";
			this.installfolder = "";
			this.archive_size = 0;
			this.extracted_size = 0;
			this.targetexe = "";
			this.dependencies = "";
			this.description = "";
		}
		public Game(string name)
			: this() {
			this.name = name;
		}

		public string name { get; set; }
		public string dependencies { get; set; }
		public string description { get; set; }
		private string _detection_filename;
		public string detection_filename {
			get { return _detection_filename; }
			set { _detection_filename = value.Replace("/", "\\"); }
		}

		private string _archive_filename;
		public string archive_filename {
			get { return _archive_filename; }
			set { _archive_filename = value.Replace("/", "\\"); }
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
		private long _archive_size;
		public long archive_size {
			get {
				if (_archive_size != 0) return _archive_size;
				return _archive_size = FileUtil.archiveSize(archive_filename);
			}
			set { _archive_size = value; }
		}
		public long extracted_size { get; set; }
		public string targetexe { get; set; }

		[JsonIgnore]
		private Image _image;
		[JsonIgnore]
		public Image image {
			get {
				if (_image != null) return _image;
				_image = gen_image();
				return _image;
			}
			set {
				_image = value;
			}
		}

		private System.Drawing.Image gen_image() {
			string filePath = Path.Combine(FileUtil.getCacheFolder(), name + ".png");
			if (!File.Exists(filePath)) {
				try {
					FileUtil.downloadArchive(name + ".png");
				}
				catch (WebException ex) {
					if (((ex.Response) as HttpWebResponse).StatusCode != HttpStatusCode.NotFound) {
						throw ex;
					}
				}
			}
			if (File.Exists(filePath)) {
				return Image.FromStream(new MemoryStream(File.ReadAllBytes(filePath)));
			}
			return null;
		}

		public bool exists() {
			if (detection_filename.Length == 0) return false;
			return File.Exists(Path.Combine(context.settings.getGamesFolder(installfolder), detection_filename));
		}



		public bool install() {
			if (archive_filename.Length == 0) {
				context.log("Error Cannot Extract: Invalid archive_filename");
				return false;
			}
			if (installfolder.Length == 0) {
				context.log("Error Cannot Extract: Invalid installfolder");
				return false;
			}
			if (!FileUtil.archiveExists(archive_filename)) {
				context.log("Error Cannot Extract: Archive '" + archive_filename + "'doesn't exist!");
				return false;
			}
			Directory.CreateDirectory(context.settings.getGamesFolder(installfolder));
			string archivePath = FileUtil.getCacheFolder(archive_filename);
			context.log("Extracting " + archive_filename + " to " + context.settings.getGamesFolder(installfolder), "...");
			using (SevenZipExtractor extractor = new SevenZipExtractor(archivePath)) {
				extractor.ExtractArchive(context.settings.getGamesFolder(installfolder));
				context.settings.installedFilenames[name] = new List<string>(extractor.ArchiveFileNames);
				context.log(" done.");
				extracted_size = extractor.UnpackedSize;
			}
			return true;
		}

		internal void uninstall() {
			if (installfolder.Length == 0) return;
			if (exists()) {
				context.log("Uninstalling " + name, "");

				List<string> filenames = null;
				if (context.settings.installedFilenames.ContainsKey(name)) {
					filenames = context.settings.installedFilenames[name];
				}
				if ((filenames == null) || filenames.Count() == 0) {
					if (FileUtil.archiveExists(archive_filename)) {
						filenames = FileUtil.archiveGetFilenames(archive_filename);
					}
					else {
						context.log("Error: Cannot uninstall; archive not present and install log corrupt.");
						return;
					}
				}

				foreach (string filename in filenames) {
					string path = Path.Combine(context.settings.getGamesFolder(installfolder), filename);
					if (Directory.Exists(path)) {
						if (Directory.GetFiles(path).Length == 0) {
							Directory.Delete(path, false);
						}
					}
					else if (File.Exists(path)) {
						File.Delete(path);
					}
				}
				if (Directory.GetFiles(context.settings.getGamesFolder(installfolder)).Length == 0) {
					Directory.Delete(context.settings.getGamesFolder(installfolder), false);
				}
				context.log(" done.");
			}
		}
	}
}
