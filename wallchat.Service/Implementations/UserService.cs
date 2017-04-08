using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
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
            catch( RepositoryException rep )
            {
                _logger.Error ("Method: FindUser ( long id )");
                _logger.Error (rep.Message);
                throw new ServiceException ("Service exception: from repository ", rep);
            }
            catch( Exception ex )
            {
                _logger.Error ("Method: FindUser ( long id )", ex);
                throw new ServiceException ("Method: FindUser ( long id )", ex);
            }
        }

        public void CreateUser ( RegisterUserDTO userDto )
        {
            var user = new User( );

            if( userDto != null )
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

        public void UpdateUser ( UserDTO userDto )
        {
            try
            {
                _logger.Info ("Start updatin user with id " + userDto.Id);
                var user = _userRepository.GetById (userDto.Id);
                if( user == null )
                    throw new ServiceException ("No user with this id");

                Mapper.Initialize (
                    cfg => cfg.CreateMap<UserDTO, User>( )
                        .ForMember ("UserName", opt => opt.MapFrom (src => src.Email)));
                user = Mapper.Map<UserDTO, User> (userDto);
                _userRepository.Update (user);
                _logger.Info ("Update user with id " + user.Id);
            }
            catch( RepositoryException re )
            {
                throw new ServiceException ("Repositiry ex: " + re.Message);
            }
            catch( Exception ex )
            {
                throw new ServiceException (ex.Message);
            }
        }

        public void DeleteUser ( long id )
        {
            try
            {
                _logger.Info ("Start deleting user with id " + id);
                _userRepository.Delete (p => p.Id == id);
                _logger.Info ("Delete User with id " + id);
            }
            catch( RepositoryException re )
            {
                throw new ServiceException ("Repository ex: " + re.Message);
            }
            catch( Exception ex )
            {
                throw new ServiceException (ex.Message);
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                _logger.Info ("Start getting all users");
                var users = _userRepository.GetAll( );
                _logger.Info ("Get all users");
                return users.ToList( );
            }
            catch( RepositoryException re )
            {
                throw new ServiceException ("Repository ex: " + re.Message);
            }
            catch( Exception ex )
            {
                throw new ServiceException (ex.Message);
            }
        }

        public List<User> GetAllUsers ( Expression<Func<User, bool>> where )
        {
            try
            {
                _logger.Info ("Start getting all users");
                var users = _userRepository.GetMany (where);
                _logger.Info ("Get all users");
                return users.ToList( );
            }
            catch( RepositoryException re )
            {
                throw new ServiceException ("Repository ex: " + re.Message);
            }
            catch( Exception ex )
            {
                throw new ServiceException (ex.Message);
            }
        }
    }
}