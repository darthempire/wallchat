using System;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using wallchat.Api;
using wallchat.CustomProvider.App.Core;

[ assembly: OwinStartup ( typeof ( Startup ) ) ]

namespace wallchat.Api
{
    public class Startup
    {
        public void Configuration ( IAppBuilder app )
        {
            ConfigureOAuth (app);
            app.UseCors (CorsOptions.AllowAll);
        }

        public void ConfigureOAuth ( IAppBuilder app )
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString ("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds (5),
                Provider = new SimpleAuthorizationServerProvider( ),
                RefreshTokenProvider = new SimpleRefreshTokenProvider( )
            };

            // Token Generation
            app.UseOAuthAuthorizationServer (oAuthServerOptions);
            app.UseOAuthBearerAuthentication (new OAuthBearerAuthenticationOptions( ));
        }
    }
}