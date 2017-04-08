using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using wallchat.Api.Models.Errors;
using wallchat.Api.Models.User;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO;
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

        // GET api/users
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
                var error = new Error(  );
                error.Message = se.Message;
                error.Code = 12;
                return Json ( error );
            }
            catch( Exception e )
            {
                Console.WriteLine (e);
                throw;
            }

            return null;
        }

        // GET api/<controller>/5
        public string Get ( int id )
        {
            return "value";
        }

        // POST api/<controller>
        public void Post ( [ FromBody ] string value )
        {
        }

        // PUT api/<controller>/5
        public void Put ( int id, [ FromBody ] string value )
        {
        }

        // DELETE api/<controller>/5
        public void Delete ( int id )
        {
        }
    }
}