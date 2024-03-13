
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

namespace Raiin_Patcher

{
    class DownloadFiles
    {
        public const double dPWidth = 634;

        public static void WC_DownloadFiles()
        {
            if (Functions.GetName(MainWindow.Patchlist[MainWindow.FilesHave]).Contains("\\"))
            {
                Directory.CreateDirectory(Functions.GetFolder(Functions.GetCurrentFolder() + Functions.GetName(MainWindow.Patchlist[MainWindow.FilesHave])));
            }
            DeveloperConsole.Write.Download("Download atual: " + Functions.GetName(MainWindow.Patchlist[MainWindow.FilesHave]));
            WebClient WC_NewDownload = new WebClient();
            WC_NewDownload.DownloadProgressChanged += WC_NewDownload_DownloadProgressChanged;
            WC_NewDownload.DownloadFileCompleted += WC_NewDownload_DownloadFileCompleted;
            WC_NewDownload.Proxy = null;
            WC_NewDownload.DownloadFileAsync(new Uri(Config.Patcher.Links.Client + Functions.GetName(MainWindow.Patchlist[MainWindow.FilesHave])), Functions.GetCurrentFolder() + Functions.GetName(MainWindow.Patchlist[MainWindow.FilesHave]), Stopwatch.StartNew());
        }
    
        private static void WC_NewDownload_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //Total
            {
                double BytesHave = double.Parse((MainWindow.BytesHave + e.BytesReceived).ToString());
                double BytesNeed = double.Parse(MainWindow.BytesNeed.ToString());
                double Width = Math.Round(BytesHave / BytesNeed * dPWidth, 10);
                double Percent = Math.Round(BytesHave / BytesNeed * 100, 0);

                if ((Width <= dPWidth && Width >= 0) && (Percent <= 100 && Percent >= 0))
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.img_pb_full.Width = Width; }));
                    Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.tb_pb_full.Text = Percent + "% (" + Functions.GetName(MainWindow.Patchlist[MainWindow.FilesHave]) + ")"; }));
                }
            }

            //Speed
            {
                double BytesHave = double.Parse(e.BytesReceived.ToString());
                double StopWatch = double.Parse(((Stopwatch)e.UserState).ElapsedMilliseconds.ToString());
                double KBPerSeccond = Math.Round(BytesHave / StopWatch, 0);

                Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.tb_pb_speed.Text = KBPerSeccond + " KB/s"; }));
            }
        }

        private static void WC_NewDownload_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            DeveloperConsole.Write.Download("Download terminado: " + Functions.GetName(MainWindow.Patchlist[MainWindow.FilesHave]));
            MainWindow.BytesHave += Functions.GetSize(MainWindow.Patchlist[MainWindow.FilesHave]);
            MainWindow.FilesHave += 1;
            if (MainWindow.FilesHave == MainWindow.FilesNeed)
            {
                Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.btn_start.IsEnabled = true; }));
                Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.img_pb_full.Width = dPWidth; }));
                Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.tb_pb_speed.Text = "Terminado"; }));
                DeveloperConsole.Write.Log("Todos os ficheiros foram atualizados com sucesso: " + MainWindow.FilesHave + "/" + MainWindow.FilesNeed);
                Start.Start_EXE();
            }
            else
            {
                WC_DownloadFiles();
            }
        }
    }
}
