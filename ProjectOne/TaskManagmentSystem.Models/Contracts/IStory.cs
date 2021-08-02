using TaskManagmentSystem.Models.Enums;
using TaskManagmentSystem.Models.Enums.Story;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IStory : HasId
    {
        Priority Priority { get; }
        Size Size { get; }
        Status Status { get; }
        IMember Assignee { get; }
        void ChangePriority();
        void ChangeSize();
    }
}
