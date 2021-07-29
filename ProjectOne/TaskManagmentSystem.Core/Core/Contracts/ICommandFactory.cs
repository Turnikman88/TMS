
using ProjectOne.Commands.Contracts;

namespace TaskManagmentSystem.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand Create(string commandLine);
    }
}
