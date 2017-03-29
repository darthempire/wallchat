using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using wallchat.DAL.App.Contracts;
using wallchat.DAL.App.Implementations;
using wallchat.Model.App.Entity;
using wallchat.Repository.App.Authorization;

namespace wallchat.CustomProvider.App.Repository
{
    public class AuthRepository : IDisposable
    {
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

        #region Data

        private readonly IClientRepository _clientRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;


        public AuthRepository()
        {
            IKernel ninjectKernel = new StandardKernel( );

            ninjectKernel.Bind<IDatabaseFactory>( ).To<DatabaseFactory>( );
            ninjectKernel.Bind<IUnitOfWork>( ).To<UnitOfWork>( );

            ninjectKernel.Bind<IClientRepository>( ).To<ClientRepository>( );
            ninjectKernel.Bind<IRefreshTokenRepository>( ).To<RefreshTokenRepository>( );
            ninjectKernel.Bind<IUserRepository>( ).To<UserRepository>( );

            _refreshTokenRepository = ninjectKernel.Get<IRefreshTokenRepository>( );
            _userRepository = ninjectKernel.Get<IUserRepository>( );
            _clientRepository = ninjectKernel.Get<IClientRepository>( );
        }

        #endregion

        #region Users

        private IEnumerable<User> AllUsers => _userRepository.GetAll( ).ToList( );

        public User FindUser ( string userName, string passwordHash )
        {
            return AllUsers.FirstOrDefault (p => p.UserName == userName && p.PasswordHash == passwordHash);
        }

        public User FindUser ( long id )
        {
            var user = _userRepository.GetById (id);
            return user;
        }

        public void RegisterUser ( User user )
        {
            _userRepository.Add (user);
        }

        #endregion

        #region RefreshToken

        public bool AddRefreshToken ( RefreshToken token )
        {
            _refreshTokenRepository.Add (token);
            return true;
        }

        private List<RefreshToken> GetAllRefreshTokens()
        {
            return _refreshTokenRepository.GetAll( ).ToList( );
        }

        public RefreshToken GetRefreshToken ( string id )
        {
            return _refreshTokenRepository.GetById (id);
        }

        public bool RemoveRefreshToken ( string id )
        {
            _refreshTokenRepository.Delete (GetAllRefreshTokens( ).FirstOrDefault (p => p.Id == id));
            return true;
        }

        #endregion
    }
}