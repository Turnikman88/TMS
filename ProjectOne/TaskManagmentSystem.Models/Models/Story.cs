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
    }
}
