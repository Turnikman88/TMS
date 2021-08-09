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
        //showtaskbytype bug filter status active/fixed
        //showtaskbytype bug filter asignee NAME
        //showtaskbytype bug sort status active
        //List bugs/stories/feedback only.
        //Filter by status and/or assignee  
        //Sort by title/priority/severity/size/rating (depending on the task type)


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


            if (keyword == "filter")
            {
                filteredList = FilterBy(typeOfTask, parameter1, parameter2, filteredList);
            }
            else if (keyword == "sort")
            {

            }
            else
            {
                throw new UserInputException("You can only filter or sort tasks, please use keyword 'filter' or 'sort'");
            }

            return $"";

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

        private IEnumerable<IBoardItem> FilterBy(string typeOfTask, string parameter, string parameter2, IEnumerable<IBoardItem> list)
        {
            switch (typeOfTask, parameter, parameter2)
            {
                case ("bug", "status", "active"):
                    return list.Select(x => x as Bug).Where(x => x.Status == Models.Enums.Bug.Status.Active);
                case ("bug", "status", "fixed"):
                    return list.Select(x => x as Bug).Where(x => x.Status == Models.Enums.Bug.Status.Fixed);
                case ("bug", "asignee", "ascending"):
                    return list.Select(x => x as Bug).OrderBy(x => x.Assignee.Name);
                case ("bug", "asignee", "descending"):
                    return list.Select(x => x as Bug).OrderByDescending(x => x.Assignee.Name);
                case ("story", "status", "notdone"):
                    return list.Select(x => x as Story).Where(x => x.Status == Models.Enums.Story.Status.NotDone);
                case ("story", "status", "done"):
                    return list.Select(x => x as Story).Where(x => x.Status == Models.Enums.Story.Status.Done);
                case ("story", "status", "inprogress"):
                    return list.Select(x => x as Story).Where(x => x.Status == Models.Enums.Story.Status.InProgress);
                case ("story", "asignee", "ascending"):
                    return list.Select(x => x as Story).OrderBy(x => x.Assignee.Name);
                case ("story", "asignee", "descending"):
                    return list.Select(x => x as Story).OrderByDescending(x => x.Assignee.Name);
                case ("feedback", "status", "new"):
                    return list.Select(x => x as Feedback).Where(x => x.Status == Models.Enums.Feedback.Status.New);
                case ("feedback", "status", "done"):
                    return list.Select(x => x as Feedback).Where(x => x.Status == Models.Enums.Feedback.Status.Done);
                case ("feedback", "status", "unschedule"):
                    return list.Select(x => x as Feedback).Where(x => x.Status == Models.Enums.Feedback.Status.Unscheduled);
                case ("feedback", "status", "schedule"):
                    return list.Select(x => x as Feedback).Where(x => x.Status == Models.Enums.Feedback.Status.Scheduled);
                default:
                    throw new UserInputException("");
            }
        }

    }
}
