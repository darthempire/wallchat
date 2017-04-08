using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using wallchat.Api.App.Filters;
using wallchat.Api.Models.Errors;
using wallchat.Api.Models.User;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO;
using wallchat.Service.Contracts;

namespace wallchat.Api.Controllers
{
    [ RoutePrefix ( "api/Account" ) ]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController ( IUserService userService )
        {
            _userService = userService;
        }

        // POST api/Account/Register
        [ AllowAnonymous ]
        [ Route ( "Register" ) ]
        public async Task<IHttpActionResult> Register ( RegisterUserModel userModel )
        {
            if( !ModelState.IsValid )
                return BadRequest (ModelState);
            try
            {
                var user = new RegisterUserDTO { UserName = userModel.UserName, PasswordHash = userModel.Password };
                _userService.CreateUser (user);
                return Ok( );
            }
            catch( Exception e )
            {
                return BadRequest (e.Message);
            }
        }

        // GET api/Account
        [Role("manager")]
        public IHttpActionResult Get()
        {
            try
            {
                var users = _userService.GetAllUsers( );
                Mapper.Initialize (
                    cfg => cfg.CreateMap<UserDTO, UserViewModel>( ));
                var viewUsers = Mapper.Map<List<UserDTO>, List<UserViewModel>> (users);
                return Json (viewUsers);
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json (error);
            }
            catch( Exception ex )
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json (error);
            }
        }

        // GET api/Account/5
        [Role("manager")]
        public IHttpActionResult Get ( int id )
        {
            try
            {
                var user = _userService.FindUser (id);
                Mapper.Initialize (
                    cfg => cfg.CreateMap<UserDTO, UserViewModel>( ));
                var viewUser = Mapper.Map<UserDTO, UserViewModel> (user);
                return Json (viewUser);
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json (error);
            }
            catch( Exception ex )
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json (error);
            }
        }

        // DELETE api/Account/5
        [Role("supermanager")]
        public IHttpActionResult Delete ( int id )
        {
            try
            {
                _userService.DeleteUser (id);
                return Ok( );
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json (error);
            }
            catch( Exception ex )
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json (error);
            }
        }

        // PUT api/Account/5
        [Role("manager")]
        public IHttpActionResult Update ( UserViewModel userModel )
        {
            try
            {
                Mapper.Initialize (
                    cfg => cfg.CreateMap<UserViewModel, UserDTO>( ));
                var viewDto = Mapper.Map<UserViewModel, UserDTO> (userModel);
                _userService.UpdateUser (viewDto);
                return Ok( );
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json (error);
            }
            catch( Exception ex )
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json (error);
            }
        }
    }
}