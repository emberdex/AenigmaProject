using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

namespace AenigmaProject
{
    public enum AenigmaLevelType
    {
        Level, Cutscene
    }
    
    public class AenigmaLevel
    {
        public Guid ID;
        public AenigmaLevelType LevelType;
        public string Password;
        public string Data;
        public string CorrectAnswer;
        public Point CursorCoords;
        public Guid NextStage;
        public Guid FailedStage;
        public bool IsFinalStage;
        public int Attempts;

        public AenigmaLevel()
        {
            
        }

        public AenigmaLevel(Guid id, AenigmaLevelType type, string password, string data, string correctAnswer,
            Point cursorCoords, Guid nextStage, Guid failedStage, bool isFinalStage, int attempts)
        {
            this.ID = id;
            this.LevelType = type;
            this.Password = password;
            this.Data = data;
            this.CorrectAnswer = correctAnswer;
            this.CursorCoords = cursorCoords;
            this.NextStage = nextStage;
            this.FailedStage = failedStage;
            this.IsFinalStage = isFinalStage;
            this.Attempts = attempts;
        }

        public static AenigmaLevel Deserialize(string json)
        {
            AenigmaLevel tmp = JsonConvert.DeserializeObject<AenigmaLevel>(json,
                new JsonSerializerSettings
                {
                    Error = delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                    {
                        throw new LevelLoadException(
                            $"Encountered {args.ErrorContext.Error} while deserialising level data.");
                    }
                }
            );

            return tmp;
        }
    }
}