using System.Collections.Generic;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowAllTeams : BaseCommand
    {
        public ShowAllTeams(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            CheckIsRoot();

            if (this.Repository.Teams.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Teams count: {this.Repository.Teams.Count}");
                foreach (var team in this.Repository.Teams)
                {
                    sb.AppendLine(team.ToString());
                    sb.AppendLine(Constants.PRINT_INFO_SEPARATOR);
                }
                return sb.ToString().Trim();
            }
            else
            {
                return "There are no registered teams.";
            }

        }

    }
}
