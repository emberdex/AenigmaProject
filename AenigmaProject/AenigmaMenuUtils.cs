using System;
using System.IO;
using System.Threading;

namespace AenigmaProject
{
    public class AenigmaMenuUtils
    {
        /// <summary>
        /// The current line on which the print function relies to set the correct cursor Y position.
        /// </summary>
        private static int CurLine = 2;
        
        /// <summary>
        /// The "phone number" that the "BBS" is "dialling into".
        /// </summary>
        public static string PhoneNumber = "";
        
        /// <summary>
        /// The number of "connected users".
        /// </summary>
        public static int CurrentUsers = 0;
        
        /// <summary>
        /// The last time the game was played.
        /// </summary>
        public static DateTime LastPlayed = DateTime.MinValue;
        
        /// <summary>
        /// The number of attempts at the game over its lifetime.
        /// </summary>
        public static int LifetimeAttempts = 0;

        /// <summary>
        /// The number of failed passwords.
        /// </summary>
        public static int FailedLoginAttempts = 0;

        public static bool ShouldTimeout = true;
        public static int NumberOfTimeouts = 0;
        public static int TimeSinceInputAttempts = 0;

        /// <summary>
        /// Generates a random phone number, in the format 01632 960xxx, where xxx is between 000 and 999.
        /// This number is listed in Ofcom's range of telephone numbers for use in TV and radio drama programmes, 
        /// and is therefore guaranteed never to connect for safety purposes.
        /// </summary>
        /// <returns>The phone number, as a string.</returns>
        public static string GetRandomPhoneNumber()
        {
            return String.Format("01632 {0}", new Random().Next(960000, 960999));
        }

        /// <summary>
        /// Convenience method to clear the box and reset the line counter.
        /// Use this instead of just Console.Clear() and DrawBox() alone.
        /// </summary>
        public static void ClearBox()
        {
            Console.Clear();
            CurLine = 2;
            DrawBox();
        }

        /// <summary>
        /// Resets the cursor position and line values, but does not clear the screen.
        /// Useful for overwriting console output.
        /// </summary>
        public static void ResetCursorPosition()
        {
            CurLine = 2;
            Console.SetCursorPosition(3, CurLine);
        }

        /// <summary>
        /// Using ANSI control codes, writes a message in inverted colour text at the bottom of the screen.
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteStatusMessage(string msg)
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;
            
            AenigmaMenuUtils.ClearStatusMessage();
            
            Console.SetCursorPosition(3, Console.WindowHeight - 1);
            // Set background colour to white, and foreground colour to black.
            Console.Write("\x1B[47m\x1B[30m");
            Console.Write(msg);
            // Reset to terminal defaults.
            Console.Write("\x1B[0m");

            Console.SetCursorPosition(cursorX, cursorY);
        }

        /// <summary>
        /// Clear the status messages.
        /// </summary>
        public static void ClearStatusMessage()
        {
            int cursorX = Console.CursorLeft;
            int cursorY = Console.CursorTop;
            
            Console.SetCursorPosition(3, Console.WindowHeight - 1);
            for (int i = 3; i < Console.WindowWidth - 2; i++)
            {
                Console.Write('═');
                Console.SetCursorPosition(i, Console.WindowHeight - 1);
            }
            
            Console.SetCursorPosition(cursorX, cursorY);
        }
        
        /// <summary>
        /// Using ASCII box drawing characters, draw a box around the outside of the screen.
        /// Sets the cursor position, and adapts to the size of the current console.
        /// </summary>
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

        /// <summary>
        /// Writes a line in the box drawn using DrawBox().
        /// </summary>
        /// <param name="str">The string to write into the box.</param>
        /// <param name="timeout">Time to wait between writing each character (default = 10).</param>
        public static void WriteLineToBox(string str, int timeout = 10)
        {
            Console.SetCursorPosition(3, CurLine);
            AenigmaUtils.SlowPrint(str, timeout);
            CurLine++;
            Console.SetCursorPosition(3, CurLine);
        }

        /// <summary>
        /// Run through the main menu.
        /// </summary>
        public static void HandleMainMenu()
        {
            ClearBox();
            
            WriteLineToBox($"[Connected to Aenigma BBS via {PhoneNumber}]");
            WriteLineToBox("[INFO // https://aenigma.mynameistavis.com]");
            WriteLineToBox("[INFO // Retrieving data from BBS...]");

            int amountLoaded = 0;

            while (amountLoaded < 126)
            {
                if (amountLoaded >= 114)
                {
                    amountLoaded += (126 - amountLoaded);
                }
                
                WriteStatusMessage($"Downloaded {amountLoaded} / 126 KB.");
                
                amountLoaded += new Random().Next(5, 12);
                Thread.Sleep(300);
            }
            
            CurrentUsers = new Random().Next(30, 90);
            

            using (StreamReader sr = new StreamReader("boot/main_menu.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("{0}"))
                    {
                        line = string.Format(line, LifetimeAttempts,
                            (LastPlayed != DateTime.MinValue) ? LastPlayed.ToString("HH:mm dd/MM/yy") : "00:00 00/00/00",
                            CurrentUsers);
                    }
                    
                    line = line.Replace("{color_begin}", "\x1B[0;32m");
                    line = line.Replace("{color_end}", "\x1B[0m");
                    
                    WriteLineToBox(line, 0);
                }
            }
            
            ClearStatusMessage();
            
            Console.SetCursorPosition(14, 46);
            
            AenigmaLevel nextLevel = null;
            int LastNumberOfTimeouts = NumberOfTimeouts;
            while (nextLevel == null && LastNumberOfTimeouts == NumberOfTimeouts)
            {
                string password = AenigmaUtils.ReadLine();

                if (password == "sudoritual2216")
                {
                    Environment.Exit(255);
                }

                if (password == "sudoritual2217")
                {
                    BeginBootSequence();
                }
                
                try
                {
                    nextLevel = AenigmaLevelManager.GetLevelByPassword(password);
                }
                catch (LevelNotFoundException)
                {
                    FailedLoginAttempts++;
                    if (FailedLoginAttempts >= 3)
                    {
                        AenigmaMenuUtils.WriteStatusMessage(
                            "Whatever it is you're doing, please stop it and just type \"start\".");
                    }
                    else
                    {
                        AenigmaMenuUtils.WriteStatusMessage("Invalid password!");
                    }

                    Console.SetCursorPosition(14, 46);
                    for (int i = 0; i < password?.Length; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.SetCursorPosition(14, 46);
                }
            }

            FailedLoginAttempts = 0;
            LifetimeAttempts += 1;

            if (LastNumberOfTimeouts != NumberOfTimeouts) return;
            
            AenigmaLevelHandler.JumpToLevel(nextLevel);
        }
        
        /// <summary>
        /// Run through the boot sequence.
        /// </summary>
        public static void BeginBootSequence()
        {
            ClearBox();

            using (StreamReader sr = new StreamReader("boot/a0.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line, 0);
                }
            }

            AenigmaUtils.ReadLine();
            
            ClearBox();

            using (StreamReader sr = new StreamReader("boot/a1.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line, 0);
                }
            }

            AenigmaUtils.ReadLine();

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

                PhoneNumber = GetRandomPhoneNumber();
                while ((line = sr.ReadLine()) != null)
                {
                    WriteLineToBox(line.Replace("XXXXX XXXXXX", PhoneNumber), 0);
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
        }
    }
}