using TaskManagmentSystem.Models.Enums;
using TaskManagmentSystem.Models.Enums.Story;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IStory
    {
        Priority Priority { get; }
        Size Size { get; }
        Status Status { get; }
        IMember Assignee { get; }
        
    }
}
