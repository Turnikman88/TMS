using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IMember : IName
    {
        IList<ITasks> Tasks { get; }
        IList<IEventLog> EventLogs { get; }
        void AddTask(ITasks task);
    }
}
