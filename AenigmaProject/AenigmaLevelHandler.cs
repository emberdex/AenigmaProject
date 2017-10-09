using System;
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
        
        EARMARKED FOR REMOVAL - DUPLICATE OF AenigmaMenuUtils.LastPlayed */
        
        /// <summary>
        /// The number of lives the player has remaining.
        /// </summary>
        public static int NumberOfLives = 3;

        public static string HandleLevelUserInput()
        {
            Console.SetCursorPosition(26, 23);

            string tmp = AenigmaUtils.ReadLine();
            
            Console.SetCursorPosition(26, 23);
            for (int i = 27; i < 45; i++)
            {
                Console.Write("_");
            }
            
            Console.SetCursorPosition(26, 23);
            return tmp;
        }

        public static void HandleLevel(AenigmaLevel level)
        {
            AenigmaLevel nextLevel = AenigmaLevelManager.GetLevelById(level.NextStage);
            
            if (level.LevelType == AenigmaLevelType.Level)
            {
                Console.SetCursorPosition(107, 43);
                Console.Write("   ");
                Console.SetCursorPosition(107, 43);
                for (int i = 0; i < NumberOfLives; i++)
                {
                    Console.Write("X");
                }
                
                int LastNumberOfTimeouts = AenigmaMenuUtils.NumberOfTimeouts;
                while (NumberOfLives > 0 && LastNumberOfTimeouts == AenigmaMenuUtils.NumberOfTimeouts)
                {
                    string response = HandleLevelUserInput();

                    if (response.ToLower().Equals(level.CorrectAnswer.ToLower()))
                    {
                        break;
                    }

                    else
                    {
                        NumberOfLives -= 1;
                        
                        Console.SetCursorPosition(107, 43);
                        Console.Write("   ");
                        Console.SetCursorPosition(107, 43);
                        for (int i = 0; i < NumberOfLives; i++)
                        {
                            Console.Write("X");
                        }
                        
                        Thread.Sleep(1000);
                    }
                }
            } 
            else if (level.LevelType == AenigmaLevelType.Cutscene)
            {
                if (level.ID != Guid.Parse("994495c4-7bef-4e39-a449-55a3d39743fa") && level.ID != Guid.Parse("38f677db-e167-4f9e-be18-a5def9b7a683"))
                {
                    AenigmaMenuUtils.WriteStatusMessage("Press ENTER to continue.");
                }

                Console.ReadKey(true);
                AenigmaMenuUtils.TimeSinceInputAttempts = 0;
            }

            if (NumberOfLives == 0)
            {
                AenigmaMenuUtils.WriteStatusMessage("You have been banned. Reason: Brute forcing.");
                Thread.Sleep(1000);
                
                AenigmaMenuUtils.HandleMainMenu();
            }
            else
            {
                NumberOfLives = 3;
                if(!nextLevel.IsFinalStage) JumpToLevel(nextLevel);
                else AenigmaMenuUtils.HandleMainMenu();
            }
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
            AenigmaUtils.SlowPrint(level.Data, 0);
            
            HandleLevel(CurrentLevel);
        }
    }
}