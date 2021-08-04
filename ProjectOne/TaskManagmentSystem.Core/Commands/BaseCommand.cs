using ProjectOne.Commands.Contracts;
using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;
using TaskManagmentSystem.Models.Enums;

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

        protected void CheckIsRoot()
        {
            if (this.Repository.LoggedUser.Role != Role.Root)
            {
                throw new UserInputException(Constants.USER_NOT_ROOT);
            }
        }

        //maybe some Enum parser
    }
}
