using ProjectOne.Models.Enums;
using ProjectOne.Models.Enums.Story;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models.Contracts
{
    public interface IStory
    {
        Priority Priority { get; }
        Size Size { get; }
        Status Status { get; }
        IMember Assignee { get; }
        
    }
}
