using ProjectOne.Models.Common;
using ProjectOne.Models.Contracts;
using ProjectOne.Models.Enums.Feedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models
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
    }
}
