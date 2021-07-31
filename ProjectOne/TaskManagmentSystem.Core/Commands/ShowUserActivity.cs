using System;
using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowUserActivity : BaseCommand
    {
        public ShowUserActivity(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)

        {

        }
        public override string Execute()
        {
            throw new NotImplementedException();
        }
    }
}
