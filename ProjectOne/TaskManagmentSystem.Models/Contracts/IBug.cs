using System.Collections.Generic;
using TaskManagmentSystem.Models.Enums;
using TaskManagmentSystem.Models.Enums.Bug;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IBug : IBoardItem, HasId
    {
        Priority Priority { get; }
        Severity Severity { get; }
        Status Status { get; }
        List<string> Steps { get; }
        IMember Assignee { get; }
        void ChangePriority();
        void ChangeSeverity();
    }
}
