using System;

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
