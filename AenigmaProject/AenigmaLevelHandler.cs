using System;
using System.Drawing.Imaging;
using System.Threading;

namespace AenigmaProject
{
    public class AenigmaLevelHandler
    {
        /// <summary>
        /// The GUID of the current level.
        /// </summary>
        public static Guid CurrentLevelGuid;

        /// <summary>
        /// The current level object.
        /// </summary>
        public static AenigmaLevel CurrentLevel = null;

        /// <summary>
        /// The number of tries at the puzzle since the program was started.
        /// </summary>
        public static int NumberOfAttempts = 0;
        
        /* /// <summary>
        /// The time the game was last played.
        /// </summary>
        public static DateTime LastVisit = DateTime.UtcNow;
        
        EARMARKED FOR REMOVAL - DUPLICATE OF AenigmaMenuUtils.LastPlayed*/
        
        /// <summary>
        /// The number of lives the player has remaining.
        /// </summary>
        public static int NumberOfLives = 3;

        public static void HandleLevel(AenigmaLevel level)
        {
            AenigmaLevel l = AenigmaLevelManager.GetLevelById(level.NextStage);

            Console.Read();
            if(!l.IsFinalStage) JumpToLevel(l);
            else AenigmaMenuUtils.HandleMainMenu();
        }

        public static void JumpToLevel(AenigmaLevel level)
        {
            // Do some sanity checking on the level.
            if (level == null)
            {
                throw new InvalidLevelException("Cannot load a null level.");
            }
            
            if (level.ID == Guid.Empty)
            {
                throw new InvalidLevelException("Cannot load a level with an empty GUID.");
            }

            if (string.IsNullOrEmpty(level.Data) || string.IsNullOrWhiteSpace(level.Data))
            {
                throw new InvalidLevelException("Cannot load a level with no level data.");
            }
            
            CurrentLevel = level;
            
            // Reset the lives counter to 3.
            NumberOfLives = 3;
            
            // If this is the starting level, then increment the attempts counter and set the "last played" time.
            if (CurrentLevel.Password != null && CurrentLevel.Password.ToLower() == "start")
            {
                NumberOfAttempts++;
                AenigmaMenuUtils.LastPlayed = DateTime.UtcNow;
            }
            
            // Draw the level data.
            Console.Clear();
            Console.Write(level.Data);
            
            HandleLevel(CurrentLevel);
        }
    }
}