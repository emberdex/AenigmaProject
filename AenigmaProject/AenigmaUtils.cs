using System;
using System.Threading;

namespace AenigmaProject
{
    /// <summary>
    /// Pretty error response codes.
    /// </summary>
    public enum AenigmaErrorResponse
    {
        ABORT,
        RETRY,
        FAIL
    }
    
    /// <summary>
    /// Class containing various helper functions for the game.
    /// </summary>
    public class AenigmaUtils
    {
        /// <summary>
        /// Prints a string with a delay.
        /// Has a random delay by default.
        /// </summary>
        /// <param name="str">The string to print.</param>
        public static void SlowPrint(string str)
        {
            foreach(char c in str)
            {
                Console.Write(c);
                Thread.Sleep(new Random().Next(2, 10));
            }
        }

        public static string ReadLine()
        {
            string str = Console.ReadLine();
            AenigmaMenuUtils.TimeSinceInputAttempts = 0;
            
            return str;
        }

        /// <summary>
        /// Prints a string with a delay.
        /// </summary>
        /// <param name="str">The string to print.</param>
        /// <param name="timeout">The timeout, in milliseconds.</param>
        public static void SlowPrint(string str, int timeout)
        {
            foreach (char c in str)
            {
                Console.Write(c);
                Thread.Sleep(timeout);
            }
        }

        /// <summary>
        /// Displays an MS-DOS style Abort/Retry/Fail prompt.
        /// </summary>
        /// <returns>The response to the prompt, in the form of an AenigmaErrorResponse.</returns>
        public static AenigmaErrorResponse AbortRetryFail()
        {
            Console.WriteLine("An error occurred: (A)bort/(R)etry/[F]ail?: ");
            char response = Console.ReadKey().KeyChar;

            switch (char.ToLower(response))
            {
                case 'a':
                    return AenigmaErrorResponse.ABORT;
                case 'r':
                    return AenigmaErrorResponse.RETRY;
                default:
                    return AenigmaErrorResponse.FAIL;
            }
        }

        /// <summary>
        /// Displays an MS-DOS style Abort/Retry/Fail prompt.
        /// </summary>
        /// <param name="error">The error to display along with the prompt.</param>
        /// <returns>The response to the prompt, in the form of an AenigmaErrorResponse.</returns>
        public static AenigmaErrorResponse AbortRetryFail(string error)
        {
            Console.WriteLine($"{error}: (A)bort/(R)etry/[F]ail?: ");
            char response = Console.ReadKey().KeyChar;

            switch (char.ToLower(response))
            {
                case 'a':
                    return AenigmaErrorResponse.ABORT;
                case 'r':
                    return AenigmaErrorResponse.RETRY;
                default:
                    return AenigmaErrorResponse.FAIL;
            }
        }
    }
}