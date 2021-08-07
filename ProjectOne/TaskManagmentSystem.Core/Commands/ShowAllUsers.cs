using System.Collections.Generic;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowAllUsers : BaseCommand
    {
        public ShowAllUsers(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)

        {

        }
        public override string Execute()
        {
            CheckIsRoot();
            if (this.Repository.Users.Count > 0)
            {
                var sb = new StringBuilder();

                foreach (var user in this.Repository.Users) 
                {
                    sb.AppendLine(user.ToString());
                    sb.AppendLine(Constants.PRINT_INFO_SEPARATOR);
                }
                return sb.ToString().Trim();
            }
            return "There are no registered users.";
        }
    }
}