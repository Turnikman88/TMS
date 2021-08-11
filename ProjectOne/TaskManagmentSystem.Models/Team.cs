using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Models
{
    public class Team : ITeam, IActivityLog
    {
        private string name;
        private IList<IMember> members = new List<IMember>();
        private IList<IBoard> boards = new List<IBoard>();
        private IList<IMember> administrators = new List<IMember>();
        private IList<IEventLog> eventLogs = new List<IEventLog>();


        public Team(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public int Id { get; }
        public string Name
        {
            get => this.name;
            private set
            {
                Validator.ValidateObjectIsNotNULL(value, string.Format(Constants.ITEM_NULL_ERR, nameof(Team)));
                Validator.ValidateRange(value.Length, Constants.TEAM_NAME_MIN_SYMBOLS, Constants.TEAM_NAME_MAX_SYMBOLS, string.Format(Constants.STRING_LENGHT_ERR, nameof(Team), Constants.TEAM_NAME_MIN_SYMBOLS, Constants.TEAM_NAME_MAX_SYMBOLS));

                this.name = value;
            }
        }
        public IList<IMember> Members
            => new List<IMember>(this.members);
        public IList<IBoard> Boards
             => new List<IBoard>(this.boards);
        public IList<IMember> Administrators
            => new List<IMember>(administrators);
        public IList<IEventLog> EventLogs
            => new List<IEventLog>(this.eventLogs);
        public void AddBoard(IBoard board)
        {
            Validator.ValidateObjectIsNotNULL(board, string.Format(Constants.ITEM_NULL_ERR, "Board"));
            this.boards.Add(board);
            AddEvent(new EventLog($"Board {board.Name}, ID: {board.Id} was created!"));

        }
        public void AddMember(IMember member)
        {
            Validator.ValidateObjectIsNotNULL(member, string.Format(Constants.ITEM_NULL_ERR, "Member"));
            this.members.Add(member);
            AddEvent(new EventLog($"User {member.Name}, ID: {member.Id} joined team {this.Name}!"));
        }
        public void AddAdministrator(IMember admin)
        {
            Validator.ValidateObjectIsNotNULL(admin, string.Format(Constants.ITEM_NULL_ERR, "Admin"));
            this.administrators.Add(admin);
            AddEvent(new EventLog($"User {admin.Name}, ID: {admin.Id} is admin of team {this.Name}!"));

        }
        public string ViewHistory()
        {
            var sb = new StringBuilder();
            sb.Append(string.Join($"{Environment.NewLine}", eventLogs.OrderByDescending(x => x.EventTime).Select(x => x.ViewInfo())));
            sb.Append(string.Join($"{Environment.NewLine}", members.Select(x => x.ViewHistory())));
            sb.Append(string.Join($"{Environment.NewLine}", boards.Select(x => x.ViewHistory())));
            return sb.ToString().TrimEnd();
        }

        public override string ToString()
        {
            var members = this.Members.Count == 0 ? "There are no members!" :
                string.Join(Environment.NewLine, this.Members.OrderBy(x => x.Name).Select(x => x.ToString()));
            var boards = this.Boards.Count == 0 ? "There are no boards!" :
                string.Join(Environment.NewLine, this.Boards.OrderBy(x => x.Name).Select(x => x.ToString()));
            var sb = new StringBuilder();
            sb.AppendLine(Constants.PRINT_INFO_SEPARATOR);
            sb.AppendLine($"        Name: {this.Name}, ID: {this.Id}");
            sb.AppendLine("         Members:");
            sb.AppendLine($"                {members}");
            sb.AppendLine(Constants.PRINT_INFO_SEPARATOR);
            sb.AppendLine("         Boards:");
            sb.AppendLine($"                {boards}");
            return sb.ToString().TrimEnd();
        }
        protected void AddEvent(IEventLog eventLog)
        {
            this.eventLogs.Add(eventLog);
        }
        public void RemoveMember(IMember member)
        {
            this.members.Remove(member);
            AddEvent(new EventLog($"User {member.Name}, ID: {member.Id} left team {this.Name}!"));
        }

        public void RemoveAdministrator(IMember admin)
        {
            this.administrators.Remove(admin);
        }

        public void RemoveBoard(IBoard board)
        {
            this.boards.Remove(board);
            AddEvent(new EventLog($"Board {board.Name}, ID: {board.Id} was removed!"));
        }
    }
}
