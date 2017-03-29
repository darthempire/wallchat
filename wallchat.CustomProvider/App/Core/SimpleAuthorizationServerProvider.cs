﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using wallchat.CustomProvider.App.Repository;
using wallchat.Model.App.Entity;

namespace wallchat.CustomProvider.App.Core
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication ( OAuthValidateClientAuthenticationContext context )
        {
            var clientId = string.Empty;
            var clientSecret = string.Empty;
            Client client = null;

            if ( !context.TryGetBasicCredentials (out clientId, out clientSecret) )
                context.TryGetFormCredentials (out clientId, out clientSecret);

            if ( context.ClientId == null )
            {
                context.Validated( );
                context.SetError ("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object> (null);
            }

            using (var repo = new AuthRepository( ))
            {
                client = repo.FindClient (context.ClientId);
            }

            if ( client == null )
            {
                context.SetError (
                    "invalid_clientId",
                    $"Client '{context.ClientId}' is not registered in the system.");
                return Task.FromResult<object> (null);
            }

            //if (client.ApplicationType == ApplicationTypes.NativeConfidential) ;
            //{
            //    if (string.IsNullOrWhiteSpace(clientSecret))
            //    {
            //        context.SetError("invalid_clientId", "Client secret should be sent.");
            //        return Task.FromResult<object>(null);
            //    }
            //    if (client.Secret != Crypto.GetHash(clientSecret))
            //    {
            //        context.SetError("invalid_clientId", "Client secret is invalid.");
            //        return Task.FromResult<object>(null);
            //    }
            //}

            if ( !client.Active )
            {
                context.SetError ("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object> (null);
            }

            context.OwinContext.Set ("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set ("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString( ));

            context.Validated( );
            return Task.FromResult<object> (null);
        }

        public override async Task GrantResourceOwnerCredentials ( OAuthGrantResourceOwnerCredentialsContext context )
        {
            var allowedOrigin = context.OwinContext.Get<string> ("as:clientAllowedOrigin") ?? "*";
            context.OwinContext.Response.Headers.Add ("Access-Control-Allow-Origin", new[] {allowedOrigin});

            using (var authRepository = new AuthRepository( ))
            {
                var user = authRepository.FindUser (context.UserName, context.Password);
                if ( user == null )
                {
                    context.SetError ("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }

            var identity = new ClaimsIdentity (context.Options.AuthenticationType);
            identity.AddClaim (new Claim ("name", context.UserName));
            identity.AddClaim (new Claim ("role", "user"));

            var props = new AuthenticationProperties (
                new Dictionary<string, string>
                {
                    {
                        "as:client_id", context.ClientId ?? string.Empty
                    },
                    {
                        "userName", context.UserName
                    }
                });

            var ticket = new AuthenticationTicket (identity, props);
            context.Validated (ticket);
        }

        public override Task TokenEndpoint ( OAuthTokenEndpointContext context )
        {
            foreach ( var property in context.Properties.Dictionary )
                context.AdditionalResponseParameters.Add (property.Key, property.Value);

            return Task.FromResult<object> (null);
        }

        public override Task GrantRefreshToken ( OAuthGrantRefreshTokenContext context )
        {
            var originalClient = context.Ticket.Properties.Dictionary [ "as:client_id" ];
            var currentClient = context.ClientId;

            if ( originalClient != currentClient )
            {
                context.SetError ("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object> (null);
            }

            // Change auth ticket for refresh token requests
            // if only add, this add when refresh token
            //var newIdentity = new ClaimsIdentity ( context.Ticket.Identity );
            //newIdentity.RemoveClaim ( context.Ticket.Identity.Claims.FirstOrDefault(c => c.Type == "clientId") );
            //newIdentity.AddClaim ( new Claim ( "clientId", context.ClientId ) );

            //var newTicket = new AuthenticationTicket ( newIdentity, context.Ticket.Properties );
            context.Validated( );

            return Task.FromResult<object> (null);
        }
    }
}