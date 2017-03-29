using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using wallchat.Api.Models.User;
using wallchat.DAL.App.Contracts;
using wallchat.DAL.App.Implementations;
using wallchat.Model.App.Entity;
using wallchat.Repository.App.Authorization;

namespace wallchat.CustomProvider.App.Repository
{
    public class AuthRepository : IDisposable
    {
        #region Data

        private readonly IClientRepository _clientRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;


        public AuthRepository()
        {
            IKernel ninjectKernel = new StandardKernel();

            ninjectKernel.Bind<IDatabaseFactory>().To<DatabaseFactory>();
            ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            ninjectKernel.Bind<IClientRepository>().To<ClientRepository>();
            ninjectKernel.Bind<IRefreshTokenRepository>().To<RefreshTokenRepository>();
            ninjectKernel.Bind<IUserRepository>().To<UserRepository>();

            _refreshTokenRepository = ninjectKernel.Get<IRefreshTokenRepository>();
            _userRepository = ninjectKernel.Get<IUserRepository>();
            _clientRepository = ninjectKernel.Get<IClientRepository>();
        }

        #endregion

        #region Helpers

        public void Dispose()
        {
            //
        }

        #endregion

        #region Client

        public Client FindClient ( string clientId )
        {
            var client = _clientRepository.GetById (clientId);
            return client;
        }

        #endregion

        #region User

        public User FindUser ( long id )
        {
            var user = _userRepository.GetById (id);
            return user;
        }

        public void RegisterUser ( UserModel user )
        {
            var newUser = new User
            {
                UserName = user.UserName,
                PasswordHash = user.Password
            };

            _userRepository.Add (newUser);
        }

        #endregion

        #region RefreshToken

        public void AddRefreshToken ( RefreshToken token )
        {
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _refreshTokenRepository.GetAll( ).ToList( );
        }

        #endregion
    }
}