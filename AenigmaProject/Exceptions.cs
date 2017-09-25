using System;

namespace AenigmaProject
{
    /// <summary>
    /// Exception thrown if a level to be loaded is not found.
    /// </summary>
    public class LevelNotFoundException : Exception
    {
        public LevelNotFoundException(string message) : base(message)
        {
            
        }
    }
    
    /// <summary>
    /// Exception thrown if a level fails to load.
    /// </summary>
    public class LevelLoadException : Exception
    {
        public string FilePath;
        public LevelLoadException(string message) : base(message)
        {
            
        }

        public LevelLoadException(string message, string filePath)
        {
            this.FilePath = filePath;
        }
    }

    public class InvalidLevelException : Exception
    {
        public InvalidLevelException(string message) : base(message)
        {
            
        }
    }

    /// <summary>
    /// Exception thrown if level JSON is invalid.
    /// </summary>
    public class InvalidLevelDataException : Exception
    {
        public Exception OriginalException;
        public string Path;
        public InvalidLevelDataException(string message) : base(message)
        {
            
        }

        public InvalidLevelDataException(string message, Exception e, String path) : base(message)
        {
            this.OriginalException = e;
            this.Path = path;
        }
    }
}