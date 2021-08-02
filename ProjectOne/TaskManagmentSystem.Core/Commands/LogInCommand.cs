using System;
using System.Collections.Generic;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class LogInCommand : BaseCommand
    {
        private const int numberOfParameters = 1;

        public LogInCommand(List<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute() //ToDO: some readability improvements needed
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            string currentUsername = CommandParameters[0];
            if (this.Repository.LoggedUser != null)
            {
                throw new UserInputException(string.Format(Constants.USER_LOGGED_IN_ALREADY, this.Repository.LoggedUser.Name));
            }
            
            var user = this.Repository.FindUserByName(currentUsername) ?? throw new UserInputException(Constants.WRONG_USERNAME);
            this.Repository.LoggedUser = user;
            return string.Format(Constants.USER_LOGGED_IN, this.Repository.LoggedUser.Name);
        }
    }
}
