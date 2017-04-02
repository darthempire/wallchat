using System;
using NLog;
using wallchat.DAL.App.Contracts;
using wallchat.Model.App.DTO;
using wallchat.Model.App.Entity;
using wallchat.Repository.App.Authorization;
using wallchat.Service.Contracts;

namespace wallchat.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly Logger _logger;

        public UserService (
            IUnitOfWork unitOfWork,
            IUserRepository userRepository )
        {
            _logger = LogManager.GetCurrentClassLogger();
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public User FindUser ( long id )
        {
            var user = _userRepository.GetById (id);
            return user;
        }

        public void CreateUser ( RegisterUserDTO userDto)
        {
            var user = new User();

            if (userDto != null)
            {
                user.UserName = userDto.UserName;
                user.PasswordHash = userDto.PasswordHash;
                user.PhoneNumber = userDto.PhoneNumber;
                user.Email = userDto.Email;
                user.DateRegistration = DateTime.Now;

                _userRepository.Add(user);
            }
        }
    }
}