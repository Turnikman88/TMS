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

            var sb = new StringBuilder();
            sb.AppendLine($"Users count: {this.Repository.Users.Count}");
            foreach (var user in this.Repository.Users)
            {
                sb.AppendLine(user.ToString());
                sb.AppendLine(Constants.PRINT_INFO_SEPARATOR);
            }
            return sb.ToString().Trim();
        }
    }
}