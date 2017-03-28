using Microsoft.Owin;
using Owin;
using wallchat.Api.App_Start;

[ assembly: OwinStartup ( typeof ( Startup ) ) ]

namespace wallchat.Api.App_Start
{
    public class Startup
    {
        public void Configuration ( IAppBuilder app )
        {
            //ConfigureOAuth(app);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
    }
}