﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using wallchat.Api.App.Filters;
using wallchat.Api.Models.Errors;
using wallchat.Api.Models.User;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO.Users;
using wallchat.Service.Contracts;

namespace wallchat.Api.Controllers
{
    [ RoutePrefix ( "api/users" ) ]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;

        public UsersController ( IUserService userService )
        {
            _userService = userService;
        }

        private long CurrentUserId
        {
            get
            {
                var principal = RequestContext.Principal as ClaimsPrincipal;
                var userId = principal?.Claims.FirstOrDefault(c => c.Type == "userId");
                return userId != null ? Convert.ToInt64(userId?.Value) : 0;
            }
        }

        //lol
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
        //[Authorize]
        [Role("*")]
        public IHttpActionResult Get()
        {
            try
            {
                var users = _userService.GetAllUsers( );
                Mapper.Initialize (
                    cfg => cfg.CreateMap<UserDTO, UserModel>( ));
                var viewUsers = Mapper.Map<List<UserDTO>, List<UserModel>> (users);
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
        [Role("*")]
        public IHttpActionResult Get ( int id )
        {
            try
            {
                var user = _userService.FindUser (id);
                Mapper.Initialize (
                    cfg => cfg.CreateMap<UserDTO, UserModel>( ));
                var viewUser = Mapper.Map<UserDTO, UserModel> (user);
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
        [Role("*")]
        [ HttpDelete ]
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
        [Role("*")]
        [ HttpPut ]
        public IHttpActionResult Update ( UserUpdateModel userModel )
        {
            try
            {
                Mapper.Initialize (
                    cfg => cfg.CreateMap<UserUpdateModel, UpdateUserDTO>( ));
                var viewDto = Mapper.Map<UserUpdateModel, UpdateUserDTO> (userModel);
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