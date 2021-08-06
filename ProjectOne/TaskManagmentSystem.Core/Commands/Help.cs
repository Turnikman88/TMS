using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class Help : BaseCommand
    {
        public Help(IList<string> commandParameters, IRepository repository)
            :base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            
            var help = File.ReadAllText(Constants.PATH_TO_DATABASE + "help.txt");
            return help;
        }
    }
}
