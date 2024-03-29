
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;

namespace Raiin_Patcher
{
    class Start
    {
        public static void URL(string Link)
        {
            Process.Start(Link);
            DeveloperConsole.Write.Log(Link + " started");
        }

        public static void Start_EXE()
        {
            if(File.Exists(Functions.GetCurrentFolder() + Config.Patcher.Names.Start))
            {
                Process NewProcess = new Process();
                NewProcess.StartInfo.FileName = Functions.GetCurrentFolder() + Config.Patcher.Names.Start;
                NewProcess.Start();
                DeveloperConsole.Write.Log(Config.Patcher.Names.Start + " started");
            }
            else
            {
                MessageBox.Show(Config.Patcher.Names.Start + " not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void Config_EXE()
        {
            if (File.Exists(Functions.GetCurrentFolder() + Config.Patcher.Names.Config))
            {
                Process NewProcess = new Process();
                NewProcess.StartInfo.FileName = Functions.GetCurrentFolder() + Config.Patcher.Names.Config;
                NewProcess.Start();
                DeveloperConsole.Write.Log(Config.Patcher.Names.Config + " started");
            }
            else
            {
                MessageBox.Show(Config.Patcher.Names.Config + " not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void Patchlist_Thread()
        {
            Thread NewThread = new Thread(DownloadPatchlist.WC_DownloadPatchlist);
            NewThread.SetApartmentState(ApartmentState.STA);
            NewThread.Start();
            DeveloperConsole.Write.Log("Patchlist_Thread" + " started");
        }

        public static void Files_Thread()
        {
            Thread NewThread = new Thread(DownloadFiles.WC_DownloadFiles);
            NewThread.SetApartmentState(ApartmentState.STA);
            NewThread.Start();
            DeveloperConsole.Write.Log("Files_Thread" + " started");
        }
    }
}
