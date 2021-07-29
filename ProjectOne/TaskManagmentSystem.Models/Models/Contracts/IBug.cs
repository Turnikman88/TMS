using ProjectOne.Models.Enums;
using ProjectOne.Models.Enums.Bug;
using System.Collections.Generic;

namespace ProjectOne.Models.Contracts
{
    public interface IBug : IBoardItem
    {
        Priority Priority { get; }
        Severity Severity { get; }
        Status Status { get; }
        List<string> Steps { get; }
        IMember Assignee { get; }
    }
}
