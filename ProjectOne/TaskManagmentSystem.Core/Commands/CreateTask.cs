using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class CreateTask : BaseCommand
    {
        private const int numberOfParameters = 3;
        public CreateTask(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (CommandParameters.Count < numberOfParameters)
            {
                Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            }

            string taskType = CommandParameters[0];

            string taskTitle = CommandParameters[1];

            string taskDescription = CommandParameters[2];

            string[] parameters = CommandParameters.Skip(3).ToArray();

            var type = this.Repository.ModelsClassTypes.FirstOrDefault(x => x.Name.ToLower() == taskType) ?? throw new UserInputException(string.Format(Constants.TASK_TYPE_ERR, taskType));
            var task = this.Repository.CreateTask(type, taskTitle, taskDescription, parameters);
            return $"{task.GetType().Name} {taskTitle}, ID:  {task.Id} was created";
        }
    }
}
