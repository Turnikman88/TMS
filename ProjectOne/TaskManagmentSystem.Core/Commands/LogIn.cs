using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class LogIn : BaseCommand
    {
        private const int numberOfParameters = 2;

        public LogIn(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute() 
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            string currentUsername = CommandParameters[0];
            string password = CommandParameters[1];
            if (this.Repository.LoggedUser != null)
            {
                if (this.Repository.LoggedUser.Name.ToLower() == currentUsername)
                {
                    throw new UserInputException(Constants.THIS_USER_LOGGED_IN);
                }
                throw new UserInputException(string.Format(Constants.USER_LOGGED_IN_ALREADY, this.Repository.LoggedUser.Name));
            }

            var user = this.Repository.FindUserByName(currentUsername) ?? throw new UserInputException(Constants.WRONG_USERNAME);
            if (password != user.Password)
            {
                throw new UserInputException(Constants.WRONG_PASSWORD);
            }
            this.Repository.LoggedUser = user;
            return string.Format(Constants.USER_LOGGED_IN, this.Repository.LoggedUser.Name);
        }
    }
}
