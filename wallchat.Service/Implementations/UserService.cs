using System;
using NLog;
using wallchat.DAL.App.Contracts;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO;
using wallchat.Model.App.Entity;
using wallchat.Repository.App.Authorization;
using wallchat.Service.Contracts;

namespace wallchat.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly Logger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService (
            IUnitOfWork unitOfWork,
            IUserRepository userRepository )
        {
            _logger = LogManager.GetCurrentClassLogger( );
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public User FindUser ( long id )
        {
            try
            {
                var user = _userRepository.GetById (id);
                _logger.Info ("Get User: id = " + id);
                return user;
            }
            catch ( RepositoryException rep )
            {
                _logger.Error ("Method: FindUser ( long id )");
                _logger.Error (rep.Message);
                throw new ServiceException ("Service exception: from repository ", rep);
            }
            catch ( Exception ex )
            {
                _logger.Error ("Method: FindUser ( long id )", ex);
                throw new ServiceException ("Method: FindUser ( long id )", ex);
            }
        }

        public void CreateUser ( RegisterUserDTO userDto )
        {
            var user = new User( );

            if ( userDto != null )
            {
                user.UserName = userDto.UserName;
                user.PasswordHash = userDto.PasswordHash;
                user.PhoneNumber = userDto.PhoneNumber;
                user.Email = userDto.Email;
                user.DateRegistration = DateTime.Now;
                user.RoleId = 1;

                _userRepository.Add (user);
            }
        }
    }
}