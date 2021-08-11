using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;
namespace TaskManagmentSystem.Models
{
    public abstract class BoardItem : IBoardItem, ICommentable, IActivityLog
    {
        private string title;
        private string description;
        private readonly IList<IComment> comments = new List<IComment>();
        private readonly IList<IEventLog> eventLogs = new List<IEventLog>();

        protected BoardItem(int id, string title, string description, string type)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            AddEvent(new EventLog(string.Format(Constants.EVENT_WAS_CREATED, type, id)));
        }

        public string Title
        {
            get => this.title;

            private set
            {
                Validator.ValidateRange(value.Length, Constants.TITLE_MIN_SYMBOLS, Constants.TITLE_MAX_SYMBOLS, string.Format(Constants.STRING_LENGHT_ERR, nameof(Title), Constants.TITLE_MIN_SYMBOLS, Constants.TITLE_MAX_SYMBOLS));
                this.title = value;
            }
        }

        public int Id { get; }

        public string Description
        {
            get => this.description;

            private set
            {
                Validator.ValidateRange(value.Length, Constants.DESCRIPTION_MIN_SYMBOLS, Constants.DESCRIPTION_MAX_SYMBOLS, string.Format(Constants.STRING_LENGHT_ERR, nameof(Description), Constants.DESCRIPTION_MIN_SYMBOLS, Constants.DESCRIPTION_MAX_SYMBOLS));
                this.description = value;
            }
        }

        public IList<IComment> Comments
            => new List<IComment>(this.comments);

        public IList<IEventLog> EventLogs
            => new List<IEventLog>(this.eventLogs);

        public void AddComment(IComment comment)
        {
            Validator.ValidateObjectIsNotNULL(comment, string.Format(Constants.ITEM_NULL_ERR, "Comment"));
            this.comments.Add(comment);
            AddEvent(new EventLog($"New comment was added, Author: {comment.Author}"));
        }
        public void RemoveComment(IComment comment)
        {
            Validator.ValidateObjectIsNotNULL(comment, string.Format(Constants.ITEM_NULL_ERR, "Comment"));
            this.comments.Remove(comment);
            var length = (comment.Content.Length >= 20 ? 20 : comment.Content.Length);
            AddEvent(new EventLog($"Comment was deleted '{comment.Content.Substring(0, length)} ...'"));

        }
        public string ViewHistory()
        {
            return string.Join($"{Environment.NewLine}", eventLogs.OrderByDescending(x => x.EventTime).Select(x => x.ViewInfo()));
        }

        protected void AddEvent(IEventLog eventLog)
        {
            this.eventLogs.Add(eventLog);
        }

        public abstract void ChangeStatus();
        protected abstract string AddAdditionalInfo();
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{this.Title} {Environment.NewLine} " +
                $"Descritpion: {this.Description} {Environment.NewLine} ");
            sb.AppendLine(AddAdditionalInfo());
            if (comments.Count > 0)
            {
                sb.AppendLine(Constants.PRINT_INFO_SEPARATOR);
                foreach (var comment in comments)
                {
                    sb.AppendLine(comment.ToString());
                }
                sb.AppendLine(Constants.PRINT_INFO_SEPARATOR);
            }
            else
            {
                sb.AppendLine("No comments");
            }

            return sb.ToString().TrimEnd();
        }

    }
}
