using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class Change : BaseCommand
    {
        private readonly int numberOfParameters;
        //advance teamname 42 priority
        public Change(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
            numberOfParameters = commandParameters.Count == 4 ? 4 : 3;
        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string teamName = CommandParameters[0];
            int itemID = ParseIntParameter(CommandParameters[1]);
            string statusType = CommandParameters[2].ToLower();
            string[] parameters = statusType == "rating" ? CommandParameters.Skip(3).ToArray() : null;

            if (!this.Repository.IsTeamMember(this.Repository.GetTeam(teamName), this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            var task = this.Repository.FindTaskByID(itemID);
            var type = task.GetType();
            var methodName = this.GetType().Name + char.ToUpper(statusType[0]) + statusType.Substring(1);
            var method = type.GetMethod(methodName)
                ?? throw new UserInputException(string.Format(Constants.GIVEN_STATUS_TYPE_ERR, statusType));

            method.Invoke(task, parameters);

            return $"{statusType} of item {task.GetType().Name} ID: {itemID} was changed!";
        }
    }
}
