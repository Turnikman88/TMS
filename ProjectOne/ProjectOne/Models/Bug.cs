using ProjectOne.Models.Contracts;
using ProjectOne.Models.Enums;
using ProjectOne.Models.Enums.Bug;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models
{
    public class Bug : BoardItem, IBug
    {
        public Priority Priority => throw new NotImplementedException();

        public Severity Severity => throw new NotImplementedException();

        public Status Status => throw new NotImplementedException();
    }
}
