using System.Collections.Generic;
using TaskManagmentSystem.Models.Enums;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IMember : IName, IHasId
    {
       
        string Password { get; }
        Role Role { get; }
        IList<IEventLog> EventLogs { get; }
        IList<IBoardItem> Tasks { get; }
        void AddTask(IBoardItem task);
        void RemoveTask(IBoardItem task);
        void ChangePass(string newPass);
        void ChangeRole();
        string ViewHistory();
    }
}
