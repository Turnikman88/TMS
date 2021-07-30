using System.Collections.Generic;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Models
{
    public class Member : IMember, IActivityLog
    {
        private string name;
        private IList<IEventLog> eventLogs = new List<IEventLog>();
        private IList<ITasks> tasks = new List<ITasks>();

        public Member(string name)
        {
            this.Name = name;
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

        //ToDo: event logging?
        public IList<IEventLog> EventLogs
            => new List<IEventLog>(this.eventLogs);

        public IList<ITasks> Tasks
        {
            get => new List<ITasks>(this.tasks);
            private set
            {
                Validator.ValidateObjectIsNotNULL(value, Constants.MEMBER_FIRST_TASK_NULL);
                this.tasks = value;
            }
        }

        public void AddTask(ITasks task)
        {
            Validator.ValidateObjectIsNotNULL(task, string.Format(Constants.ITEM_NULL_ERR, "Task"));
            this.tasks.Add(task);
        }
    }
}
