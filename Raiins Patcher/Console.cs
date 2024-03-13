
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Raiin_Patcher
{
    class DeveloperConsole
    {
        #region Variables

        [DllImport("kernel32")]
        static extern bool AllocConsole();

        static List<string> lLog = new List<string>();
        #endregion

        public static void Create()
        {
            AllocConsole();
        }

        public static class Write
        {
            public static void Download(string Text)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" -> " + Text);
            }

            public static void Log(string Text)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" -> " + Text);
            }
        }      
    }
}
