using System.Collections;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Contracts
{
    public interface IRepository
    {
        IMember CreateUser(string username);
        IList Users { get; }
        IList Teams { get; }



    }
}
