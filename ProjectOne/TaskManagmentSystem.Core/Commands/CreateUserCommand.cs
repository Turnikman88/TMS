using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    internal class CreateUserCommand : BaseCommand
    {


        public CreateUserCommand(List<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}