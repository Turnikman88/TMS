using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IActivityLog
    {
        IList<IEventLog> EventLogs { get; }
    }
}
