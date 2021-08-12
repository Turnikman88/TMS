using System;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Models
{
    public class EventLog : IEventLog
    {
        const string logDateFormat = "yyyy/MM/dd";
        const string logTimeFormat = "HH:mm:ss";
        private string description;
        
        public EventLog(string description)
        {
            this.Description = description;
            EventTime = DateTime.Now;
        }

        
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
        
        public string ViewInfo()
        {
            return $"[ {EventTime.Date.ToString(logDateFormat)} | {EventTime.ToString(logTimeFormat)} ]{Description}";
        }
    }
}
