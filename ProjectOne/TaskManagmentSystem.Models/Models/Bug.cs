using ProjectOne.Models.Contracts;
using ProjectOne.Models.Enums;
using ProjectOne.Models.Enums.Bug;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models
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
        
    }
}
