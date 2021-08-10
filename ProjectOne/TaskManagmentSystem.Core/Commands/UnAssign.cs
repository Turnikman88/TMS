using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class UnAssign : BaseCommand
    {
        private const int numberOfParameters = 2;
        //assign [team] [taskTobeAssigned]
        public UnAssign(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            string teamNameOrID = CommandParameters[0];

            int taskId = ParseIntParameter(CommandParameters[1]);

            var team = this.Repository.GetTeam(teamNameOrID);
            var user = this.Repository.LoggedUser;
            var task = this.Repository.GetTaskById(taskId);

            if (!this.Repository.IsTeamMember(team, user))
            {
                throw new UserInputException(string.Format(Constants.MEMBER_NOT_IN_TEAM, user.Name));
            }
            if (!user.Tasks.Any(x => x.Id == taskId))
            {
                throw new UserInputException(string.Format(Constants.TASK_NOT_ASSIGNED, user.Name));
            }

            var type = task.GetType();
            var method = type.GetMethod("RemoveAssignee");
            method.Invoke(task, null);

            user.RemoveTask(task);

            return $"User {user.Name} was unassigned from {task.GetType().Name} with ID: {taskId}";

        }
    }
}
