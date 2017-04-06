using System;
using System.Threading.Tasks;
using System.Web.Http;
using wallchat.Api.Models.User;
using wallchat.Model.App.DTO;
using wallchat.Service.Contracts;
using wallchat.Helpers.Exceptions;


namespace wallchat.Api.Controllers
{
    [ RoutePrefix ( "api/Account" ) ]
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;

        public AccountController ( IUserService userService )
        {
            _userService = userService;
        }

        // POST api/Account/Register
        [ AllowAnonymous ]
        [ Route ( "Register" ) ]
        public async Task<IHttpActionResult> Register ( UserModel userModel )
        {
            if ( !ModelState.IsValid )
                return BadRequest (ModelState);
            try
            {
                var user = new RegisterUserDTO { UserName = userModel.UserName, PasswordHash = userModel.Password };
                _userService.CreateUser(user);
                return Ok();
            }
            catch ( Exception e )
            {
                return BadRequest(e.Message);
            }
        }
    }
}