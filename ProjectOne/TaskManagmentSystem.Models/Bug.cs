using System;
using System.Collections.Generic;
using TaskManagmentSystem.Models.Common;
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

        public Bug(int id, string title, string description, List<string> steps)
            : base(id, title, description, "Bug")
        {
            this.Priority = Priority.Low;
            this.steps = steps;
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

        public IList<string> Steps
        {
            get => new List<string>(this.steps);
        }

        public IMember Assignee { get; private set; }

        public void ChangePriority()
        {
            _ = this.priority != Priority.High ? priority++ : priority = Priority.Low;
            AddEvent(new EventLog($"Priority for ID {this.Id} {this.Title} was changed to {this.Priority}"));
        }
        public void ChangeSeverity()
        {
            _ = severity != Severity.Critical ? severity++ : severity = Severity.Minor;

            AddEvent(new EventLog($"Saverity for ID {this.Id} {this.Title} was changed to {this.Severity}"));
        }
        public override void ChangeStatus()
        {
            this.status = this.status == Status.Active ? Status.Fixed : Status.Active;
            AddEvent(new EventLog($"Status for ID {this.Id} {this.Title} was changed to {this.Status}"));
        }
        public void AddAssignee(IMember assignee)
        {
            Validator.ValidateObjectIsNotNULL(assignee, string.Format(Constants.ITEM_NULL_ERR, "Assignee"));
            this.Assignee = assignee;
            AddEvent(new EventLog($"Bug with Id: {this.Id} was assigned to {assignee.Name}"));
        }
        public void RemoveAssignee()
        {
            this.Assignee = null;
            AddEvent(new EventLog($"{this.GetType().Name} with Id: {this.Id} was unassigned!"));
        }
        public override string ToString()
        {
            return "Bug: " + base.ToString();
        }
        protected override string AddAdditionalInfo()
        {
            var assignee = this.Assignee is null ? "No assignee" : this.Assignee.Name;
            return $"Assignee {assignee} {Environment.NewLine} Status: {this.Status} {Environment.NewLine} Priority: {this.Priority} {Environment.NewLine} Saverity: {this.Severity} {Environment.NewLine} Steps to reproduce: {Environment.NewLine}" + String.Join(Environment.NewLine, this.steps);
        }
    }
}
