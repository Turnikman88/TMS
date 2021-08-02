namespace TaskManagmentSystem.Models.Contracts
{
    public interface IBoardItem : IHasId //ToDO: Maybe we need to add something
    {
        string Title { get; }
        string Description { get; }
        void AddComment(Comment comment);

        void ChangeStatus();
    }
}
