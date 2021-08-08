using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class CreateTask : BaseCommand
    {
        private const int numberOfParameters = 4;
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

            string boardIdentifier = CommandParameters[1];

            string taskTitle = CommandParameters[2];

            string taskDescription = CommandParameters[3];

            var board = this.Repository.GetBoard(boardIdentifier);


            string[] parameters = CommandParameters.Skip(4).ToArray();

            var type = this.Repository.ModelsClassTypes.FirstOrDefault(x => x.Name.ToLower() == taskType) ?? throw new UserInputException(string.Format(Constants.TASK_TYPE_ERR, taskType));
            var task = this.Repository.CreateTask(type, taskTitle, taskDescription, board, parameters);
            return $"{task.GetType().Name} {taskTitle}, ID: {task.Id} was created";
        }
    }
}
