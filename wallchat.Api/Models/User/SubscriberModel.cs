using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wallchat.Api.Models.User
{
    public class SubscriberModel
    {
        public int Id { get; set; }
        public long SubscriberId { get; set; }
        public long UserId { get; set; }
        public virtual UserModel User { get; set; }
        public DateTime DateCreation { get; set; }
    }
}