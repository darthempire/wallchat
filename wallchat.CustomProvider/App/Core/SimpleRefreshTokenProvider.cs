using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;
using wallchat.CustomProvider.App.Repository;
using wallchat.Helpers.Cryptography;
using wallchat.Model.App.Entity;

namespace wallchat.CustomProvider.App.Core
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create ( AuthenticationTokenCreateContext context )
        {
            var clientid = context.Ticket.Properties.Dictionary [ "as:client_id" ];

            if ( string.IsNullOrEmpty (clientid) )
                return;

            var refreshTokenId = Guid.NewGuid( ).ToString ("n");

            using (var repo = new AuthRepository( ))
            {
                var refreshTokenLifeTime = context.OwinContext.Get<string> ("as:clientRefreshTokenLifeTime");

                var token = new RefreshToken
                {
                    Id = SHA256.GetHash (refreshTokenId),
                    ClientId = clientid,
                    Subject = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes (Convert.ToDouble (refreshTokenLifeTime))
                };

                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                token.ProtectedTicket = context.SerializeTicket( );

                var result = repo.AddRefreshToken (token);

                if ( result )
                    context.SetToken (refreshTokenId);
            }
        }

        public async Task CreateAsync ( AuthenticationTokenCreateContext context )
        {
            var clientid = context.Ticket.Properties.Dictionary [ "as:client_id" ];

            if ( string.IsNullOrEmpty (clientid) )
                return;

            var refreshTokenId = Guid.NewGuid( ).ToString ("n");

            using (var repo = new AuthRepository( ))
            {
                var refreshTokenLifeTime = context.OwinContext.Get<string> ("as:clientRefreshTokenLifeTime");

                var token = new RefreshToken
                {
                    Id = SHA256.GetHash (refreshTokenId),
                    ClientId = clientid,
                    Subject = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes (Convert.ToDouble (refreshTokenLifeTime))
                };

                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                token.ProtectedTicket = context.SerializeTicket( );

                var result = repo.AddRefreshToken (token);

                if ( result )
                    context.SetToken (refreshTokenId);
            }
        }

        public void Receive ( AuthenticationTokenReceiveContext context )
        {
            var allowedOrigin = context.OwinContext.Get<string> ("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add ("Access-Control-Allow-Origin", new[] {allowedOrigin});

            var hashedTokenId = SHA256.GetHash (context.Token);

            using (var repo = new AuthRepository( ))
            {
                var refreshToken = repo.GetRefreshToken (hashedTokenId);

                if ( refreshToken != null )
                {
                    //Get protectedTicket from refreshToken class
                    context.DeserializeTicket (refreshToken.ProtectedTicket);
                    var result = repo.RemoveRefreshToken (hashedTokenId);
                }
            }
        }

        public async Task ReceiveAsync ( AuthenticationTokenReceiveContext context )
        {
            var allowedOrigin = context.OwinContext.Get<string> ("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add ("Access-Control-Allow-Origin", new[] {allowedOrigin});

            var hashedTokenId = SHA256.GetHash (context.Token);

            using (var repo = new AuthRepository( ))
            {
                var refreshToken = repo.GetRefreshToken (hashedTokenId);

                if ( refreshToken != null )
                {
                    //Get protectedTicket from refreshToken class
                    context.DeserializeTicket (refreshToken.ProtectedTicket);
                    var result = repo.RemoveRefreshToken (hashedTokenId);
                }
            }
        }
    }
}