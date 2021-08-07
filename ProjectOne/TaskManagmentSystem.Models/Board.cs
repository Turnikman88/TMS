using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Models
{
    public class Board : IBoard, IActivityLog
    {
        private string name;
        private readonly IList<IBoardItem> tasks = new List<IBoardItem>();
        private readonly IList<IEventLog> eventLogs = new List<IEventLog>();

        public Board(int id, string name)
        {
            this.Name = name;
            this.Id = id;
            AddEvent(new EventLog(string.Format(Constants.EVENT_WAS_CREATED, "Board", id)));
        }

        public string Name
        {
            get => this.name;
            private set
            {
                Validator.ValidateObjectIsNotNULL(value, string.Format(Constants.ITEM_NULL_ERR, nameof(Board)));
                Validator.ValidateRange(value.Length, Constants.BOARD_NAME_MIN_SYMBOLS, Constants.BOARD_NAME_MAX_SYMBOLS, string.Format(Constants.STRING_LENGHT_ERR, nameof(Board), Constants.BOARD_NAME_MIN_SYMBOLS, Constants.BOARD_NAME_MAX_SYMBOLS));
                this.name = value;
            }
        }

        public IList<IBoardItem> Tasks
            => new List<IBoardItem>(this.tasks);

        public IList<IEventLog> EventLogs
            => new List<IEventLog>(this.eventLogs);

        public int Id { get; }

        public void AddTask(IBoardItem task)
        {
            this.tasks.Add(task);
            AddEvent(new EventLog($"{task.GetType().Name} '{task.Title}' with ID {task.Id} was pinned to board '{this.Name}'"));
        }
        public void RemoveTask(IBoardItem task)
        {
            if (task is null)
            {
                throw new UserInputException(string.Format(Constants.ITEM_NULL_ERR, "Task"));
            }
            this.tasks.Remove(task);
            AddEvent(new EventLog($"{task.GetType().Name} '{task.Title}' with ID {task.Id} was unpinned from board '{this.Name}'"));
        }
        protected void AddEvent(IEventLog eventLog)
        {
            this.eventLogs.Add(eventLog);
        }
        public string ViewHistory()
        {
            var sb = new StringBuilder();
            sb.Append(string.Join($"{Environment.NewLine}", eventLogs.Select(x => x.ViewInfo())));
            sb.Append(string.Join($"{Environment.NewLine}", tasks.Select(x => x.ViewHistory())));
            return sb.ToString().Trim();
        }
        public override string ToString()
        {
            var tasks = this.Tasks.Count == 0 ? "No tasks" :
                string.Join(Environment.NewLine, this.Tasks.OrderBy(x => x.Id).Select(x => x.ToString()));
            return $"Name: {this.Name}, ID: {this.Id}, Number of tasks: {tasks}";
        }
    }
}
