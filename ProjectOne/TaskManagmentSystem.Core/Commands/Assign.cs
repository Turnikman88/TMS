using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
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
            int taskId = ParseIntParameter(CommandParameters[2]);

            var team = this.Repository.GetTeam(teamNameOrID);
            var user = this.Repository.GetUser(userNameOrID);

            if (!this.Repository.IsTeamMember(team, this.Repository.LoggedUser))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, this.Repository.LoggedUser.Name));
            }
            if (!team.Members.Contains(user))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, user.Name));
            }

            var task = this.Repository.FindTaskByID(taskId);
            var type = task.GetType();
            var method = type.GetMethod("AddAssignee") ?? throw new UserInputException(Constants.FEEDBACKS_CANNOT_BE_ASSIGNED_ERR);
            method.Invoke(task, new object[] { user });

            user.AddTask(task);

            return $"User {user.Name} was assigned to {task.GetType().Name} with ID: {taskId}";
        }
    }
}
