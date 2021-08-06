using ProjectOne.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            string[] arguments = commandLine.Split(); //ToDo: check why cant remove empty entries
            string commandName = ExtractName(arguments);
            CheckPremissionToExecute(commandName);
            List<string> commandParameters = ExtractParameters(arguments);
            ICommand command = null;
            var type = this.repository.CoreClassTypes.FirstOrDefault(x => x.Name.ToLower() == commandName)
                ?? throw new UserInputException(string.Format(Constants.INVALID_COMMAND_ERR, commandName));

            command = Activator.CreateInstance(type, commandParameters, repository) as ICommand;

            return command;
        }

        private List<string> ExtractParameters(string[] arguments)
        {
            var list = arguments.Skip(1).ToList();

            if (string.Join(" ", list).Contains("\""))
            {
                var arg = string.Join(" ", list);
                var newParams = new List<string>();
                var word = new StringBuilder();
                bool quotesOpen = false;
                for (int i = 0; i < arg.Length; i++)
                {
                    var currSymbol = arg[i].ToString();
                    if (currSymbol == "\"")
                    {
                        quotesOpen = quotesOpen == true ? false : true;
                    }
                    else if (quotesOpen)
                    {
                        word.Append(currSymbol);
                    }
                    else
                    {
                        word.Append(arg[i]);
                    }
                    if (i == arg.Length - 1 || !quotesOpen && currSymbol == " ")
                    {
                        newParams.Add(word.ToString());
                        word.Clear();
                    }
                }
                return newParams.Select(x => x.Trim()).ToList();
            }
            return list;
        }

        private string ExtractName(string[] arguments)
        {
            string nameOfCommand = arguments[0];
            return nameOfCommand;
        }
        private void CheckPremissionToExecute(string commandName)
        {

            if (this.repository.LoggedUser == null && commandName.ToLower() != "createuser" && commandName.ToLower() != "login"
                && commandName.ToLower() != "help")
            {
                throw new UserInputException(Constants.USER_NOT_LOGGED_IN); //ToDo: fix error message when type login 
            }

        }
    }
}
