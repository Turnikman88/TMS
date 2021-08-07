﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;
using TaskManagmentSystem.Models.Enums;

namespace TaskManagmentSystem.Models
{
    public class Member : IMember, IActivityLog   //ToDo: Ask Kalin
    {
        private string name;
        private IList<IEventLog> eventLogs = new List<IEventLog>();
        private IList<IBoardItem> tasks = new List<IBoardItem>();
        private string password;
        private Role role;

        public Member(int id, string name, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
            this.Role = Role.Normal;
            AddEvent(new EventLog(string.Format(Constants.EVENT_WAS_CREATED, "Member", id)));
        }
        public int Id { get; }
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
        public string Password
        {
            get => this.password;
            private set
            {
                Validator.ValidatePattern(value, Constants.PASSWORD_PATTERN, Constants.PASSWORD_PATTERN_ERR);
                this.password = value;
            }
        }

        public Role Role
        {
            get => this.role;
            private set
            {
                this.role = value;
            }
        }
        //ToDo: event logging?
        public IList<IEventLog> EventLogs
            => new List<IEventLog>(this.eventLogs);

        public IList<IBoardItem> Tasks
        {
            get => new List<IBoardItem>(this.tasks);
        }
        protected void AddEvent(IEventLog eventLog)
        {
            this.eventLogs.Add(eventLog);
        }
        public void AddTask(IBoardItem task)
        {
            Validator.ValidateObjectIsNotNULL(task, string.Format(Constants.ITEM_NULL_ERR, "Task"));
            this.tasks.Add(task);
            AddEvent(new EventLog($"{task.GetType().Name} '{task.Title}' with ID {task.Id} was assigned on '{this.Name}'"));
        }
        public void RemoveTask(IBoardItem task)
        {
            Validator.ValidateObjectIsNotNULL(task, string.Format(Constants.ITEM_NULL_ERR, "Task"));
            this.tasks.Remove(task);
            AddEvent(new EventLog($"{task.GetType().Name} '{task.Title}' with ID {task.Id} was unassigned from '{this.Name}'"));
        }
        public string ChangePass(string newPass)
        {
            Console.WriteLine("Please enter your old password"); //ToDo: maybe in constants ? // I think event log for password is stupid idea
            string oldPass = Console.ReadLine();
            if (this.Password == oldPass)
            {
                this.Password = newPass;
                return string.Format(Constants.PASSWORD_CHANGED_SUCC, oldPass, newPass);
            }
            return Constants.PASSWORD_CHANGE_ERR;
        }
        public void ChangeRole(string role)
        {
            this.Role = Validator.ParseRole(role);
            AddEvent(new EventLog($"User {this.Name} with ID {this.Id} changed his role to {this.Role}"));
        }
        public string ViewHistory()
        {
            return string.Join($"{Environment.NewLine}", eventLogs.Select(x => x.ViewInfo()));
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Name: {this.Name}, ID: {this.Id}, Role: {this.Role} user, Number of assigned tasks: {this.Tasks.Count}");
            return sb.ToString();
        }
    }
}
