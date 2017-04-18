using System;
using System.ComponentModel.DataAnnotations;
using wallchat.Model.App.Entity;

namespace wallchat.Model.App.DTO.Users
{
    public class UserDTO
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

        public DateTime DateRegistration { get; set; }

        public int AccessFailedCount { get; set; }
        public bool IsBlocked { get; set; }

        [ DataType ( DataType.DateTime ) ]
        public DateTime? BlockDate { get; set; }

        public string Information { get; set; }

        public Role Role { get; set; }
        public int RoleId { get; set; }
    }


    public class RegisterUserDTO
    {
        [ StringLength ( 14 ) ]
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        [ DataType ( DataType.EmailAddress ) ]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [ StringLength ( 14 ) ]
        public string Name { get; set; }

        [ StringLength ( 14 ) ]
        public string Surname { get; set; }

        public DateTime? DateBirth { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Information { get; set; }
    }
}