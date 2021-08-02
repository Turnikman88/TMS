using System;
using System.Collections.Generic;
using System.Text;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public class LogOutCommand : BaseCommand
    {
        public LogOutCommand(IRepository repository)
            : base(new List<string>(), repository)
        {

        }

        public override string Execute()
        {
            if (this.Repository.LoggedUser != null)
            {
                this.Repository.LoggedUser = null;
                return Constants.USER_LOGGED_OUT;
            }
            throw new UserInputException(Constants.NO_USER_LOGGED);
        }
    }
}
