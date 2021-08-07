using System;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Core.providers;
using TaskManagmentSystem.Core.providers.Contracts;
using TaskManagmentSystem.Models;


namespace TaskManagmentSystem.CLI
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.WriteLine(Model.GenerateLogo()); // we need that because assemblies get optimized if there is not declared type of that assembly

            IRepository reository = new Repository();
            ICommandFactory commandManager = new CommandFactory(reository);
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(commandManager, writer);
            engine.Start();
        }

    }
}
