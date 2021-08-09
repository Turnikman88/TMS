using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class RemoveTask : BaseCommand
    {
        private const int numberOfParameters = 2;
        public RemoveTask(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string boardIdentifier = CommandParameters[0];
            int taskId = ParseIntParameter(CommandParameters[1]);

            var board = this.Repository.GetBoard(boardIdentifier);
            var task = this.Repository.GetTask(taskId);
            var user = this.Repository.LoggedUser;
            board.RemoveTask(task);
            if (user.Tasks.Any(x => x.Id == task.Id)) 
            {
                user.RemoveTask(task);
            }
            this.Repository.RemoveTask(task);
            return $"{task.GetType().Name} {task.Title}, ID: {task.Id} was removed!";
        }
    }
}
