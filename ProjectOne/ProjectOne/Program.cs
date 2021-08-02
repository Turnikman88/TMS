using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;


namespace TaskManagmentSystem.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new Model(); // we need that because assemblies get optimized if there is not declared type of that assembly
                      

            IRepository reository = new Repository(GetCoreClassTypes(), GetModelsClassTypes());
            ICommandFactory commandManager = new CommandFactory(reository);
            IEngine engine = new Engine(commandManager);
            engine.Start();
        }

        private static List<Type> GetCoreClassTypes()
        {
            return Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(x => Assembly.Load(x))
                .SelectMany(x => x.GetTypes())
                .Where(x => x.FullName.Contains(Constants.CORE_ASSEMBLY_KEY) && x.IsClass).ToList();
        }

        private static List<Type> GetModelsClassTypes()
        {
            return Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(x => Assembly.Load(x))
                .SelectMany(x => x.GetTypes())
                .Where(x => x.FullName.Contains(Constants.MODELS_ASSEMBLY_KEY) && x.IsClass).ToList();
        }
    }
}
