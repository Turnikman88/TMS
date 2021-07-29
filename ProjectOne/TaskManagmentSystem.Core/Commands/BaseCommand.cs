﻿using ProjectOne.Commands.Contracts;
using System;
using System.Collections.Generic;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected BaseCommand(IRepository repository)
                    : this(new List<string>(), repository)
        {
        }

        protected BaseCommand(IList<string> commandParameters, IRepository repository)
        {
            this.CommandParameters = commandParameters;
            this.Repository = repository;
        }

        public IList<string> CommandParameters { get; }
        public IRepository Repository { get; }

        public abstract string Execute();

        protected int ParseIntParameter(string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            throw new UserInputException(Constants.PARSE_INT_ERR);
        }
        //maybe some Enum parser
    }
}