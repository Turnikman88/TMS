using System;
using System.Collections.Generic;
using System.Reflection;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class CreateTaskCommand : BaseCommand
    {
        private const int numberOfParameters = 3;
        public CreateTaskCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string taskType = CommandParameters[0];
            string taskTypeForReflection = $".{taskType}";

            string taskTitle = CommandParameters[1];
            string taskDescription = CommandParameters[2];
            Type type = Reflection.GetTypeOfTask(taskTypeForReflection);
            if (type is null)
            {
                throw new UserInputException(string.Format(Constants.TASK_TYPE_ERR, taskType));
            }
            
            IBoardItem task = this.Repository.CreateTask(type, taskTitle, taskDescription);
            //ToDo: Add this task to Board;
            return $"{task.GetType().Name} was created";
        }
    }
}
