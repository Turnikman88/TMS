using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;
using TaskManagmentSystem.Models.Enums.Feedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagmentSystem.Models
{
    public class Feedback : BoardItem, IFeedback
    {
        private int rating;
        private Status status;

        public Feedback(string title, string description)
            :base(title, description)
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

        public override void AdvanceStatus()
        {
            if (this.status == Status.Done)
            {
                throw new UserInputException(string.Format(Constants.STATUS_ADVANCE_ERROR, Status.Done.ToString()));
            }
            status++;
        }
        public override void RevertStatus()
        {
            if (this.status == Status.New)
            {
                throw new UserInputException(string.Format(Constants.STATUS_REVERT_ERROR, Status.New.ToString()));
            }
            status--;
        }
    }
}
