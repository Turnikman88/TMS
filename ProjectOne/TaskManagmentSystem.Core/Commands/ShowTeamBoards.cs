using System.Collections.Generic;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowTeamBoards : BaseCommand
    {
        private const int numberOfParameters = 1;
        //showteamboards [teamname]
        public ShowTeamBoards(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string teamName = CommandParameters[0];

            var team = this.Repository.GetTeam(teamName);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            StringBuilder sb = new StringBuilder();
            foreach (var board in team.Boards)
            {
                sb.AppendLine(board.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
