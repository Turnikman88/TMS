using ProjectOne.Models.Common;
using ProjectOne.Models.Contracts;

namespace ProjectOne.Models
{
    public class Comment : IComment
    {
        private string author;
        private string content;

        public Comment(string content, string author)
        {
            Content = content;
            Author = author;
        }

        public string Content
        {
            get => this.content;
            protected set
            {
                Validator.ValidateObjectIsNotNULL(value, string.Format(Constants.ITEM_NULL_ERR, "Content"));
                this.content = value;
            }
        }

        public string Author
        {
            get => author;
            protected set
            {
                Validator.ValidateObjectIsNotNULL(value, string.Format(Constants.ITEM_NULL_ERR, "Author"));
                this.author = value;
            }
        }
    }
}
