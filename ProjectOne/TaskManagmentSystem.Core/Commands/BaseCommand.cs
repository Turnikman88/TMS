using ProjectOne.Commands.Contracts;
using ProjectOne.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagmentSystem.Core.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected BaseCommand(IRepository repository)
                    : this(new List<string>(), repository)
        {
        }

        protected BaseCommand(IList<string> commandParameters, IRepository repository)
        {
            this.CommandParameters = commandParameters;
            this.Repository = repository;
        }

        public IList<string> CommandParameters { get; }
        public IRepository Repository { get; }

        public abstract string Execute();

        //ToDo: implement comands to check here
    }
}
