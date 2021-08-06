using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class Assign : BaseCommand
    {
        private const int numberOfParameters = 3;
        //assign [team] [user] [taskTobeAssigned]
        public Assign(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            string teamNameOrID = CommandParameters[0];
            string userNameOrID = CommandParameters[1];
            int id = int.Parse(CommandParameters[2]);

            var team = this.Repository.GetTeam(teamNameOrID);
            var user = this.Repository.GetUser(userNameOrID);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser) || !team.Members.Contains(user))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }

            var task = this.Repository.FindTaskByID(id);
            if (task is Bug)
            {
                var bug = (Bug)task;
                bug.AddAssignee(user);
            }
            else
            {
                var story = (Story)task;
                story.AddAssignee(user);
            }

            return $"User {userNameOrID} was assigned to {id}";
        }
    }
}
