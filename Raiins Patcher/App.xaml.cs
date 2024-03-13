using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Raiin_Patcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                Environment.Exit(0);
            }
            else
            {
                if (e.Args.Count() == 1)
                {
                    if (e.Args[0] == "raiin")
                    {
                        DeveloperConsole.Create();
                    }
                }
            }
        }
    }
}
