using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class RemoveUser : BaseCommand
    {
        private int numberOfParameters = 1;
        public RemoveUser(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            CheckIsRoot();
            string userIdentificator = CommandParameters[0];
            var user = this.Repository.GetUser(userIdentificator);
            this.Repository.RemoveUser(user);
            return $"User with username {user.Name}, ID: {user.Id} was removed";
        }
    }
}
