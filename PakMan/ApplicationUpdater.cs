using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PakMan
{
	partial class MainForm
	{
		private string checkForApplicationUpdates(bool silent=false) {
			if(!ApplicationDeployment.IsNetworkDeployed) {
				if (!silent) MessageBox.Show("Sorry, portable version can't check for updates.");
				return this.ProductVersion.TrimEnd(new char[] { '0', '.' }) + " Portable";
			}

			ApplicationDeployment ad = null;
			UpdateCheckInfo info = null;
			try {
				ad = ApplicationDeployment.CurrentDeployment;
				info = ad.CheckForDetailedUpdate();
			}
			catch (InvalidDeploymentException ide) {
				MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
				return this.ProductVersion.TrimEnd(new char[] { '0', '.' }) + " CO Deployment Corrupt";
			}
			catch (DeploymentDownloadException dde) {
				MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
				return this.ProductVersion.TrimEnd(new char[] { '0', '.' }) + "-d";
			}
			catch (InvalidOperationException ex) {
				// There's probably just no update out
				if (!silent) {
					MessageBox.Show("Nope, no update out. Latest version is still just " + info.AvailableVersion);
				}
				return ad.CurrentVersion.ToString(ad.CurrentVersion.Revision == 0 ? 3 : 4);
			}
			if(info == null) {
				return this.ProductVersion.TrimEnd(new char[] { '0', '.' }) + "-?";
			}
			else if (info.UpdateAvailable) {
				if (silent || MessageBox.Show("New update: v" + info.AvailableVersion + ", update?", Application.ProductName + " Updater", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK) {
					ad.Update();
					//Application.ExitThread();   
					MessageBox.Show("About to");
					Application.Restart();
					MessageBox.Show("Uhm");
					Application.Exit();
					MessageBox.Show("UHHH");
					Environment.Exit(0);
					MessageBox.Show("YOU'RE DEAD NOW, RIGHT");
				}
			}
			else if (!silent) {
				MessageBox.Show("Nope, no update. Latest version is still just " + ad.CurrentVersion);
			}
			return ad.CurrentVersion.ToString(ad.CurrentVersion.Revision == 0 ? 3 : 4);
		}

		private void addDLLReflection() {
			AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => {
				string resourceName = new AssemblyName(args.Name).Name + ".dll";
				string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

				using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource)) {
					Byte[] assemblyData = new Byte[stream.Length];
					stream.Read(assemblyData, 0, assemblyData.Length);
					return Assembly.Load(assemblyData);
				}
			};
		}
	}
}
