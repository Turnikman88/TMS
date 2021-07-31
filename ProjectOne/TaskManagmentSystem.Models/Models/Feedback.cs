using System;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;
using TaskManagmentSystem.Models.Enums.Feedback;

namespace TaskManagmentSystem.Models
{
    public class Feedback : BoardItem, IFeedback
    {
        private int rating;
        private Status status;

        public Feedback(int id, string title, string description)
            : base(id, title, description)
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

        public override string ToString()
        {
            return "Feedback: " + base.ToString();
        }
        protected override string AddAdditionalInfo()
        {
            return $"Status: {this.Status} {Environment.NewLine} Rating: {this.Rating}";
        }

    }
}
