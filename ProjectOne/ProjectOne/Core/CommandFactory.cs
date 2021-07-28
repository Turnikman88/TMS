using ProjectOne.Commands.Contracts;
using ProjectOne.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Core
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand Create(string commandLine)
        {
            throw new NotImplementedException();
        }
    }
}
