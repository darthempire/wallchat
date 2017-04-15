using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        //hello
        //sadfasdf
        //sadfasdf
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            //var user = new User (  );

            //user.Email = "vasya@mail.com";
            //user.PasswordHash = "123456";
            //user.DateRegistration = DateTime.Now;
            //user.UserName = "darthvasya";

            //_userService.CreateUser (user);

            return new [] { "value1", "value2" };
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