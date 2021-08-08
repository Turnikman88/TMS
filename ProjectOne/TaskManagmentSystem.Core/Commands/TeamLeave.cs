using System;
using System.Collections.Generic;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class TeamLeave : BaseCommand
    {
        private int numberOfParameters = 1;
        public TeamLeave(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, this.CommandParameters.Count);

            string teamIndicator = CommandParameters[0];
            var user = this.Repository.LoggedUser;
            var team = this.Repository.GetTeam(teamIndicator);

            if (!this.Repository.IsTeamMember(team, user))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, user.Name));
            }

            this.Repository.LeaveTeam(team);
            return $"User with name {user.Name} successfully leaved team {team.Name}";
        }
    }
}
