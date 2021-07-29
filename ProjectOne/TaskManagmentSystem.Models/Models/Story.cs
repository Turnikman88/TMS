using ProjectOne.Models.Common;
using ProjectOne.Models.Contracts;
using ProjectOne.Models.Enums;
using ProjectOne.Models.Enums.Story;

namespace ProjectOne.Models
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
                throw new UserInputException(string.Format(Constants.STATUS_ADVANCE_ERROR, "Done"));
            }
            status++;
        }
        public override void RevertStatus()
        {
            if (this.status == Status.NotDone)
            {
                throw new UserInputException(string.Format(Constants.STATUS_REVERT_ERROR, "NotDone"));
            }
            status--;
        }
    }
}
