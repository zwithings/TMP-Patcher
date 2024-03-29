
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Windows;

namespace Raiin_Patcher
{
    class Functions
    {
        public static string GetHashRaw(string File_Name)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(File_Name))
                    {
                        var hash = md5.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "GetHashRaw Error", MessageBoxButton.OK,MessageBoxImage.Error);
                Environment.Exit(0);
                return "";
            }           
        }

        public static string GetName(string Line)
        {
            try
            {
                return Line.Split('|')[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "GetName Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return "";
            }
        }

        public static string GetHash(string Line)
        {
            try
            {
                return Line.Split('|')[1];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "GetHash Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return "";
            }
        }

        public static long GetSize(string Line)
        {
            try
            {
                var parts = Line.Split('|');
                if (parts.Length < 3)
                {
                    MessageBox.Show("Nieprawidlowy format linii: " + Line, "GetSize Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(0);
                }
                return long.Parse(parts[2]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "GetSize Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return 0;
            }
        }

        public static long GetBytesNeed(List<string> Patchlist)
        {
            try
            {
                long Result = 0;
                foreach(string i in Patchlist)
                {
                    Result += GetSize(i);
                }
                return Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "GetBytesNeed Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return 0;
            }
        }

        public static int GetFilesNeed(List<string> Patchlist)
        {
            try
            {
                return Patchlist.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "GetFilesNeed Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return 0;
            }
        }

        public static string GetFolder(string Line)
        {
            try
            {
                string Result = "";
                for (int i = 0; i <= Line.Split('\\').Count() - 2; i++)
                {
                    Result += Line.Split('\\')[i] + "\\";
                }
                return Result.Remove(Result.Length - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "GetFolder Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return "";
            }
        }
        

        public static List<string> GetPatchlist(string Patchlist_Name)
        {
            try
            {
                List<string> Result = new List<string>();
                string[] TMP_Patchlist = File.ReadAllLines(Patchlist_Name);
                File.Delete(Patchlist_Name);
                if (TMP_Patchlist.Count() > 0)
                {
                    double iHave = 0;
                    double iNeed = TMP_Patchlist.Count();

                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        MainWindow.WPF.IsEnabled = false;
                        Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.img_pb_full.Width = 0; }));
                        Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.tb_pb_speed.Text = "Checking local..."; }));
                    }));

                    foreach (string i in TMP_Patchlist)
                    {
                        if (GetSize(i) != 0)
                        {
                            if (File.Exists(GetCurrentFolder() + GetName(i)))
                            {
                                DeveloperConsole.Write.Log(GetHashRaw(GetCurrentFolder() + GetName(i)));
                                if (GetHashRaw(GetCurrentFolder() + GetName(i)) != GetHash(i))
                                {
                                    Result.Add(i);
                                }
                            }
                            else
                            {
                                Result.Add(i);
                            }
                        }
                        iHave++;
                        Application.Current.Dispatcher.Invoke(new Action(() => {
                            MainWindow.WPF.img_pb_full.Width = iHave / iNeed * DownloadFiles.dPWidth;
                            MainWindow.WPF.tb_pb_full.Text = GetName(i) + " (" + Math.Round(iHave / iNeed * 100, 0) + "%)";
                        }));
                    }
                    Application.Current.Dispatcher.Invoke(new Action(() => {
                        MainWindow.WPF.IsEnabled = true;
                        Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.img_pb_full.Width = DownloadFiles.dPWidth; }));
                        Application.Current.Dispatcher.Invoke(new Action(() => { MainWindow.WPF.tb_pb_speed.Text = "Finished"; }));
                    }));
                }
                return Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "GetPatchlist Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return new List<string>();
            }
        }

        public static string GetCurrentFolder()
        {
            try
            {
                return Directory.GetCurrentDirectory() + "\\";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "GetCurrentFolder Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return "";
            }
        }
    }
}
