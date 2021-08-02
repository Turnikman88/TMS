using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IMember : IName, IHasId
    {
        IList<IBoardItem> Tasks { get; }
        IList<IEventLog> EventLogs { get; }
        void AddTask(IBoardItem task);
    }
}
