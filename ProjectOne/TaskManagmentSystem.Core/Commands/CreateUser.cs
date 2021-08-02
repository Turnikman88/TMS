using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    internal class CreateUser : BaseCommand
    {
        private const int numberOfParameters = 1;


        public CreateUser(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            string username = CommandParameters[0];
            if (this.Repository.FindUserByName(username) != null)
            {
                throw new UserInputException(string.Format(Constants.USER_ALREADY_EXIST, username));
            }
            var user = this.Repository.CreateUser(username);

            return $"User with username {user.Name} was created";
        }
    }
}