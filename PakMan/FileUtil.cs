﻿using SevenZip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PakMan
{
	class FileUtil
	{
		public string cacheFolder;
		public static string getCacheFolder() {return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "pakman", "archive_cache");}
		public static string getCacheFolder(string p) {
			return Path.Combine(getCacheFolder(), p);
		}

		static MainForm context;

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

		public FileUtil() {
			cacheFolder = getCacheFolder();
			System.IO.Directory.CreateDirectory(cacheFolder);
		}

		public FileUtil(MainForm context) : this() {
			FileUtil.context = context;
		}


		public void log(string text, string suffix="\r\n") {
			if(context != null) context.log(text, suffix);
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
			FileInfo objFile = new FileInfo(Path.Combine(cacheFolder, filename));
			upload(objFile.Name, objFile.OpenRead());
		}

		public void upload(string filename, Stream objFileStream) {
			string ftpServerIP = Credentials.ftpServerIP;
			string ftpUserName = Credentials.ftpUserName;
			string ftpPassword = Credentials.ftpPassword;

			FtpWebRequest objFTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + filename));
			objFTPRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
			objFTPRequest.KeepAlive = false; // Should the control connection be closed after a command is executed.
			objFTPRequest.UseBinary = true;
			objFTPRequest.ContentLength = objFileStream.Length;
			objFTPRequest.Method = WebRequestMethods.Ftp.UploadFile;

			int intBufferLength = 16 * 1024;
			byte[] objBuffer = new byte[intBufferLength];

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



		public static Int32 getSteamIDFromVanity(string name) {
			using (WebClient Client = new WebClient()) {
				string resp = Client.DownloadString("http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key=16D54C8DB27D36D10F81FE0B098C7C45&vanityurl=" + name);
				ResolveVanityURLResponse arr = JsonConvert.DeserializeObject<ResolveVanityURLResponse>(resp);
				if (arr.response.success == 1) {
					return (Int32)(Int64.Parse(arr.response.steamid) & 0x00000000FFFFFFFF);
				}
			}
			return 0;
		}

		private class ResolveVanityURLResponse {
			public class ResolveVanityURLResponseInner {
				public string steamid;
				public int success;
			}
			public ResolveVanityURLResponseInner response;
		}

		public static Bitmap ConvertTextToImage(string txt, int width, int Height, string fontname="Bookman Old Style", int fontsize=10) {
			Bitmap bmp = new Bitmap(width, Height);
			using (Graphics graphics = Graphics.FromImage(bmp)) {
				Font font = new Font(fontname, fontsize);
				graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
				graphics.DrawString(txt, font, new SolidBrush(Color.Black), 0, 0);
				graphics.Flush();
				font.Dispose();
				graphics.Dispose();
			}
			return bmp;
		}

		private static Bitmap _selectImageText;
		public static Bitmap selectImageText {
			get{
				if(_selectImageText == null) {_selectImageText = FileUtil.ConvertTextToImage("Select Image", 200, 100);}
				return _selectImageText;
			}
		}
	}

	public class Mapping {
		public IList<GameMapping> games { get; set; }
	}

	public class GameMapping {
		public GameMapping() {
			this.name = "?";
			this.filename = "";
			this.savefolder = "";
			this.installfolder = "";
			this.archive_size = 0;
			this.extracted_size = 0;
			this.targetexe = "";
			this.dependencies = "";
			this.description = "";
		}
		public GameMapping(string name) : this() {
			this.name = name;
		}

		public string name { get; set; }
		public string dependencies { get; set; }
		public string description { get; set; }
		public string detection_filename { get; set; }

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
			if(!File.Exists(filePath)) {
				try {
					new FileUtil().downloadArchive(name + ".png");
				}
				catch (WebException ex) {
					if (((ex.Response) as HttpWebResponse).StatusCode != HttpStatusCode.NotFound) {
						throw ex;
					}
				}
			}
			if(File.Exists(filePath)) {
				return Image.FromStream(new MemoryStream(File.ReadAllBytes(filePath)));
			}
			return null;
		}
	}

	public class State {
		public IDictionary<string, StateMapping> games { get; set; }
	}

	public class StateMapping {
		public bool ticked { get; set; }
	}




	public class UserSettings {
		public string steamFolder;
		public Int32 steamID = 0;

		public static string UserSettingsFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "pakman", "user.json");

		public static UserSettings open() {
			try {
				return JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(UserSettings.UserSettingsFile));
			}
			catch (IOException) { }
			return new UserSettings();
		}

		public void save() {
			File.WriteAllText(UserSettings.UserSettingsFile, JsonConvert.SerializeObject(this, Formatting.Indented));
		}

		public string steamUserdataPath {
			get { return Path.Combine(steamFolder, "userdata", steamID.ToString()); }
		}

		public string steamShortcutsPath {
			get { return Path.Combine(steamUserdataPath, "config", "shortcuts.vdf"); }
		}
		public string steamGridPath {
			get { return Path.Combine(steamUserdataPath, "config", "grid"); }
		}
	}
}
