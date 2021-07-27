using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models.Enums.Bug
{
    public enum Status
    {
        Active,
        Fixed
    }
}
namespace ProjectOne.Models.Enums.Story
{
    public enum Status
    {
        NotDone, 
        InProgress,
        Done
    }
}
namespace ProjectOne.Models.Enums.Feedback
{
    public enum Status
    {
        New, 
        Unscheduled, 
        Scheduled,
        Done
    }
}
