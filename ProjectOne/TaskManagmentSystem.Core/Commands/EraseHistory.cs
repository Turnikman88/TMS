using System.Collections.Generic;
using System.IO;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    class EraseHistory : BaseCommand
    {
        public EraseHistory(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            //ToDo: Add validation? Anyone can erase? Huh?
            using (StreamWriter writer = new StreamWriter(Constants.PATH_TO_DATABASE + @"\CommandHistory.txt"))
            {

            }
            return Constants.DATABASE_HISTORY_DELETED;
        }
    }
}
