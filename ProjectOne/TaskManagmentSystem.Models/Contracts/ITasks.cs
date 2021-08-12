using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface ITasks
    {
        IList<IBoardItem> Tasks { get; }
    }
}
