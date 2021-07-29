using System;
using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    class AddCommentToTask : BaseCommand
    {
        public AddCommentToTask(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            throw new NotImplementedException();
        }
    }
}
