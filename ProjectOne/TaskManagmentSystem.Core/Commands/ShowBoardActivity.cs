using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowBoardActivity : BaseCommand
    {
        private const int numberOfParameters = 2;
        //showboardactivity [teamname] [boardID]
        public ShowBoardActivity(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string teamName = CommandParameters[0];
            int boardName = int.Parse(CommandParameters[1]);

            var team = this.Repository.GetTeam(teamName);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            return team.Boards.FirstOrDefault(x => x.Id == boardName).ViewHistory();
        }
    }
}
