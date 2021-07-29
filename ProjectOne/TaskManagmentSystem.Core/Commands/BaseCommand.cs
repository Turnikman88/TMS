using ProjectOne.Commands.Contracts;
using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;

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
