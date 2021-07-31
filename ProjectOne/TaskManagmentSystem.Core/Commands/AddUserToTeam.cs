using System;
using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class AddUserToTeam : BaseCommand
    {
        private int numberOfParameters = 2;
        public AddUserToTeam(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, this.CommandParameters.Count);

            string userIndicator = CommandParameters[0];
            string teamIndicator = CommandParameters[1];

            IMember user = GetUser(userIndicator);
            ITeam team = GetTeam(teamIndicator);

            if (IsTeamMember(team, user))
            {
                throw new UserInputException(string.Format(Constants.USER_ALREADY_EXIST, user.Name));
            }
            team.Members.Add(user);
            return $"User with name {user.Name} was successfully added to team {team.Name}";
        }
    }
}
