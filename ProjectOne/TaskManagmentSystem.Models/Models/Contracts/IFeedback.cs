using TaskManagmentSystem.Models.Enums.Feedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IFeedback
    {
        int Rating { get; }
        Status Status { get; }

    }
}
