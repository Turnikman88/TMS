using ProjectOne.Commands.Contracts;
using ProjectOne.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectOne.Core
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
            string name = ExtractName(arguments);
            List<string> commandParams = ExtractParameters(arguments);
            switch (name)
            {
                case "createperson":
                    break;
            }
            return null; //remove later
        }

        private List<string> ExtractParameters(string[] arguments)
        {
            List<string> commandParams = new List<string>();
            var list = commandParams.Skip(1).ToList();
            return list;
        }

        private string ExtractName(string[] arguments)
        {
            string nameOfCommand = arguments[0];
            return nameOfCommand;
        }
    }
}
