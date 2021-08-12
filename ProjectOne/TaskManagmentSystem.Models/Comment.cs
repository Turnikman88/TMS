using System;
using TaskManagmentSystem.Models.Common;
using TaskManagmentSystem.Models.Contracts;

namespace TaskManagmentSystem.Models
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

        public override string ToString()
        {
            return $"{Constants.PRINT_INFO_SEPARATOR}"
                + Environment.NewLine
                + $"    {this.Content}"
                + Environment.NewLine
                + $"      User: {this.Author}"
                + Environment.NewLine
                + $"{Constants.PRINT_INFO_SEPARATOR}";
        }
    }
}
