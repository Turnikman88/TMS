using ProjectOne.Commands.Contracts;
using ProjectOne.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagmentSystem.Models.Common;

namespace ProjectOne.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandFactory commandFactory;

        

        public Engine(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
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
                    Console.WriteLine(ex);
                }
            }
        }

        private void ProcessCommand(string commandLine)
        {
            Validator.ValidateObjectIsNotNULL(commandLine.Trim(), Constants.EmptyCommandError);

            CheckPremissionToExecute(commandLine);

            ICommand command = this.commandFactory.Create(commandLine);
            string result = command.Execute();
            Console.WriteLine(result.Trim());
        }

        private void CheckPremissionToExecute(string commandLine)
        {
            //ToDo: Checks if user is the member of the team, before processing command
        }
    }
}
