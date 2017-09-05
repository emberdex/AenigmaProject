using System;
using System.Threading;

namespace AenigmaProject
{
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
    }
}