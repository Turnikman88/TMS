using System.Collections.Generic;

namespace ProjectOne.Models.Contracts
{
    public interface ITasks
    {
        IList<IBoardItem> Tasks { get; } //ToDo: Each member must have a name, list of tasks
    }
}
