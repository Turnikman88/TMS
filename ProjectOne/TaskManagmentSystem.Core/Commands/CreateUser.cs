using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    internal class CreateUser : BaseCommand
    {
        private const int numberOfParameters = 2;


        public CreateUser(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            string username = CommandParameters[0];
            string password = CommandParameters[1];
            if (this.Repository.FindUserByName(username) != null)
            {
                throw new UserInputException(string.Format(Constants.USER_ALREADY_EXIST, username));
            }
            var user = this.Repository.CreateUser(username, password);

            return $"User with username {user.Name}, ID {user.Id} was created";
        }
    }
}