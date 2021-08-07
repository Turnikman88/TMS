using ProjectOne.Commands.Contracts;
using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Enums;

namespace TaskManagmentSystem.Core.Commands
{
    public abstract class BaseCommand : ICommand
    {        
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
        protected int ParseIntParameter(string value) // ToDo: we have the same method in models validator
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            throw new UserInputException(string.Format(Constants.PARSE_INT_ERR, value));
        }
    }
}
