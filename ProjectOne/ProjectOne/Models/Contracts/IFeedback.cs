using ProjectOne.Models.Enums.Feedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models.Contracts
{
    public interface IFeedback
    {
        int Rating { get; }
        Status Status { get; }

    }
}
