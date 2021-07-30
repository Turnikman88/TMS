using ProjectOne.Commands.Contracts;
using System;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Core.providers;
using TaskManagmentSystem.Core.providers.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandFactory commandFactory;
        private readonly IWriter writer;
        

        public Engine(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
            this.writer = new ConsoleWriter();
        }
        public void Start()
        {
            while (true)
            {
                try
                {
                    string inputLine = Console.ReadLine().Trim();

                    if (inputLine.ToLower() == Constants.TerminationCommand)
                    {
                        break;
                    }

                    ProcessCommand(inputLine);
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }

        private void ProcessCommand(string commandLine)
        {
            Validator.ValidateObjectIsNotNULL(commandLine.Trim(), Constants.EmptyCommandError);

            ICommand command = this.commandFactory.Create(commandLine);
            string result = command.Execute();
            writer.WriteLine(result.Trim());
        }


    }
}
