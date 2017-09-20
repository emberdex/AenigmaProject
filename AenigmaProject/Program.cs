using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace AenigmaProject
{
    internal class Program
    {
        public static String LevelPath = "stages";
        
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
                Console.WriteLine($"Failed to load levels: {lle.Message}");
            }

            Console.TreatControlCAsInput = true;
            Console.CancelKeyPress += delegate
            {
                AenigmaMenuUtils.WriteStatusMessage("Nice try!");
            };
            
            AenigmaMenuUtils.BeginBootSequence();
        }
    }
}