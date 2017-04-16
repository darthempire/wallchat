using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wallchat.Api.Models.News
{
    public class RegisterArticleModel
    {
        public string Text { get; set; }
        
        public DateTime PublishDate { get; set; }

        public string Header { get; set; }

        public string ShortDescription { get; set; }

        public string ImageUrl { get; set; }
    }
}