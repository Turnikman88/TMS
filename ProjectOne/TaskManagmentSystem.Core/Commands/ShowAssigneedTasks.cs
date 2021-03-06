using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowAssigneedTasks : BaseCommand
    {
        private const int numberOfParameters = 2;
        //showassigneedtasks [teamid/name] [boardid/name]
        public ShowAssigneedTasks(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string teamIdentifier = CommandParameters[0];
            string boardIdentifier = CommandParameters[1];

            var team = this.Repository.GetTeam(teamIdentifier);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            var board = this.Repository.GetBoard(boardIdentifier);
            var resultBugs = board.Tasks.Where(x => x.GetType() == typeof(Bug)).Select(x => x as Bug);

            StringBuilder sb = new StringBuilder();
            foreach (var item in resultBugs)
            {
                if (item.Assignee != null)
                {
                    sb.Append(item.ToString());
                }
            }

            var resultStory = board.Tasks.Where(x => x.GetType() == typeof(Story)).Select(x => x as Story);

            foreach (var item in resultStory)
            {
                if (item.Assignee != null)
                {
                    sb.Append(item.ToString());
                }
            }

            return sb.ToString().Trim();
        }
    }
}
