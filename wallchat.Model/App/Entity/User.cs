using System;
using System.ComponentModel.DataAnnotations;

namespace wallchat.Model.App.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [ StringLength ( 14 ) ]
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        [ DataType ( DataType.EmailAddress ) ]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneConfirmed { get; set; }

        [ DataType ( DataType.DateTime ) ]
        public DateTime DateRegistration { get; set; }

        public int AccessFailedCount { get; set; }
    }
}