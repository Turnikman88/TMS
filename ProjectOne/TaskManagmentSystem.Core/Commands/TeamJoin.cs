using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class TeamJoin : BaseCommand
    {
        private int numberOfParameters = 2;
        public TeamJoin(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, this.CommandParameters.Count);

            string userIndicator = CommandParameters[0];
            string teamIndicator = CommandParameters[1];

            IMember user = this.Repository.GetUser(userIndicator);
            ITeam team = this.Repository.GetTeam(teamIndicator);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_ALLOWED_JOINING, user.Name));
            }

            if (this.Repository.IsTeamMember(team, user))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_ALREADY_IN_TEAM, user.Name));
            }

            team.AddMember(user);

            return $"User with name {user.Name} was successfully added to team {team.Name}";
        }
    }
}
