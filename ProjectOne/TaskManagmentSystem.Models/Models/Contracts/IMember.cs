using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IMember : IName, IHasId
    {
        IList<ITasks> Tasks { get; }
        IList<IEventLog> EventLogs { get; }
        void AddTask(ITasks task);
    }
}
