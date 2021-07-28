using System.Collections.Generic;

namespace ProjectOne.Models.Contracts
{
    public interface IMember : IName
    {
        public IList<ITasks> Tasks { get; }
        public IList<IEventLog> EventLogs { get; }
        void AddTask(ITasks task);
    }
}
