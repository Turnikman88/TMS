using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        //showtaskbytype bug sort title
        //List bugs/stories/feedback only.
        //Filter by status and/or assignee  
        //Sort by title/priority/severity/size/rating (depending on the task type)

        public ShowTaskByType(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (CommandParameters.Count < 3 || CommandParameters.Count > 4)
            {
                Validator.ValidateParametersCount(numberOfParameters, CommandParameters.Count);
            }

            string typeOfTask = CommandParameters[0].ToLower();
            string action = CommandParameters[1].ToLower();
            string parameter = CommandParameters[2].ToLower();

            var listTask = this.Repository.GetTasks();
            var filteredList = GetOnlyOneTypeOfTasks(listTask, typeOfTask);

            if (action == "filter")
            {
                Validator.ValidateParametersCount(numberOfParameters + 1, CommandParameters.Count);

                string parameter2 = CommandParameters[3];
                filteredList = FilterBy(typeOfTask, parameter, parameter2, filteredList);
            }
            else if (action == "sort")
            {
                filteredList = SortBy(typeOfTask, parameter, filteredList);
            }
            else
            {
                throw new UserInputException("You can only filter or sort tasks, please use keyword 'filter' or 'sort'");
            }

            StringBuilder sb = new StringBuilder();

            foreach (var item in filteredList)
            {
                sb.Append(item.ToString());
            }
            return sb.ToString().Trim();
        }

        private IEnumerable<IBoardItem> GetOnlyOneTypeOfTasks(IList<IBoardItem> list, string taskType)
        {
            switch (taskType)
            {
                case "bug":
                    return list.Where(x => x.GetType() == typeof(Bug)).ToList();
                case "story":
                    return list.Where(x => x.GetType() == typeof(Story)).ToList();
                case "feedback":
                    return list.Where(x => x.GetType() == typeof(Feedback)).ToList();
                default:
                    throw new UserInputException("No Search results match the specified criteria.");
            }
        }

        private IEnumerable<IBoardItem> FilterBy(string typeOfTask, string parameter, string parameter2, IEnumerable<IBoardItem> list)
        {
            if (parameter == "assignee" && (typeOfTask == "bug" || typeOfTask == "story"))
            {
                switch (typeOfTask)
                {
                    case "bug":
                        return list.Select(x => x as Bug).Where(x => x.Assignee?.Name == parameter2);
                    case "story":
                        return list.Select(x => x as Story).Where(x => x.Assignee?.Name == parameter2);
                    default:
                        throw new UserInputException($"Name {parameter2} does not have any tasks assigneed");
                }
            }

            switch (typeOfTask, parameter, parameter2.ToLower())
            {
                case ("bug", "status", "active"):
                    return list.Select(x => x as Bug).Where(x => x.Status == Models.Enums.Bug.Status.Active);
                case ("bug", "status", "fixed"):
                    return list.Select(x => x as Bug).Where(x => x.Status == Models.Enums.Bug.Status.Fixed);
                case ("story", "status", "notdone"):
                    return list.Select(x => x as Story).Where(x => x.Status == Models.Enums.Story.Status.NotDone);
                case ("story", "status", "done"):
                    return list.Select(x => x as Story).Where(x => x.Status == Models.Enums.Story.Status.Done);
                case ("story", "status", "inprogress"):
                    return list.Select(x => x as Story).Where(x => x.Status == Models.Enums.Story.Status.InProgress);
                case ("feedback", "status", "new"):
                    return list.Select(x => x as Feedback).Where(x => x.Status == Models.Enums.Feedback.Status.New);
                case ("feedback", "status", "done"):
                    return list.Select(x => x as Feedback).Where(x => x.Status == Models.Enums.Feedback.Status.Done);
                case ("feedback", "status", "unschedule"):
                    return list.Select(x => x as Feedback).Where(x => x.Status == Models.Enums.Feedback.Status.Unscheduled);
                case ("feedback", "status", "schedule"):
                    return list.Select(x => x as Feedback).Where(x => x.Status == Models.Enums.Feedback.Status.Scheduled);
                default:
                    throw new UserInputException("You can only filter by status or asignee, take in mind Status does not have asignees");
            }
        }
        private IEnumerable<IBoardItem> SortBy(string typeOfTask, string parameter, IEnumerable<IBoardItem> list)
        {
            switch (typeOfTask, parameter)
            {
                case ("bug", "title"):
                case ("feedback", "title"):
                case ("story", "title"):
                    return list.OrderBy(x => x.Title);
                case ("bug", "priority"):
                    return list.Select(x => x as Bug).OrderBy(x => x.Priority);
                case ("bug", "severity"):
                    return list.Select(x => x as Bug).OrderBy(x => x.Severity);
                case ("feedback", "rating"):
                    return list.Select(x => x as Feedback).OrderBy(x => x.Rating);
                case ("story", "priority"):
                    return list.Select(x => x as Story).OrderBy(x => x.Priority);
                case ("story", "size"):
                    return list.Select(x => x as Story).OrderBy(x => x.Size);
                default:
                    throw new UserInputException("You can only sort by Title, Priority[for Bug or Story], Severity[Bug], Size[Story] or Rating[Feedback]. Take in mind that some of the items have only one of these criteria.");

            }
        }
    }
}
