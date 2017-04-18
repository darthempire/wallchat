using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using NLog;
using wallchat.DAL.App.Contracts;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO.Users;
using wallchat.Model.App.Entity;
using wallchat.Model.App.Enums;
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

        public UserDTO FindUser ( long id )
        {
            try
            {
                var user = _userRepository.GetById (id);
                Mapper.Initialize (
                    cfg => cfg.CreateMap<User, UserDTO>( ));
                var userDto = Mapper.Map<User, UserDTO> (user);
                _logger.Info ("Get User: id = " + id);
                return userDto;
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
            try
            {
                if (userDto == null) return;
                _logger.Info("Start create new user");
                Mapper.Initialize(
                    cfg => cfg.CreateMap<RegisterUserDTO, User>());
                var user = Mapper.Map<RegisterUserDTO, User>(userDto);

                user.DateRegistration = DateTime.Now;
                user.RoleId = Convert.ToInt32(Roles.User);

                _userRepository.Add(user);
                _logger.Info("User with Id " + user.Id + "created");
            }
            catch (RepositoryException re)
            {
                throw new ServiceException("Repositiry ex: " + re.Message);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
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
                    cfg => cfg.CreateMap<UserDTO, User>( ));
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

        public List<UserDTO> GetAllUsers()
        {
            try
            {
                _logger.Info ("Start getting all users");
                var users = _userRepository.GetAll( );
                Mapper.Initialize (
                    cfg => cfg.CreateMap<User, UserDTO>( ));
                var usersDto = Mapper.Map<IEnumerable<User>, List<UserDTO>> (users);
                _logger.Info ("Get all users");
                return usersDto;
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

        public List<UserDTO> GetAllUsers ( Expression<Func<User, bool>> where )
        {
            try
            {
                _logger.Info ("Start getting all users");
                var users = _userRepository.GetMany (where);
                Mapper.Initialize (
                    cfg => cfg.CreateMap<User, UserDTO>( ));
                var usersDto = Mapper.Map<IEnumerable<User>, List<UserDTO>> (users);
                _logger.Info ("Get all users");
                return usersDto;
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