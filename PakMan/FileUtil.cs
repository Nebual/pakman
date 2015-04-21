using SevenZip;
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
using System.Collections.ObjectModel;

namespace PakMan
{
	static class FileUtil
	{
		public static string getCacheFolder() {return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "pakman", "archive_cache");}
		public static string getCacheFolder(string p) {
			return Path.Combine(getCacheFolder(), p);
		}

		public static MainForm context;

		public static void init() {
			Directory.CreateDirectory(getCacheFolder());
		}

		public static long GetDirectorySize(string parentDirectory) {
			return new DirectoryInfo(parentDirectory).GetFiles("*.*", SearchOption.AllDirectories).Sum(file => file.Length);
		}

		public static void writeCacheFile(string filename, string data) {
			File.WriteAllText(Path.Combine(Application.UserAppDataPath, "archive_cache", filename), data);
		}

		public static bool archiveExists(string archiveName) {
			return File.Exists(getCacheFolder(archiveName));
		}
		public static void download(string filename, string savepath) {
			using (WebClient Client = new WebClient()) {
				Client.DownloadFile("http://nebtown.info/pakman/" + filename, savepath);
			}
		}
		public static void downloadArchive(string archiveName, bool overwrite = false) {
			if (archiveName.Length == 0) {
				context.log("Error Cannot Download: Invalid archiveName");
				return;
			}
			if (overwrite || !archiveExists(archiveName)) {
				context.log("Downloading " + archiveName, "...");
				download(archiveName, getCacheFolder(archiveName));
				context.log(" done.");
			}
		}
		public static void deleteArchive(string archiveName) {
			context.log("Deleting archive " + archiveName, "...");
			delete(archiveName);
			context.log(" done.");
		}
		public static void delete(string fileName) {
			if (fileName.Length != 0 && archiveExists(fileName)) {
				File.Delete(getCacheFolder(fileName));
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

		public static void createArchive(string archiveName, string folderPath) {
			context.log("Creating archive " + archiveName, " ...");
			SevenZipCompressor compressor = new SevenZipCompressor();
			compressor.CompressionLevel = CompressionLevel.Ultra;
			compressor.CompressionMethod = CompressionMethod.Lzma;
			compressor.CompressionMode = CompressionMode.Create;
			compressor.IncludeEmptyDirectories = true;
			compressor.CompressDirectory(folderPath, getCacheFolder(archiveName), true);
			context.log(" done.");
		}

		public static List<string> archiveGetFilenames(string archiveName) {
			using (SevenZipExtractor extractor = new SevenZipExtractor(getCacheFolder(archiveName))) {
				return new List<string>(extractor.ArchiveFileNames);
			}
		}

		public static void uploadArchive(string filename) {
			context.log("Uploading archive " + filename, " ...");
			FileInfo objFile = new FileInfo(getCacheFolder(filename));
			upload(objFile.Name, objFile.OpenRead());
			context.log(" done");
		}

		public static void upload(string filename, Stream objFileStream) {
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

			Stream objStream = objFTPRequest.GetRequestStream();

			int len = 0;
			while ((len = objFileStream.Read(objBuffer, 0, intBufferLength)) != 0) {
				objStream.Write(objBuffer, 0, len);
			}

			objStream.Close();
			objFileStream.Close();
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


	public class UserSettings {
		public string steamFolder;
		public Int32 steamID = 0;

		public IDictionary<string, List<string>> installedFilenames = new Dictionary<string, List<string>>();

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

		[JsonIgnore]
		public string steamUserdataPath {
			get { return Path.Combine(steamFolder, "userdata", steamID.ToString()); }
		}
		[JsonIgnore]
		public string steamShortcutsPath {
			get { return Path.Combine(steamUserdataPath, "config", "shortcuts.vdf"); }
		}
		[JsonIgnore]
		public string steamGridPath {
			get { return Path.Combine(steamUserdataPath, "config", "grid"); }
		}
	}
}
