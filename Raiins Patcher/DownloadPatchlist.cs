
using System;
using System.IO;
using System.Net;
using System.Windows;

namespace Raiin_Patcher
{
    class DownloadPatchlist
    {
        public static void WC_DownloadPatchlist()
        {
            DeveloperConsole.Write.Download("Download actual: Patchlist.raiin");
            Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.btn_start.IsEnabled = false; }));
            WebClient WC_NewDownload = new WebClient();
            WC_NewDownload.DownloadProgressChanged += WC_NewDownload_DownloadProgressChanged;
            WC_NewDownload.DownloadFileCompleted += WC_NewDownload_DownloadFileCompleted;
            WC_NewDownload.Proxy = null;
            WC_NewDownload.DownloadFileAsync(new Uri(Config.Patcher.Links.Patchlist), Functions.GetCurrentFolder() + "Patchlist.raiin");
        }

        private static void WC_NewDownload_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //Laden
        }

        private static void WC_NewDownload_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            DeveloperConsole.Write.Download("Download: Patchlist.raiin");
            MainWindow.BytesHave = 0;
            MainWindow.BytesNeed = 0;
            MainWindow.FilesHave = 0;
            MainWindow.FilesNeed = 0;
            DeveloperConsole.Write.Log("Checking Patchlist now: " + "Patchlist.raiin");
            DateTime Time1 = DateTime.Now; ;
            if(File.ReadAllText(Functions.GetCurrentFolder() + "Patchlist.raiin") != "update")
            {
                MainWindow.Patchlist = Functions.GetPatchlist(Functions.GetCurrentFolder() + "Patchlist.raiin");
                DateTime Time2 = DateTime.Now;
                DeveloperConsole.Write.Log("Time elapsed: " + (Time2.TimeOfDay - Time1.TimeOfDay).ToString());
                DeveloperConsole.Write.Log("Patchlist checked successfully: " + "Patchlist.raiin");
                if (MainWindow.Patchlist.Count > 0)
                {
                    MainWindow.BytesNeed = Functions.GetBytesNeed(MainWindow.Patchlist);
                    MainWindow.FilesNeed = Functions.GetFilesNeed(MainWindow.Patchlist);
                    Start.Files_Thread();
                }
                else if (MainWindow.Patchlist.Count == 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.btn_start.IsEnabled = true; }));
                    DeveloperConsole.Write.Log("Nothing to patch");
                    Start.Start_EXE();
                }
            }
            else
            {
                Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.btn_start.IsEnabled = true; }));

                File.Delete(Functions.GetCurrentFolder() + "Patchlist.raiin");

                DeveloperConsole.Write.Log("Client will updated...");
                Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.tb_pb_speed.Text = "The client is currently being modified! Try again in 5 minutes."; }));
            }

        }
    }
}
