using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace wallchat.Api.App.Filters
{
    public class AuthAttribute : Attribute, IAuthorizationFilter
    {
        public bool AllowMultiple { get; }

        private string[] usersList;

        public AuthAttribute(params string[] users)
        {
            this.usersList = users;
        }

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {


            ClaimsPrincipal principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            var firstOrDefault = principal.Claims.FirstOrDefault(c => c.Type == "role");
            if (firstOrDefault != null && ( principal == null || !usersList.Contains(firstOrDefault.Value) ))
            {
                return Task.FromResult<HttpResponseMessage>(
                       actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            else
            {
                return continuation();
            }
        }
    }
}