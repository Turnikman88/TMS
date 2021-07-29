using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;
using System.Collections.Generic;

namespace TaskManagmentSystem.Models
{
    public class Team : ITeam
    {
        private string name;
        private IList<IMember> members = new List<IMember>();
        private IList<IBoard> boards = new List<IBoard>();
        public Team(string name)
        {
            this.Name = name;
        }
        public IList<IMember> Members
            => new List<IMember>(this.members);


        public IList<IBoard> Boards
             => new List<IBoard>(this.boards);


        public string Name
        {
            get => this.name;
            private set
            {
                Validator.ValidateObjectIsNotNULL(value, string.Format(Constants.ITEM_NULL_ERR, nameof(Team)));
                Validator.ValidateRange(value.Length, Constants.TEAM_NAME_MIN_SYMBOLS, Constants.TEAM_NAME_MAX_SYMBOLS, string.Format(Constants.STRING_LENGHT_ERR, nameof(Team), Constants.TEAM_NAME_MIN_SYMBOLS, Constants.TEAM_NAME_MAX_SYMBOLS));
                Validator.ValidateNameUniqueness(value);
                this.name = value;
            }
        }

        public void AddPersonToTeam(IMember member)
        {
            this.members.Add(member);
        }

    }
}
