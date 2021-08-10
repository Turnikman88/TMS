using System;
using TaskManagmentSystem.Core.providers.Contracts;

namespace TaskManagmentSystem.Core.providers
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string value)
        {
            Console.Write(value);
        }
        public void WriteLine(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value);
        }
    }
}
