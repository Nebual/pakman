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

		private bool _dirty;
		public bool dirty {
			get { return _dirty; }
			set {
				if (value != _dirty) {
					_dirty = value;
					context.saveMappingsPublic.Text = (_dirty ? "* " : "") + context.saveMappingsPublic.Text.TrimStart(new char[] { '*', ' ' });
				}
			}
		}

		public FileUtil(MainForm context) {
			this.context = context;
			cacheFolder = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "pakman", "archive_cache");
			System.IO.Directory.CreateDirectory(cacheFolder);
		}


		public void log(string text, string suffix="\r\n") {
			context.log(text, suffix);
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
				log("Downloading " + archiveName, "...");
				using (WebClient Client = new WebClient()) {
					Client.DownloadFile("http://nebtown.info/pakman/" + archiveName, Path.Combine(cacheFolder, archiveName));
				}
				log(" done.");
			}
		}
		public void deleteArchive(string archiveName) {
			if (archiveName.Length == 0) {
				//log("Error Cannot Delete: Invalid archiveName");
				return;
			}
			if (archiveExists(archiveName)) {
				log("Deleting archive " + archiveName, "...");
				File.Delete(Path.Combine(cacheFolder, archiveName));
				log(" done.");
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
			log("Extracting " + archiveName + " to " + destinationPath, "...");
			using (SevenZipExtractor extractor = new SevenZipExtractor(archivePath)) {
				extractor.ExtractArchive(destinationPath);
				log(" done.");
				return extractor.UnpackedSize;
			}
		}
		public void deleteGame(string destinationPath) {
			if (destinationPath.Length == 0) return;
			if (Directory.Exists(destinationPath)) {
				log("Uninstalling " + destinationPath, "");
				Directory.Delete(destinationPath, true);
				log(" done.");
			}
		}

		public static long archiveSize(string archiveName) {
			if (archiveName.Length == 0) return 0;
			string archivePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "pakman", "archive_cache", archiveName);
			if (!File.Exists(archivePath)) return 0;
			return new System.IO.FileInfo(archivePath).Length;
		}

		static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
		public static string SizeSuffix(Int64 value) {
			if (value < 0) { return "-" + SizeSuffix(-value); }
			if (value == 0) { return "0.0 bytes"; }

			int mag = (int)Math.Log(value, 1024);
			decimal adjustedSize = (decimal)value / (1L << (mag * 10));

			return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
		}

		public void createArchive(string archiveName, string folderPath) {
			log("Creating archive " + archiveName, " ...");
			SevenZipCompressor compressor = new SevenZipCompressor();
			compressor.CompressionLevel = CompressionLevel.Ultra;
			compressor.CompressionMethod = CompressionMethod.Lzma;
			compressor.CompressionMode = CompressionMode.Create;
			compressor.IncludeEmptyDirectories = true;
			compressor.CompressDirectory(folderPath, Path.Combine(cacheFolder, archiveName), true);
			log(" done.");
		}

		public void uploadArchive(string filename) {
			log("Uploading archive " + filename, " ...");
			string ftpServerIP = "ftp.nebtown.info";
			string ftpUserName = "pakman@nebtown.info";
			string ftpPassword = "[HV[O9@5}dz3";

			FileInfo objFile = new FileInfo(Path.Combine(cacheFolder, filename));
			FtpWebRequest objFTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + objFile.Name));
			objFTPRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
			objFTPRequest.KeepAlive = false; // Should the control connection be closed after a command is executed.
			objFTPRequest.UseBinary = true;
			objFTPRequest.ContentLength = objFile.Length;
			objFTPRequest.Method = WebRequestMethods.Ftp.UploadFile;

			int intBufferLength = 16 * 1024;
			byte[] objBuffer = new byte[intBufferLength];

			FileStream objFileStream = objFile.OpenRead();
			try {
				Stream objStream = objFTPRequest.GetRequestStream();

				int len = 0;
				while ((len = objFileStream.Read(objBuffer, 0, intBufferLength)) != 0) {
					objStream.Write(objBuffer, 0, len);
				}

				objStream.Close();
				objFileStream.Close();
				log(" done");
			}
			catch (Exception ex) {
				throw ex;
			}
		}
	}

	public class Mapping
	{
		public IList<GameMapping> games { get; set; }
	}

	public class GameMapping
	{
		public GameMapping() {
			this.name = "?";
			this.filename = "";
			this.savefolder = "";
			this.installfolder = "";
			this.archive_size = 0;
			this.extracted_size = 0;
			this.targetexe = "";
		}
		public GameMapping(string name) : this() {
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
		private long _archive_size;
		public long archive_size {
			get {
				if (_archive_size != 0) return _archive_size;
				return _archive_size = FileUtil.archiveSize(filename);
			}
			set { _archive_size = value; }
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
