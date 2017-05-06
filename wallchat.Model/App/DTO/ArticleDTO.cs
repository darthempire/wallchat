using System;
using System.ComponentModel.DataAnnotations;
using wallchat.Model.App.DTO.Users;
using wallchat.Model.App.Entity;

namespace wallchat.Model.App.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }
        public string Header { get; set; }
        public string ShortDescription { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }

        public virtual UserDTO User { get; set; }
        public long UserId { get; set; }
    }

    public class RegisterArticleDTO
    {
        public string Text { get; set; }

        public string Header { get; set; }

        public string ShortDescription { get; set; }

        public string ImageUrl { get; set; }

        public long UserId { get; set; }
    }
}