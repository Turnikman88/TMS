using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowAllTasks : BaseCommand
    {
        private const int numberOfParameters = 3;
        //showalltasks [teamname/id] (keyword)[filter/sort] [titleName]
        public ShowAllTasks(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string teamIdentifier = CommandParameters[0];
            string keyword = CommandParameters[1].ToLower();
            string title = CommandParameters[2];

            IEnumerable<IBoardItem> result = null;

            var team = this.Repository.GetTeam(teamIdentifier);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }


            if (keyword == "filter")
            {
                result = this.Repository.GetTasks().Where(x => x.Title.Contains(title));
            }
            else if (keyword == "sortby")
            {
                result = this.Repository.GetTasks().OrderBy(x => x.Title);
            }
            else
            {
                throw new UserInputException("showalltasks command needs four keywords [teamname/id] ([filter] [title] or [sortby] title)");
            }

            return string.Join(Environment.NewLine, result);

        }
    }
}
