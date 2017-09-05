using System;

namespace AenigmaProject
{
    public class AenigmaLevelHandler
    {
        public static Guid CurrentLevelGuid;
        public static AenigmaLevel CurrentLevel;

        public static int NumberOfAttempts = 0;
        public static DateTime LastVisit = DateTime.UtcNow;
        
        public static void JumpToLevel(Guid levelGuid)
        {
            // Clear the screen.
            Console.Clear();

            // Get the level by GUID.
            try
            {
                CurrentLevel = AenigmaLevelManager.GetLevelById(levelGuid);
            }
            catch (LevelNotFoundException lnfe)
            {
                Console.WriteLine($"Can't jump to level GUID {levelGuid} because it doesn't exist.");
            }
        }
    }
}