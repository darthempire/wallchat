using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallchat.Model.App.Entity
{
    public class Article
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public virtual User User { get; set; }
        public long UserId { get; set; }
    }
}
