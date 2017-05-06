using System;

namespace wallchat.Model.App.Entity
{
    public class Subscriber
    {
        public int Id { get; set; }

        public long SubscriberId { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime DateCreation { get; set; }
    }
}