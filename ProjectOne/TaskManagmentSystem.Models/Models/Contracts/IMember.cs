using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IMember : IName
    {
        public IList<ITasks> Tasks { get; }
        public IList<IEventLog> EventLogs { get; }
        void AddTask(ITasks task);
    }
}
