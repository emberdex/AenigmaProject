using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace AenigmaProject
{
    public class AenigmaMenuUtils
    {
        private static int cur_line = 2;
        public static string phone_number = "";
        

        public static string GetRandomPhoneNumber()
        {
            return String.Format("01632 {0}", new Random().Next(960000, 960999));
        }

        public static void ClearBox()
        {
            Console.Clear();
            cur_line = 2;
            DrawBox();
        }

        public static void ResetCursorPosition()
        {
            cur_line = 2;
            Console.SetCursorPosition(3, cur_line);
        }

        public static void WriteStatusMessage(string msg)
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;
            
            Console.SetCursorPosition(3, Console.WindowHeight - 1);
            Console.Write("\x1B[47m\x1B[30m");
            Console.Write(msg);
            Console.Write("\x1B[0m");

            Console.SetCursorPosition(cursorX, cursorY);
        }
        
        public static void DrawBox()
        {
            Console.SetCursorPosition(0, 0);
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
                            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
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

        public static void HandleMainMenu()
        {
            ClearBox();
            
            WriteLineToBox($"[Connected to Aenigma BBS via {phone_number}]");
            WriteLineToBox("[INFO // https://aenigma.mynameistavis.com]");
            WriteLineToBox("[INFO // Retrieving data from BBS...]");

            int amount_loaded = 0;

            while (amount_loaded < 126)
            {
                if (amount_loaded >= 114)
                {
                    amount_loaded += (126 - amount_loaded);
                }
                
                WriteStatusMessage($"Downloaded {amount_loaded} / 126 KB.");
                
                amount_loaded += new Random().Next(5, 12);
                Thread.Sleep(300);
            }

            using (StreamReader sr = new StreamReader("boot/main_menu.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line, 0);
                }
            }
        }
        
        public static void BeginBootSequence()
        {
            ClearBox();

            using (StreamReader sr = new StreamReader("boot/bios.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line, 0);
                }
            }

            
            Thread.Sleep(2500);

            ClearBox();
            
            using (StreamReader sr = new StreamReader("boot/bios2.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line, 0);
                }
            }
            
            Thread.Sleep(2500);
            
            ClearBox();

            using (StreamReader sr = new StreamReader("boot/floppy.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line, 0);
                }
            }

            Thread.Sleep(500);
            
            using (StreamReader sr = new StreamReader("boot/floppy2.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line, 2);
                }
            }
            
            Thread.Sleep(2000);
            
            using (StreamReader sr = new StreamReader("boot/floppy3.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line, 2);
                }
            }
            
            Thread.Sleep(500);
            
            using (StreamReader sr = new StreamReader("boot/floppy4.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line, 0);
                }
            }
            
            Thread.Sleep(100);
            
            using (StreamReader sr = new StreamReader("boot/floppy5.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line, 0);
                }
            }
            
            Thread.Sleep(1000);
            
            using (StreamReader sr = new StreamReader("boot/floppy6.txt"))
            {
                string line;

                phone_number = GetRandomPhoneNumber();
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line.Replace("XXXXX XXXXXX", phone_number), 0);
                }
            }

            Thread.Sleep(2500);
            
            using (StreamReader sr = new StreamReader("boot/floppy7.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line, 0);
                }
            }
            
            Thread.Sleep(500);

            HandleMainMenu();
            
            Console.ReadLine();
        }
    }
}