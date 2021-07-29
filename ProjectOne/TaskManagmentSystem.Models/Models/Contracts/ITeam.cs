using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface ITeam : IName
    {
        IList<IMember> Members { get; }
        IList<IBoard> Boards { get; }

    }
}
