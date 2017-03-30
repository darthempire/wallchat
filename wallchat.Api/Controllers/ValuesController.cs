using System;
using System.Collections.Generic;
using System.Web.Http;
using wallchat.Model.App.Entity;
using wallchat.Service.Contracts;

namespace wallchat.Api.Controllers
{
    public class ValuesController : ApiController
    {
        readonly IUserService _userService;

        public ValuesController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/<controller>
        public IEnumerable < string > Get ( )
        {

            //var user = new User (  );

            //user.Email = "vasya@mail.com";
            //user.PasswordHash = "123456";
            //user.DateRegistration = DateTime.Now;
            //user.UserName = "darthvasya";

            //_userService.CreateUser (user);

            try
            {
                var user = _userService.FindUser(1);
                return new[] { "value1", "value2", user.Email.ToString() };
            }
            catch ( Exception ex )
            {
                return new[] {"dfs", "" + ex.Message};
                throw;
            }




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