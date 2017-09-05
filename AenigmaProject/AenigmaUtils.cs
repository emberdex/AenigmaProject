using System;
using System.Threading;

namespace AenigmaProject
{
    public enum AenigmaErrorResponse
    {
        ABORT,
        RETRY,
        FAIL
    }
    
    public class AenigmaUtils
    {
        
        public static void SlowPrint(string str)
        {
            foreach(char c in str)
            {
                Console.Write(c);
                Thread.Sleep(new Random().Next(2, 10));
            }
        }

        public static void SlowPrint(string str, int timeout)
        {
            foreach (char c in str)
            {
                Console.Write(c);
                Thread.Sleep(timeout);
            }
        }

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