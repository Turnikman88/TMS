using TaskManagmentSystem.Core;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Core.providers;
using TaskManagmentSystem.Core.providers.Contracts;


namespace TaskManagmentSystem.CLI
{
    public class Program
    {
        static void Main(string[] args)
        {            
            IRepository reository = new Repository();
            ICommandFactory commandManager = new CommandFactory(reository);
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(commandManager, writer);
            engine.Start();
        }
    }
}
