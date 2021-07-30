using System;
using System.Collections.Generic;
using System.Reflection;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Core.Commands
{
    public class CreateTaskCommand : BaseCommand
    {
        private const int numberOfParameters = 3;
        public CreateTaskCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (CommandParameters.Count != numberOfParameters)
            {
                throw new UserInputException("Invalid number of parameters"); //change later in constants
            }
            string itemType = CommandParameters[0];
            string itemName = CommandParameters[1];
            string itemDescription = CommandParameters[2];
            string namespaceName = Assembly.GetExecutingAssembly().GetName().Name;
            string fullType = $"{namespaceName}.{itemType}";
            Type type;
            try
            {
                type = Type.GetType(namespaceName, true, true);
            }
            catch (Exception)
            {
                throw new UserInputException("BoardItem of that type doesnt exist");  // change later in constants              
            }
            //IBoardItem item = this.Repository.CreateTask();
            return $"{itemType} was created";
        }
    }
}
