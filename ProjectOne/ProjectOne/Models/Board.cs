using ProjectOne.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectOne.Models
{
    public class Board : IBoard
    {
        public string Name => throw new NotImplementedException();

        public IList<IBoardItem> BoardItems => throw new NotImplementedException();

        public IList<string> Tasks => throw new NotImplementedException();
    }
}
