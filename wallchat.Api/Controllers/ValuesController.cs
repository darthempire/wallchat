using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using wallchat.Service.Contracts;

namespace wallchat.Api.Controllers
{
    public class User
    {
        public string _UserName { get; set; }
        public int _Id { get; set; }
    }

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

            return new[] {"value1", "value2"};
        }

        // GET api/<controller>/5
        public string Get ( int id )
        {
            return "value";
        }

        public IHttpActionResult Post([FromBody]User user)
        {
            if (user != null)
                user._Id += 10;

            return Json(user,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
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