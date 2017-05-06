using System;
using System.ComponentModel.DataAnnotations;
using wallchat.Model.App.DTO.Users;

namespace wallchat.Api.Models.News
{
    public class ArticleModel
    {
        public int Id { get; set; }

        [ DataType ( DataType.DateTime ) ]
        public DateTime PublishDate { get; set; }

        public string Header { get; set; }
        public string ShortDescription { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }

        public virtual UserDTO User { get; set; }
        public long UserId { get; set; }
    }
}