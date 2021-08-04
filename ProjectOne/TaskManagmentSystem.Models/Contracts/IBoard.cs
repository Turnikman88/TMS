namespace TaskManagmentSystem.Models.Contracts
{
    public interface IBoard : IName, ITasks, IHasId
    {
        void AddTask(IBoardItem task);
        void RemoveTask(IBoardItem task);
    }
}
