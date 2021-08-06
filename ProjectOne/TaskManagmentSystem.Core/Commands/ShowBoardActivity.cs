using System;
using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowBoardActivity : BaseCommand
    {
        public ShowBoardActivity(IList<string> commandParameters, IRepository repository)
            :base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            throw new NotImplementedException();
        }
    }
}
