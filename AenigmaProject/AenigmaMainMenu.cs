using System;
using System.Threading;

namespace AenigmaProject
{
    public class AenigmaMainMenu
    {
        private static int cur_line = 2;

        public static string GetRandomPhoneNumber()
        {
            return String.Format("01632 {0}", new Random().Next(960000, 960999));
        }
        
        public static void DrawBox()
        {
            for (int h = 0; h < Console.WindowHeight; h++)
            {
                for (int w = 0; w < Console.WindowWidth; w++)
                {
                    if (h == 0)
                    {
                        if     (w == 0)                       Console.Write("╔");
                        else if(w == Console.WindowWidth - 1) Console.Write("╗");
                        else                                  Console.Write("═");
                    }

                    else if (h == Console.WindowHeight - 1)
                    {
                        if     (w == 0)                       Console.Write("╚");
                        else if(w == Console.WindowWidth - 1) Console.Write("╝");
                        else                                  Console.Write("═");
                    }
                    
                    else
                    {
                        if(w == 0 || w == Console.WindowWidth - 1) Console.Write("║");
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
            }
        }

        public static void WriteLineToBox(string str, int timeout = 10)
        {
            Console.SetCursorPosition(3, cur_line);
            AenigmaUtils.SlowPrint(str, timeout);
            cur_line++;
            Console.SetCursorPosition(3, cur_line);
        }
        
        public static void BeginBootSequence()
        {
            DrawBox();
            WriteLineToBox(" _............_", 0);
            WriteLineToBox("| |         | |", 0);
            WriteLineToBox("| | AENIGMA | |    AEnigma Firmware Interface (AEFI) version v0.4a", 0);
            WriteLineToBox("| | 18/9/17 | |    Design:      Tavis Booth", 0);
            WriteLineToBox("| |_________| |    Programming: Toby Jones", 0);
            WriteLineToBox("|   _______   |    =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=", 0);
            WriteLineToBox("|  |     | |  |", 0);
            WriteLineToBox("|__|_____|_|__|", 0);
            WriteLineToBox("", 0);
            WriteLineToBox("Initialising the Aenigma Project...");
            Thread.Sleep(new Random().Next(1000, 3000));
            WriteLineToBox("Reticulating splines...");
            Thread.Sleep(new Random().Next(1000, 3000));
            WriteLineToBox("Respawning all llamas...");
            Thread.Sleep(new Random().Next(1000, 3000));
            WriteLineToBox("TURBO BUTTON PRESSED - ENGAGING HYPERSPEED...\n", 2);
            WriteLineToBox("");
            WriteLineToBox($"Dialling BBS on {GetRandomPhoneNumber()}...", 2);
            Thread.Sleep(new Random().Next(1000, 3000));
            WriteLineToBox("*twiddles thumbs*", 100);
            Thread.Sleep(new Random().Next(1000, 1500));
            WriteLineToBox("Connected - downloading data.", 2);
            Console.ReadLine();
        }
    }
}