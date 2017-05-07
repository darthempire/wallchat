using System;

namespace wallchat.Model.App.DTO.Users
{
    public class SubscriberDTO
    {
        public int Id { get; set; }

        public long SubscriberId { get; set; }

        public long UserId { get; set; }
        public virtual UserDTO User { get; set; }

        public DateTime DateCreation { get; set; }
    }
}