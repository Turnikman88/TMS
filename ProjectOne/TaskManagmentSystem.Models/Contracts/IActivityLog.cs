using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IActivityLog
    {
        IList<IEventLog> EventLogs { get; }
    }
}
