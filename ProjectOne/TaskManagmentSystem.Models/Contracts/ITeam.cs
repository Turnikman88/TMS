using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface ITeam : IName, IHasId
    {
        IList<IMember> Members { get; }
        IList<IBoard> Boards { get; }
        void AddMember(IMember member);
        void AddBoard(IBoard board);
       

    }
}
