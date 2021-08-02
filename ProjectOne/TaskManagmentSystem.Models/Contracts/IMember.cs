using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IMember : IName, HasId
    {
        IList<IBoardItem> Tasks { get; }
        IList<IEventLog> EventLogs { get; }
        void AddTask(IBoardItem task);
    }
}
