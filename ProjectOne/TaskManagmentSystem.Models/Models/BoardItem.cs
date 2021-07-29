using System.Collections.Generic;
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
        protected BoardItem(string title, string description)
        {
            this.Title = title;
            this.Description = description;
        }

        public string Title
        {
            get => this.title;

            private set
            {
                Validator.ValidateRange(value.Length, Constants.TITLE_MIN_SYMBOLS, Constants.TITLE_MAX_SYMBOLS,
                    string.Format(Constants.STRING_LENGHT_ERR, nameof(Title), Constants.TITLE_MIN_SYMBOLS, Constants.TITLE_MAX_SYMBOLS));
                this.title = value;
            }
        }

        public string Description
        {
            get => this.description;

            private set
            {
                Validator.ValidateRange(value.Length, Constants.DESCRIPTION_MIN_SYMBOLS, Constants.DESCRIPTION_MAX_SYMBOLS, 
                    string.Format(Constants.STRING_LENGHT_ERR, nameof(Description), Constants.DESCRIPTION_MIN_SYMBOLS, Constants.DESCRIPTION_MAX_SYMBOLS));
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
        }

        public abstract void AdvanceStatus();

        public abstract void RevertStatus();
    }
}
