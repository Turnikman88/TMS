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

        public Story(string title, string description)
            :base(title, description)
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

        public override void AdvanceStatus()
        {
            if(this.status == Status.Done)
            {
                throw new UserInputException(string.Format(Constants.STATUS_ADVANCE_ERROR, Status.Done.ToString()));
            }
            status++;
        }
        public override void RevertStatus()
        {
            if (this.status == Status.NotDone)
            {
                throw new UserInputException(string.Format(Constants.STATUS_REVERT_ERROR, Status.NotDone.ToString()));
            }
            status--;
        }
    }
}
