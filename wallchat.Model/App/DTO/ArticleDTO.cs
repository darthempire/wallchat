using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallchat.Model.App.DTO
{
   public class ArticleDTO
    {
        public int Id { get; set; }
        public  string Text { get; set; }
    }
    public class RegisterArticleDTO
    {
        public string Text { get; set; }
    }
}
