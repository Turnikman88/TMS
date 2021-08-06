using System.Collections.Generic;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowAllTeamMembers : BaseCommand
    {
        private const int numberOfParameters = 1;
        //showallteammembers [teamname]
        public ShowAllTeamMembers(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            string teamName = CommandParameters[0];

            if (!this.Repository.IsTeamMember(this.Repository.GetTeam(teamName), this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            var team = this.Repository.GetTeam(teamName);

            var sb = new StringBuilder();

            foreach (var member in team.Members)
            {
                sb.AppendLine(member.ToString());
                sb.AppendLine(Constants.PRINT_INFO_SEPARATOR);
            }
            return sb.ToString().Trim();
        }
    }
}
