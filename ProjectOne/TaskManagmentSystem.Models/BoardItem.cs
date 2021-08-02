using System;
using System.Collections.Generic;
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
        protected BoardItem(string title, string description, int id)
        {
            this.Title = title;
            this.Description = description;
            this.Id = id;
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

        public void AddComment(Comment comment)
        {
            this.comments.Add(comment);
            AddEvent(new EventLog($"New comment was added, Author: {comment.Author}"));
        }
        public void ViewHistory()
        {
            foreach (var item in eventLogs)
            {
                item.ViewInfo();
            }
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
                sb.AppendLine("-----");
                foreach (var comment in comments)
                {
                    sb.AppendLine(comment.ToString());
                }
                sb.AppendLine("-----");
            }
            else
            {
                sb.AppendLine("No comments");
            }

            return sb.ToString();
        }
    }
}
