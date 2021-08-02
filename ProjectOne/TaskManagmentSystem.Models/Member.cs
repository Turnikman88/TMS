﻿using System.Collections.Generic;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Models
{
    public class Member : IMember, IActivityLog   //ToDo: Ask Kalin
    {
        private string name;
        private IList<IEventLog> eventLogs = new List<IEventLog>();
        private IList<IBoardItem> tasks = new List<IBoardItem>();

        public Member(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public string Name
        {
            get => this.name;
            private set
            {
                Validator.ValidateObjectIsNotNULL(value, string.Format(Constants.ITEM_NULL_ERR, nameof(Member)));
                Validator.ValidateRange(value.Length, Constants.MEMBER_NAME_MIN_SYMBOLS, Constants.MEMBER_NAME_MAX_SYMBOLS, string.Format(Constants.STRING_LENGHT_ERR, nameof(Member), Constants.MEMBER_NAME_MIN_SYMBOLS, Constants.MEMBER_NAME_MAX_SYMBOLS));
                this.name = value;
            }
        }

        //ToDo: event logging?
        public IList<IEventLog> EventLogs
            => new List<IEventLog>(this.eventLogs);

        public IList<IBoardItem> Tasks
        {
            get => new List<IBoardItem>(this.tasks);
            private set
            {
                Validator.ValidateObjectIsNotNULL(value, Constants.MEMBER_FIRST_TASK_NULL);
                this.tasks = value;
            }
        }

        public int Id { get; }

        public void AddTask(IBoardItem task)
        {
            Validator.ValidateObjectIsNotNULL(task, string.Format(Constants.ITEM_NULL_ERR, "Task"));
            this.tasks.Add(task);
        }
    }
}
