using System;
using System.Collections.Generic;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowAllTeamsCommand : BaseCommand
    {
        public ShowAllTeamsCommand(IRepository repository)
            :base(new List<string>(), repository)
        {

        }
        public override string Execute()
        {
            if (this.Repository.Teams.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var team in this.Repository.Teams)
                {
                    sb.AppendLine(team.ToString());
                    sb.AppendLine(Constants.PRINT_INFO_SEPARATOR);
                }
                return sb.ToString().Trim();
            }
            else
            {
                return "There are no registered teams."; //maybe constant ?
            }

        }
    }
}
