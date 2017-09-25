using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace AenigmaProject
{
    public class AenigmaLevelManager
    {
        /// <summary>
        /// The list of levels.
        /// </summary>
        private static List<AenigmaLevel> levels = new List<AenigmaLevel>();

        /// <summary>
        /// Adds a level to the list.
        /// </summary>
        /// <param name="level">The level to add to the list.</param>
        /// <exception cref="NullReferenceException">Thrown if a null level, or a level with a GUID equivalent to Guid.Empty, is passed in.</exception>
        public static void InsertLevel(AenigmaLevel level)
        {
            if (level?.ID != Guid.Empty)
            {
                levels.Add(level);
            }
            else
            {
                throw new NullReferenceException("Tried to add an empty or NULL level to the list.");
            }
        }

        /// <summary>
        /// Removes a level from the list.
        /// </summary>
        /// <param name="id">The level to remove, by GUID.</param>
        /// <exception cref="LevelNotFoundException">Thrown if a level with the specified GUID is not found.</exception>
        public static void RemoveLevel(Guid id)
        {
            if (id != Guid.Empty)
            {
                foreach (AenigmaLevel level in levels)
                {
                    if (level?.ID == id)
                    {
                        levels.Remove(level);
                    }
                }
            }
            
            throw new LevelNotFoundException("A level with the specified GUID could not be found.");
        }
        
        /// <summary>
        /// Function to find a level in the list with a specific GUID.
        /// </summary>
        /// <param name="id">The GUID to match against.</param>
        /// <returns>The level matching the GUID.</returns>
        /// <exception cref="LevelNotFoundException">Thrown if a level with the specified GUID is not found.</exception>
        public static AenigmaLevel GetLevelById(Guid id)
        {
            foreach (AenigmaLevel level in levels)
            {
                if (level.ID.Equals(id)) return level;
            }
            
            throw new LevelNotFoundException("A level with the specified GUID could not be found.");
        }
        
        /// <summary>
        /// Loads levels from a given directory.
        /// </summary>
        /// <param name="path">The path to load levels from.</param>
        /// <exception cref="LevelLoadException">Thrown if an error occurs while loading levels.</exception>
        public static void LoadLevelsFromDirectory(String path)
        {
            if (Directory.Exists(path))
            {
                foreach(string file in Directory.EnumerateFiles(path).Where(n => n.EndsWith(".stage")))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        try
                        {
                            levels.Add(AenigmaLevel.Deserialize(sr.ReadToEnd()));
                        }
                        catch (JsonReaderException jre)
                        {
                            throw new InvalidLevelDataException("", jre, file);
                        }
                    }
                }
            }
            else
            {
                throw new LevelLoadException("The specified path does not exist.");
            }
        }

        /// <summary>
        /// Function to get a level by password, much like GetLevelByGuid.
        /// </summary>
        /// <param name="password">The password to match against.</param>
        /// <returns>The level matching the password, if found.</returns>
        /// <exception cref="LevelNotFoundException">Thrown if a level with the specified password is not found.</exception>
        public static AenigmaLevel GetLevelByPassword(string password)
        {
            foreach (AenigmaLevel level in levels)
            {
                if (String.Compare(level.Password, password, true, CultureInfo.CurrentCulture) == 0) return level;
            }
            
            throw new LevelNotFoundException("A level with the specified password could not be found.");
        }
    }
}