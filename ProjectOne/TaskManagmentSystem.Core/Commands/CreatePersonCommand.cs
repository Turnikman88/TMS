using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    internal class CreatePersonCommand : BaseCommand
    {


        public CreatePersonCommand(List<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}