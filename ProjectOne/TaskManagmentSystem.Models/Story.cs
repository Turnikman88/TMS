using System;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;
using TaskManagmentSystem.Models.Enums;
using TaskManagmentSystem.Models.Enums.Story;

namespace TaskManagmentSystem.Models
{
    public class Story : BoardItem, IStory
    {
        private Priority priority;
        private Size size;
        private Status status;

        public Story(int id, string title, string description)
            : base(id, title, description, "Story")
        {
            this.Priority = Priority.Low;
            this.Size = Size.Small;
            this.Status = Status.NotDone;
        }

        public Priority Priority
        {
            get => this.priority;
            private set
            {
                this.priority = value;
            }
        }

        public Size Size
        {
            get => this.size;
            private set
            {
                this.size = value;
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

        public IMember Assignee { get; private set; }

        public void AddAssignee(IMember assignee)
        {
            Validator.ValidateObjectIsNotNULL(assignee, string.Format(Constants.ITEM_NULL_ERR, "Assignee"));
            this.Assignee = assignee;
            AddEvent(new EventLog($"Story with ID: {this.Id} was assigned to {assignee.Name}"));
        }
        public void RemoveAssignee()
        {
            this.Assignee = null;
            AddEvent(new EventLog($"{this.GetType().Name} with ID: {this.Id} was unassigned!"));
        }

        public void ChangeSize()
        {
            _ = this.size != Size.Large ? size++ : size = Size.Small;
            AddEvent(new EventLog($"Size for ID: {this.Id} {this.Title} was changed to {this.Size}"));
        }

        public void ChangePriority()
        {
            this.priority = this.priority != Priority.High ? Priority.Medium : Priority.Low;
            AddEvent(new EventLog($"Priority for ID: {this.Id} {this.Title} was changed to {this.Priority}"));
        }

        public override void ChangeStatus()
        {
            _ = this.status != Status.Done ? status++ : status = Status.NotDone;
            AddEvent(new EventLog($"Status for ID: {this.Id} {this.Title} was changed to {this.Status}"));
        }

        public override string ToString()
        {
            return "Story: " + base.ToString();
        }
        protected override string AddAdditionalInfo()
        {
            var assignee = this.Assignee is null ? "No assignee" : this.Assignee.Name;
            return $"Assignee {assignee} {Environment.NewLine} Status: {this.Status} {Environment.NewLine} Priority: {this.Priority} {Environment.NewLine} Size: {this.Size} ";
        }
    }
}
