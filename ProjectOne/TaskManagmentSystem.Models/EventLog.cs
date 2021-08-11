using System;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Models
{
    public class EventLog : IEventLog
    //ToDo: Implement later!
    {
        //fields
        const string logDateFormat = "yyyy/MM/dd";
        const string logTimeFormat = "HH:mm:ss";
        private string description;
        //private DateTime eventDate;

        //ctor
        public EventLog(string description)
        {
            this.Description = description;
            EventTime = DateTime.Now;
        }

        //prop
        public string Description
        {
            get => this.description;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new UserInputException(Constants.NON_EMPTY_DESCRIPTION);
                }
                this.description = value;
            }
        }
        public DateTime EventTime { get; }

        //methods
        public string ViewInfo()
        {
            return $"[ {EventTime.Date.ToString(logDateFormat)} | {EventTime.ToString(logTimeFormat)} ]{Description}" + Environment.NewLine;
        }
    }
}
