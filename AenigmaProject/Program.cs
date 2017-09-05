using System;
using System.Collections.Generic;

namespace AenigmaProject
{
    internal class Program
    {
        public static String LevelPath = "stages/";
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Initialising Project Aenigma...");

            if (args.Length > 0)
            {
                LevelPath = args[0];
            }
            
            Console.WriteLine($"Reading levels from {LevelPath}.");
            
            AenigmaMainMenu.BeginBootSequence();
            //AenigmaLevelManager.LoadLevelsFromDirectory(LevelPath);
        }
    }
}