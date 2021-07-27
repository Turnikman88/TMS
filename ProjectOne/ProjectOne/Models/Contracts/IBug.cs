using ProjectOne.Models.Enums;
using ProjectOne.Models.Enums.Bug;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models.Contracts
{
    public interface IBug : IBoardItem
    {
        Priority Priority { get; }
        Severity Severity { get; }
        Status Status { get; }
    }
}
