using System;
using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowTeamBoards : BaseCommand
    {
        public ShowTeamBoards(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            throw new NotImplementedException();
        }
    }
}
