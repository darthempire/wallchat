using System;
using System.ComponentModel.DataAnnotations;

namespace wallchat.Model.App.Entity
{
    public class User
    {
        public long Id { get; set; }

        [ StringLength ( 14 ) ]
        public string UserName { get; set; }

        [ StringLength ( 14 ) ]
        public string Name { get; set; }

        [ StringLength ( 14 ) ]
        public string Surname { get; set; }

        public DateTime? DateBirth { get; set; }
        public string ProfileImageUrl { get; set; }
        public string PasswordHash { get; set; }

        [ DataType ( DataType.EmailAddress ) ]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneConfirmed { get; set; }

        [ DataType ( DataType.DateTime ) ]
        public DateTime DateRegistration { get; set; }

        public int AccessFailedCount { get; set; }
        public bool IsBlocked { get; set; }
        public string Information { get; set; }

        [ DataType ( DataType.DateTime ) ]
        public DateTime? BlockDate { get; set; }

        [ DataType ( DataType.DateTime ) ]
        public DateTime? LastUpdate { get; set; }

        public virtual Role Role { get; set; }
        public int RoleId { get; set; }
    }
}