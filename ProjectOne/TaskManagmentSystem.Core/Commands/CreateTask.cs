using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

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
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string taskType = CommandParameters[0];            

            string taskTitle = CommandParameters[1];
            string taskDescription = CommandParameters[2];

           
            var type = this.Repository.ModelsClassTypes.SingleOrDefault(x => x.Name.ToLower() == taskType) ?? throw new UserInputException(string.Format(Constants.TASK_TYPE_ERR, taskType));
            var task = this.Repository.CreateTask(type, taskTitle, taskDescription);
            return $"{task.GetType().Name} was created";            
        }
    }
}
