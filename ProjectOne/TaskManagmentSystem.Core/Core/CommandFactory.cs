using ProjectOne.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Commands;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IRepository repository;
        public CommandFactory(IRepository repository)
        {
            this.repository = repository;
        }
        public ICommand Create(string commandLine)
        {
            string[] arguments = commandLine.Split(); //ToDo: check why cant remove emptyentries
            string commandName = ExtractName(arguments);
            CheckPremissionToExecute(commandName);
            List<string> commandParameters = ExtractParameters(arguments);
            ICommand command = null;
            var type = this.repository.CoreClassTypes.First(x => x.Name.ToLower() == commandName)
                ?? throw new UserInputException(string.Format(Constants.INVALID_COMMAND_ERR, commandName));

            command = Activator.CreateInstance(type, commandParameters, repository) as ICommand;

            return command; 
        }

        private List<string> ExtractParameters(string[] arguments)
        {            
            var list = arguments.Skip(1).ToList();
            return list;
        }

        private string ExtractName(string[] arguments)
        {
            string nameOfCommand = arguments[0];
            return nameOfCommand;
        }
        private void CheckPremissionToExecute(string commandName) 
        {
            
            if (this.repository.LoggedUser == null && commandName.ToLower() != "createuser" && commandName.ToLower() != "login")
            {
                throw new UserInputException(Constants.USER_NOT_LOGGED_IN); //ToDo: fix error message when type login 
            }
            
        }
    }
}
