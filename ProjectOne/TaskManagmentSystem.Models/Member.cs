using System.Collections.Generic;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;
using System.Text;

namespace TaskManagmentSystem.Models
{
    public class Member : IMember, IActivityLog
    {
        private string name;
        private readonly IList<IEventLog> eventLogs = new List<IEventLog>();
        private readonly IList<IBoardItem> tasks = new List<IBoardItem>();

        public Member(string name, int id)
        {
            this.name = name;
            AddEvent(new EventLog($"Member {name} was created"));
            this.Id = id;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                Validator.ValidateObjectIsNotNULL(value, string.Format(Constants.ITEM_NULL_ERR, nameof(Member)));
                Validator.ValidateRange(value.Length, Constants.MEMBER_NAME_MIN_SYMBOLS, Constants.MEMBER_NAME_MAX_SYMBOLS, string.Format(Constants.STRING_LENGHT_ERR, nameof(Member), Constants.MEMBER_NAME_MIN_SYMBOLS, Constants.MEMBER_NAME_MAX_SYMBOLS));
                Validator.ValidateNameUniqueness(value);
                this.name = value;
            }
        }
        public int Id { get; }
        public IList<IEventLog> EventLogs
            => new List<IEventLog>(this.eventLogs);

        public IList<IBoardItem> Tasks
            => new List<IBoardItem>(this.tasks); //ToDo: just Itasks Tasks or List<IBoardItems>
        private void AddEvent(IEventLog eventLog)
        {
            this.eventLogs.Add(eventLog);
        }
        public void AddTask(IBoardItem task)
        {
            Validator.ValidateObjectIsNotNULL(task, string.Format(Constants.ITEM_NULL_ERR, "Task"));
            AddEvent(new EventLog($"New task  was created {task.Title}"));
        }
        public void ViewHistory()
        {
            foreach (var item in eventLogs)
            {
                item.ViewInfo();
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Member: {this.Name}, ID: {this.Id}");
            sb.AppendLine($"Team: {this}");
            if (tasks.Count > 0)
            {
                sb.AppendLine($"Tasks: {this.tasks.Count}");
                foreach (var item in tasks)
                {
                    sb.AppendLine(item.Description);
                }
            }
            else
            {
                sb.AppendLine($"No tasks assigned");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
