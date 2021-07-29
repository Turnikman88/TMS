using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagmentSystem.Models.Common
{
    public class UserInputException : ApplicationException
    {
        public UserInputException(string message)
            :base(message)
        {

        }
    }
}
