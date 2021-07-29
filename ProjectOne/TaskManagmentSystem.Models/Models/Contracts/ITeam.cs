using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface ITeam : IName
    {
        IList<IMember> Members { get; }
        IList<IBoard> Boards { get; }

    }
}
