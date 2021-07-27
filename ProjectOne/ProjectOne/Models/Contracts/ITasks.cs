using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models.Contracts
{
    public interface ITasks
    {
        IList<string> Tasks { get; } //ToDo: Each member must have a name, list of tasks
    }
}
