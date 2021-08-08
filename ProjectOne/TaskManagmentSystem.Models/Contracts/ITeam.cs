using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface ITeam : IName, IHasId
    {
        IList<IMember> Members { get; }
        IList<IBoard> Boards { get; }
        IList<IMember> Administrators { get; }
        IList<IEventLog> EventLogs { get; }
        void AddBoard(IBoard board);
        void RemoveBoard(IBoard board);
        void AddMember(IMember member);
        void RemoveMember(IMember member);
        void AddAdministrator(IMember admin);
        void RemoveAdministrator(IMember admin);
        string ViewHistory();
    }
}
