using ProjectOne.Models.Common;
using ProjectOne.Models.Contracts;
using System.Collections.Generic;

namespace ProjectOne.Models
{
    public class Board : IBoard, IActivityLog
    {
        private string name;
        private IList<IBoardItem> tasks = new List<IBoardItem>();
        private IList<IEventLog> events = new List<IEventLog>();


        public string Name
        {
            get => this.name;
            private set
            {
                Validator.ValidateObjectIsNotNULL(value, string.Format(Constants.ITEM_NULL_ERR, nameof(Board)));
                Validator.ValidateNameUniqueness(value);
                Validator.ValidateRange(value.Length, Constants.BOARD_NAME_MIN_SYMBOLS, Constants.BOARD_NAME_MAX_SYMBOLS, string.Format(Constants.STRING_LENGHT_ERR, nameof(Team), Constants.BOARD_NAME_MIN_SYMBOLS, Constants.BOARD_NAME_MAX_SYMBOLS));
            }
        }

        public IList<IBoardItem> Tasks
            => new List<IBoardItem>(this.tasks);


        public IList<IEventLog> EventLogs
            => new List<IEventLog>(this.events);

        //TODO - дали тези таскове ще имат пълна функционалност
        public void AddTask(IBoardItem task)
        {
            Validator.ValidateObjectIsNotNULL(task, string.Format(Constants.ITEM_NULL_ERR, "Task"));
            this.Tasks.Add(task);
        }
    }
}
