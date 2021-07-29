using ProjectOne.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagmentSystem.Core.Commands
{
    public class ShowAllTeamsCommand : BaseCommand
    {
        public ShowAllTeamsCommand(IRepository repository)
            :base(new List<string>(), repository)
        {

        }
        public override string Execute()
        {
            throw new NotImplementedException();
        }
    }
}
