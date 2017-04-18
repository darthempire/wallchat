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

        //hello
        //sadfasd
        /// </summary>
        /// <returns></returns>
        //vasya
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new [] { Request.RequestUri.AbsoluteUri, Request.Headers.ToString( ) };
        }

        // PUT api/<controller>/5
        public void Put ( int id, [ FromBody ] string value )
        {
        }

        // DELETE api/<controller>/5
        public void Delete (int id)
        {
        }
    }
}