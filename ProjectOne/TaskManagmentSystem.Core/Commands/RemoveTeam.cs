using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Enums;

namespace TaskManagmentSystem.Core.Commands
{
    public class RemoveTeam : BaseCommand
    {
        private const int numberOfParameters = 1;

        public RemoveTeam(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            string teamIdentifier = this.CommandParameters[0];
            var team = this.Repository.GetTeam(teamIdentifier);
            var user = this.Repository.LoggedUser;

            if (user.Role != Role.Root || !team.Administrators.Any(x => x.Id == user.Id))
            {
                throw new UserInputException(Constants.YOU_ARE_NOT_ALLOWED_TO_REMOVE);
            }

            this.Repository.RemoveTeam(team);
            return $"Team with name {team.Name}, ID: {team.Id} was removed";
        }
    }
}
