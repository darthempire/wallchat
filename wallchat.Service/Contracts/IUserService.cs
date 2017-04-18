using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using wallchat.Model.App.DTO.Users;
using wallchat.Model.App.Entity;

namespace wallchat.Service.Contracts
{
    public interface IUserService
    {
        UserDTO FindUser ( long id );
        void UpdateUser ( UserDTO userDto );
        void DeleteUser ( long id );
        List<UserDTO> GetAllUsers();
        List<UserDTO> GetAllUsers ( Expression<Func<User, bool>> where );
        void CreateUser ( RegisterUserDTO userDto );
    }
}