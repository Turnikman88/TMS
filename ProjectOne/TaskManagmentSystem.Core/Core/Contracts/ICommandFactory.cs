
using ProjectOne.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand Create(string commandLine);
    }
}
