namespace TaskManagmentSystem.Models.Contracts
{
    public interface IBoardItem : HasId  //ToDO: Maybe we need to add something
    {
        string Title { get; }
        string Description { get; }
        void ChangeStatus();
    }
}
