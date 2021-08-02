using System;
using System.Collections.Generic;
using TaskManagmentSystem.Models.Contracts;
using TaskManagmentSystem.Models.Enums;
using TaskManagmentSystem.Models.Enums.Bug;

namespace TaskManagmentSystem.Models
{
    public class Bug : BoardItem, IBug
    {
        private Priority priority;
        private Severity severity;
        private Status status;
        private readonly IList<string> steps = new List<string>();

        public Bug(int id,string title, string description, List<string> steps)
            : base(id, title, description)
        {
            this.Priority = Priority.Low;
            this.steps = steps;
            AddEvent(new EventLog($"ID: {id} Bug {title} was created"));
        }
        public Priority Priority
        {
            get => this.priority;
            private set
            {
                this.priority = value;
            }
        }

        public Severity Severity
        {
            get => this.severity;
            private set
            {
                this.severity = value;
            }
        }

        public Status Status
        {
            get => this.status;
            private set
            {
                this.status = value;
            }
        }

        public List<string> Steps
        {
            get => new List<string>(this.steps);
        }

        public IMember Assignee { get; private set; }

        public void ChangePriority()
        {
            this.priority = this.priority != Priority.High ? priority++ : Priority.Low;
            AddEvent(new EventLog($"Priority for ID {this.Id} {this.Title} was changed to {this.Priority}"));

        }

        public void ChangeSeverity()
        {
            this.severity = this.severity != Severity.Critical ? severity++ : Severity.Minor;
            AddEvent(new EventLog($"Saverity for ID {this.Id} {this.Title} was changed to {this.Severity}"));
        }
        public override void ChangeStatus()
        {
            this.status = this.status == Status.Active ? Status.Fixed : Status.Active;
            AddEvent(new EventLog($"Status for ID {this.Id} {this.Title} was changed to {this.Status}"));
        }

        public void AddAssignee(IMember assignee)
        {
            this.Assignee = assignee;
            AddEvent(new EventLog($"Assignee {assignee.Id} was assigneed to ID: {this.Id}, Story {this.Title}"));
        }
        public override string ToString()
        {
            return "Bug: " + base.ToString();
        }
        protected override string AddAdditionalInfo()
        {
            return $"Assignee {this.Assignee.Name} {Environment.NewLine} Status: {this.Status} {Environment.NewLine} Priority: {this.Priority} {Environment.NewLine} Saverity: {this.Severity} {Environment.NewLine} Steps to reproduce: {Environment.NewLine}" + String.Join(Environment.NewLine, this.steps);
        }
    }
}
