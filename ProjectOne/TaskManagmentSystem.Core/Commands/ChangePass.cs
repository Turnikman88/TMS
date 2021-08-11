using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ChangePass : BaseCommand
    {
        private const int numberOfParameters = 2;

        public ChangePass(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string oldPass = CommandParameters[0];
            string newPass = CommandParameters[1];

            var user = this.Repository.LoggedUser;

            if (user.Password != oldPass)
            {
                throw new UserInputException(Constants.PASSWORD_CHANGE_ERR);
            }

            user.ChangePass(newPass);

            return Constants.PASSWORD_CHANGED_SUCC;
        }
    }
}
