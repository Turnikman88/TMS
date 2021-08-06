using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowTeamActivity : BaseCommand
    {
        private const int numberOfParameters = 1;
        //showteamactivity [teamname]
        public ShowTeamActivity(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string teamNameOrID = CommandParameters[0];

            var team = this.Repository.GetTeam(teamNameOrID);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            return team.ViewHistory();
        }
    }
}
