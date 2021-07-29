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
        private IList<string> steps;
         
        public Bug(string title, string description)
            :base(title, description)
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

        public override void AdvanceStatus()
        {
            if (this.status == Status.Fixed)
            {
                throw new UserInputException(string.Format(Constants.STATUS_ADVANCE_ERROR, Status.Fixed.ToString()));
            }
            status++;
        }
        public override void RevertStatus()
        {
            if (this.status == Status.Active)
            {
                throw new UserInputException(string.Format(Constants.STATUS_REVERT_ERROR, Status.Active.ToString()));
            }
            status--;
        }

    }
}
