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

        public Bug(string title, string description)
            : base(title, description)
        {
            this.Priority = Priority.Low;
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

        public IMember Assignee { get; } //ToDo: later

        public void ChangePriority()
        {
            if (this.priority != Priority.High)
            {
                this.priority++;
                return;
            }
            this.priority = Priority.Low;
        }

        public void ChangeSeverity()
        {
            if (this.severity != Severity.Critical)
            {
                this.severity++;
                return;
            }
            this.severity = Severity.Minor;
        }
        public override void ChangeStatus()
        {
            this.status = this.status == Status.Active ? Status.Fixed : Status.Active;
        }
    }
}
