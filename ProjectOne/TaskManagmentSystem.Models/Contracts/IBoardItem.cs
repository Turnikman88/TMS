namespace TaskManagmentSystem.Models.Contracts
{
    public interface IBoardItem : ICommentable, IHasId 
    {
        string Title { get; }
        string Description { get; }
        void AddComment(IComment comment);
        void RemoveComment(IComment comment);
        string ViewHistory();
        void ChangeStatus();
    }
}
