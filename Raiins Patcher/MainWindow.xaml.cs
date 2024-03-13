using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Diagnostics;

namespace Raiin_Patcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables

        public static List<string> Patchlist;

        public static string PatcherHash;

        public static long BytesHave;
        public static long BytesNeed;

        public static int FilesHave;
        public static int FilesNeed;

        public static MainWindow WPF;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            WPF = this;

            //Start.PatcherHash_Thread();
        }


        private void img_bg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btn_start_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Start.Patchlist_Thread();
        }

        private void btn_minimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_quit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btn_config_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Start.Config_EXE();
        }

        private void btn_openweb_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Process.Start(Config.Patcher.Settings.ServerUrl);
        }

        private void btn_opewiki_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Process.Start(Config.Patcher.Settings.ServerUrlWiki);
        }

        private void btn_opensuport_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Process.Start(Config.Patcher.Settings.ServerUrlSupport);
        }

        private void btn_openfacebook_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Process.Start(Config.Patcher.Settings.ServerUrlFacebook);
        }

        private void btn_opendiscord_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Process.Start(Config.Patcher.Settings.ServerUrlDiscord);
        }

        private void btn_openyoutube_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Process.Start(Config.Patcher.Settings.ServerUrlYoutube);
        }

        private void btn_website_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Process.Start(Config.Patcher.Settings.ServerUrlWebsite);
        }

        private void btn_comunnity_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Process.Start(Config.Patcher.Settings.ServerUrlCommunity);
        }

        private void btn_forum_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            Process.Start(Config.Patcher.Settings.ServerUrlForum);
        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            
        }
    }
}