using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class Advance : BaseCommand
    {
        private const int numberOfParameters = 3;
        //advance teamname 42 priority
        public Advance(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string teamName = CommandParameters[0];
            int itemID = int.Parse(CommandParameters[1]);
            string statusType = CommandParameters[2];


            if (!this.Repository.IsTeamMember(this.Repository.GetTeam(teamName), this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            var task = this.Repository.FindTaskByID(itemID);

            if (task is Bug)
            {
                var bug = (Bug)task;
                switch (statusType.ToLower())
                {
                    case "priority":
                        bug.ChangePriority();
                        break;
                    case "severity":
                        bug.ChangeSeverity();
                        break;
                    case "status":
                        bug.ChangeStatus();
                        break;
                }

            }
            else if (task is Story)
            {
                var story = (Story)task;
                switch (statusType.ToLower())
                {
                    case "priority":
                        story.ChangePriority();
                        break;
                    case "size":
                        story.ChangeSize();
                        break;
                    case "status":
                        story.ChangeStatus();
                        break;
                }
            }
            else
            {
                task.ChangeStatus();
            }

            return $"{statusType} of item {itemID} was changed";
        }
    }
}
