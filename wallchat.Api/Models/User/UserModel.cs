using System;
using System.ComponentModel.DataAnnotations;
using wallchat.Model.App.Entity;

namespace wallchat.Api.Models.User
{
    public class RegisterUserModel
    {
        [ Required ]
        [ Display ( Name = "User name" ) ]
        public string UserName { get; set; }

        [ Required ]
        [ StringLength ( 100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6 ) ]
        [ DataType ( DataType.Password ) ]
        [ Display ( Name = "Password" ) ]
        public string Password { get; set; }

        [ DataType ( DataType.Password ) ]
        [ Display ( Name = "Confirm password" ) ]
        [ Compare ( "Password", ErrorMessage = "The password and confirmation password do not match." ) ]
        public string ConfirmPassword { get; set; }
    }

    public class UserModel
    {
        public long Id { get; set; }

        [StringLength(14)]
        public string UserName { get; set; }

        [StringLength(14)]
        public string Name { get; set; }

        [StringLength(14)]
        public string Surname { get; set; }

        public DateTime? DateBirth { get; set; }
        public string ProfileImageUrl { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneConfirmed { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateRegistration { get; set; }

        public int AccessFailedCount { get; set; }
        public bool IsBlocked { get; set; }
        public string Information { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? BlockDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastUpdate { get; set; }

        public virtual Role Role { get; set; }
    }

    public class UserUpdateModel
    {
        public long Id { get; set; }

        [StringLength(14)]
        public string UserName { get; set; }

        [StringLength(14)]
        public string Name { get; set; }

        [StringLength(14)]
        public string Surname { get; set; }
        public DateTime? DateBirth { get; set; }
        public string ProfileImageUrl { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBlocked { get; set; }
        public string Information { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? BlockDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastUpdate { get; set; }
        public int RoleId { get; set; }
    }
}