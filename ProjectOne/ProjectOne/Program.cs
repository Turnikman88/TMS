using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Core.providers;
using TaskManagmentSystem.Core.providers.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;


namespace TaskManagmentSystem.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.WriteLine(Model.GenerateLogo()); // we need that because assemblies get optimized if there is not declared type of that assembly

            IRepository reository = new Repository(GetCoreClassTypes(), GetModelsClassTypes());
            ICommandFactory commandManager = new CommandFactory(reository);
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(commandManager, writer);
            engine.Start();
        }

        private static List<Type> GetCoreClassTypes()
        {
            return Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(x => Assembly.Load(x))
                .SelectMany(x => x.GetTypes())
                .Where(x => x.FullName.Contains(Constants.CORE_ASSEMBLY_KEY) && x.BaseType == typeof(BaseCommand)).ToList();
        }

        private static List<Type> GetModelsClassTypes()
        {
            return Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(x => Assembly.Load(x))
                .SelectMany(x => x.GetTypes())
                .Where(x => x.FullName.Contains(Constants.MODELS_ASSEMBLY_KEY) && x.BaseType == typeof(BoardItem)).ToList();
        }
    }
}
