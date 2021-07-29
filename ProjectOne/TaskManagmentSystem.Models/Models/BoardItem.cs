using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;
using TaskManagmentSystem.Models.Enums.Bug;
using System.Collections.Generic;

namespace TaskManagmentSystem.Models
{
    public abstract class BoardItem : IBoardItem, ICommentable, IActivityLog
    {
        private string title;
        private string description;
        private IList<IComment> comments = new List<IComment>();
        private IList<IEventLog> eventLogs = new List<IEventLog>();
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
                Validator.ValidateRange(value.Length, Constants.TITLE_MIN_SYMBOLS, Constants.TITLE_MAX_SYMBOLS, string.Format(Constants.STRING_LENGHT_ERR, nameof(Title), Constants.TITLE_MIN_SYMBOLS, Constants.TITLE_MAX_SYMBOLS));
            }
        }

        public string Description
        {
            get => this.description;

            private set
            {
                Validator.ValidateRange(value.Length, Constants.DESCRIPTION_MIN_SYMBOLS, Constants.DESCRIPTION_MAX_SYMBOLS, string.Format(Constants.STRING_LENGHT_ERR, nameof(Description), Constants.DESCRIPTION_MIN_SYMBOLS, Constants.DESCRIPTION_MAX_SYMBOLS));
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
