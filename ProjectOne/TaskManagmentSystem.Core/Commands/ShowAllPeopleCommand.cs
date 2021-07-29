using System;
using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowAllPeopleCommand : BaseCommand
    {
        public ShowAllPeopleCommand(IRepository repository)
            : base(new List<string>(), repository)

        {

        }
        public override string Execute()
        {
            throw new NotImplementedException();
        }
    }
}
