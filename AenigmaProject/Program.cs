using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;

namespace AenigmaProject
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        
        public static String LevelPath = "stages/";
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Initialising Project Aenigma...");

            if (args.Length > 0)
            {
                LevelPath = args[0];
            }
            
            Console.WriteLine($"Reading levels from {LevelPath}.");

            Console.CancelKeyPress += delegate
            {
                AenigmaMenuUtils.WriteStatusMessage("Nice try!");
            };

            if (System.Environment.OSVersion.ToString().Contains("Windows"))
            {
                Console.WriteLine("Running on Windows - enabling ANSI support.");
                SetConsoleMode(Process.GetCurrentProcess().MainWindowHandle, 0x0200);
            }

            Console.Read();
            AenigmaMenuUtils.BeginBootSequence();
            Console.Read();
        }
    }
}