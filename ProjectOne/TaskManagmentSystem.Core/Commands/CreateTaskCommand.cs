using System;
using System.Collections.Generic;
using System.Text;
using TaskManagmentSystem.Core.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class CreateTaskCommand : BaseCommand
    {
        public CreateTaskCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            throw new NotImplementedException();
        }
    }
}
