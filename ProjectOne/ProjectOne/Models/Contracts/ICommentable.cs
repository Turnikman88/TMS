using System.Collections.Generic;

namespace ProjectOne.Models.Contracts
{
    public interface ICommentable
    {
        IList<IComment> Comments { get; }
    }
}
