using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace wallchat.Api.App.Filters
{
    public class RoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string [] usersList;

        public RoleAttribute(params string [] users)
        {
            usersList = users;
        }

        public bool AllowMultiple { get; }

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            var firstOrDefault = principal?.Claims.FirstOrDefault(c => c.Type == "role");
            if( firstOrDefault == null && !usersList.Contains(firstOrDefault?.Value) )
                return Task.FromResult(
                    actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized));
            return continuation();
        }
    }
}