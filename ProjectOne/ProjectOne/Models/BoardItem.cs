using ProjectOne.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models
{
    public abstract class BoardItem : IBoardItem
    {
        public string Title => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();
    }
}
