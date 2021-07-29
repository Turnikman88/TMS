namespace ProjectOne.Models.Contracts
{
    public interface IBoardItem  //ToDO: Maybe we need to add something
    {
        string Title { get; }
        string Description { get; }
        void AdvanceStatus();
        void RevertStatus();
    }
}
