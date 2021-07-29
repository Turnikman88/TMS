using System.Collections.Generic;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface ICommentable
    {
        IList<IComment> Comments { get; }
    }
}
