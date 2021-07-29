
using System;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Contracts;

namespace TaskManagmentSystem.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepository reository = new Repository();
            ICommandFactory commandManager = new CommandFactory(reository);
            IEngine engine = new Engine(commandManager);
            engine.Start();
        }
    }
}
