using System;
using System.CodeDom;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using Newtonsoft.Json;

namespace AenigmaProject
{
    internal class Program
    {
        public static String LevelPath = "stages";
        public const int Timeout = 300;
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Initialising Project Aenigma...");

            if (args.Length > 0)
            {
                LevelPath = args[0];
            }
            
            Console.WriteLine($"Reading levels from {LevelPath}.");

            try
            {
                AenigmaLevelManager.LoadLevelsFromDirectory(LevelPath);
            }
            catch (LevelLoadException lle)
            {
                Console.WriteLine(lle.Message);
                Console.WriteLine("An error occurred while loading a level. Exiting.");
                Thread.Sleep(20000);
                Environment.Exit(1);
            }
            catch (InvalidLevelDataException ilde)
            {
                Console.WriteLine("ERROR: An attempt to load invalid level data was made.");
                Console.WriteLine($"Offending file: {ilde.Path}");
                Console.WriteLine($"{ilde.OriginalException.GetType()}: {ilde.OriginalException.Message}");
                Thread.Sleep(20000);
                Environment.Exit(1);
            }

            Console.TreatControlCAsInput = true;
            Console.CancelKeyPress += delegate
            {
                AenigmaMenuUtils.WriteStatusMessage("Nice try!");
                return;
            };
            
            Thread t = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(1000);
                        if (AenigmaMenuUtils.ShouldTimeout)
                        {
                            AenigmaMenuUtils.TimeSinceInputAttempts += 1;
                            if (AenigmaMenuUtils.TimeSinceInputAttempts >= Timeout)
                            {
                                AenigmaMenuUtils.NumberOfTimeouts += 1;
                                AenigmaMenuUtils.BeginBootSequence();
                            }
                        }
                    }
                }
            ));

            t.Start();
            
            AenigmaMenuUtils.BeginBootSequence();
        }
    }
}