﻿using System;
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

    public class UserViewModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public virtual Role Role { get; set; }

        [ StringLength ( 14 ) ]
        public string UserName { get; set; }

        [ DataType ( DataType.EmailAddress ) ]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneConfirmed { get; set; }

        [ DataType ( DataType.DateTime ) ]
        public DateTime DateRegistration { get; set; }
    }
}