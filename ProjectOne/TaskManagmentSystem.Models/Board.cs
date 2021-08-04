using System.Collections.Generic;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Models
{
    public class Board : IBoard, IActivityLog
    {
        private string name;
        private readonly IList<IBoardItem> tasks = new List<IBoardItem>();
        private readonly IList<IEventLog> events = new List<IEventLog>();

        public Board(int id, string name)
        {
            this.Name = name;
            this.Id = id;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                Validator.ValidateObjectIsNotNULL(value, string.Format(Constants.ITEM_NULL_ERR, nameof(Board)));
                Validator.ValidateNameUniqueness(value);
                Validator.ValidateRange(value.Length, Constants.BOARD_NAME_MIN_SYMBOLS, Constants.BOARD_NAME_MAX_SYMBOLS, string.Format(Constants.STRING_LENGHT_ERR, nameof(Team), Constants.BOARD_NAME_MIN_SYMBOLS, Constants.BOARD_NAME_MAX_SYMBOLS));
                this.name = value;
            }
        }

        public IList<IBoardItem> Tasks
            => new List<IBoardItem>(this.tasks);

        public IList<IEventLog> EventLogs
            => new List<IEventLog>(this.events);

        public int Id { get; }

        //TODO - дали тези таскове ще имат пълна функционалност
        public void AddTask(IBoardItem task)
        {
            Validator.ValidateObjectIsNotNULL(task, string.Format(Constants.ITEM_NULL_ERR, "Task"));
            this.tasks.Add(task);
        }
        public void RemoveTask(IBoardItem task)
        {
            Validator.ValidateObjectIsNotNULL(task, string.Format(Constants.ITEM_NULL_ERR, "Task"));
            this.tasks.Remove(task);
        }
    }
}
