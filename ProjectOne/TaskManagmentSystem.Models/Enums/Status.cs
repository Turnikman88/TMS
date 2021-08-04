namespace TaskManagmentSystem.Models.Enums.Bug
{
    public enum Status : byte
    {
        Active,
        Fixed
    }
}
namespace TaskManagmentSystem.Models.Enums.Story
{
    public enum Status : byte
    {
        NotDone, 
        InProgress,
        Done
    }
}
namespace TaskManagmentSystem.Models.Enums.Feedback
{
    public enum Status : byte
    {
        New, 
        Unscheduled, 
        Scheduled,
        Done
    }
}
