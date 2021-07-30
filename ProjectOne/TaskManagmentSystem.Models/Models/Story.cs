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

        public Story(string title, string description)
            : base(title, description)
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

        public IMember Assignee { get; } //ToDo: later

        public void ChangeSize()
        {
            if (this.size != Size.Large)
            {
                this.size++;
                return;
            }
            this.size = Size.Small;
        }

        public void ChangePriority()
        {
            if (this.priority != Priority.High)
            {
                this.priority++;
                return;
            }
            this.priority = Priority.Low;
        }

        public override void ChangeStatus()
        {
            if (this.status != Status.Done)
            {
                this.status++;
                return;
            }
            this.status = Status.NotDone;
        }

    }
}
