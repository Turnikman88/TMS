using TaskManagmentSystem.Models.Enums.Feedback;

namespace TaskManagmentSystem.Models.Contracts
{
    public interface IFeedback
    {
        int Rating { get; }
        Status Status { get; }
    }
}
