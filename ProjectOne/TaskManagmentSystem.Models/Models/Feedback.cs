using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;
using TaskManagmentSystem.Models.Enums.Feedback;

namespace TaskManagmentSystem.Models
{
    public class Feedback : BoardItem, IFeedback
    {
        private int rating;
        private Status status;

        public Feedback(string title, string description)
            : base(title, description)
        {
            this.Status = Status.New;
        }

        public int Rating
        {
            get => this.rating;
            private set
            {
                Validator.ValidateRange(value, Constants.RATING_MIN_VALUE, Constants.RATING_MAX_VALUE, Constants.RATING_OUTOFRANGE_ERR);
                this.rating = value;
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
        public override void ChangeStatus()
        {
            if (this.status != Status.Done)
            {
                this.status++;
                return;
            }
            this.status = Status.New;
        }


    }
}
