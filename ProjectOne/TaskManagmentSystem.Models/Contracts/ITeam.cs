using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface ITeam : IName, IHasId
    {
        IList<IMember> Members { get; }
        IList<IBoard> Boards { get; }
        IList<IMember> Administrators { get; }
        void AddBoard(IBoard board);
        void AddMember(IMember member);
        void AddAdministrator(IMember admin);
    }
}
