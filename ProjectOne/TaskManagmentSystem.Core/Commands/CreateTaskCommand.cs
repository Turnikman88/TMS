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
            string taskTitle = CommandParameters[1];
            string taskDescription = CommandParameters[2];
            var namespaceName = "TaskManagmentSystem.Models";
            string fullType = $"{namespaceName}.{taskType}";
            Type type;
            /*            switch (taskType.ToLower())
                        {
                            case "bug":                    
                                type = typeof(Bug);
                                break;
                            case "story":
                                type = typeof(Story);
                                break;
                            case "feedback":
                                type = typeof(Feedback);
                                break;
                            default:                    
                                throw new UserInputException(string.Format(Constants.TASK_TYPE_ERR, taskType));                   
                        }*/
            type = Type.GetType("TaskManagmentSystem.Models.Bug", true, true);
            
            IBoardItem task = this.Repository.CreateTask(type, taskTitle, taskDescription);
            //ToDo: Add this task to Board;
            return $"{task.GetType().Name} was created";
        }
    }
}
