using System;

namespace AenigmaProject
{
    public class LevelNotFoundException : Exception
    {
        public LevelNotFoundException(string message) : base(message)
        {
            
        }
    }
    
    public class LevelLoadException : Exception
    {
        public LevelLoadException(string message) : base(message)
        {
            
        }
    }

    public class InvalidLevelException : Exception
    {
        public InvalidLevelException(string message) : base(message)
        {
            
        }
    }
}