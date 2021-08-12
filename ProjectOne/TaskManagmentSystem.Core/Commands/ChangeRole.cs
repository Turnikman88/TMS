using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ChangeRole : BaseCommand
    {
        private const int numberOfParameters = 1;
        public ChangeRole(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            CheckIsRoot();
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string userIdentificator = CommandParameters[0];

            var user = this.Repository.GetUser(userIdentificator);

            if (this.Repository.LoggedUser == user)
            {
                throw new UserInputException(Constants.CANNOT_CHANGE_OWN_ROLE_ERR);
            }

            user.ChangeRole();

            return $"User {user.Name} with ID: {user.Id} changed his role to {user.Role}";
        }
    }
}
