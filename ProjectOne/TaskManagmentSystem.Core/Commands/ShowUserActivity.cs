using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowUserActivity : BaseCommand
    {
        private const int numberOfParameters = 2;
        //showuseractivity [teamname] [username/id] 
        public ShowUserActivity(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string teamName = CommandParameters[0];
            string userIdentificator = CommandParameters[1];

            var team = this.Repository.GetTeam(teamName);
            var user = this.Repository.GetUser(userIdentificator);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            return user.ViewHistory();
        }
    }
}
