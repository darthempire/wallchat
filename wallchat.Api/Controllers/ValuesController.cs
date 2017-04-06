using System.Collections.Generic;
using System.Web.Http;
using wallchat.Service.Contracts;

namespace wallchat.Api.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IUserService _userService;

        public ValuesController ( IUserService userService )
        {
            _userService = userService;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            //var user = new User (  );

            //user.Email = "vasya@mail.com";
            //user.PasswordHash = "123456";
            //user.DateRegistration = DateTime.Now;
            //user.UserName = "darthvasya";

            //_userService.CreateUser (user);

            var user = _userService.FindUser (1);
            return new[] {"value1", "value2", user.UserId};
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