using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowTaskByType : BaseCommand
    {
        private const int numberOfParameters = 3;
        //ShowTaskByType [typeoftask] (keyword)[1.filter / 2.sort][1.status / assignee][2.title / priority / severity / size / rating]
        //showtaskbytype bug filter status active


        public ShowTaskByType(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);

            string typeOfTask = CommandParameters[0].ToLower();
            string keyword = CommandParameters[1].ToLower();
            string parameter1 = CommandParameters[2].ToLower();
            string parameter2 = CommandParameters[3].ToLower();

            var listTask = this.Repository.GetTasks();
            var filteredList = GetOnlyOneTypeOfTasks(listTask, typeOfTask);

            switch (keyword)
            {
                case "filter":
                    {
                        if (typeOfTask == "bug")
                        {
                            var bugList = filteredList.Select(x => (Bug)x);
                            if (parameter1 == "statusactive")
                            {
                                bugList = bugList.Where(x => x.Status == Models.Enums.Bug.Status.Active);
                            }
                            else if (parameter1 == "statusfixed")
                            {
                                bugList = bugList.Where(x => x.Status == Models.Enums.Bug.Status.Fixed);
                            }
                            else
                            {
                                throw new UserInputException("Bug has only two status Statuses Active and Fixed");
                            }
                        }
                    }
                    break;
                case "sort":
                    if (parameter1 == "title")
                    {
                        filteredList = filteredList.OrderBy(x => x.Title);
                    }
                    else if (parameter1 == "status")
                    {


                    }
                    break;
                default:
                    throw new UserInputException("You can only filter or sort tasks, please use keyword 'filter' or 'sort'");
            }

            throw new NotImplementedException();
        }

        private IEnumerable<IBoardItem> GetOnlyOneTypeOfTasks(IList<IBoardItem> list, string taskType)
        {
            switch (taskType)
            {
                case "bug":
                    return list.Where(x => x.GetType() == typeof(Bug));
                case "story":
                    return list.Where(x => x.GetType() == typeof(Story));
                case "feedback":
                    return list.Where(x => x.GetType() == typeof(Feedback));
                default:
                    throw new UserInputException("No Search results match the specified criteria.");

            }
        }

    }
}
