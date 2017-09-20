using System;
using System.Diagnostics;

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

            Console.TreatControlCAsInput = true;
            Console.CancelKeyPress += delegate
            {
                AenigmaMenuUtils.WriteStatusMessage("Nice try!");
            };

            Console.Read();
            AenigmaMenuUtils.BeginBootSequence();
            Console.Read();
        }
    }
}