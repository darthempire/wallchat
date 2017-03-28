using System;
using wallchat.DAL.App.Contracts;
using wallchat.Model.App.Entity;
using wallchat.Repository.App.Authorization;
using wallchat.Service.App.Contracts;

namespace wallchat.Service.App.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService (
            IUnitOfWork unitOfWork,
            IUserRepository userRepository )
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public User FindUser ( long id )
        {
            var user = _userRepository.GetById (id);
            if(user == null)
                throw new ArgumentNullException(nameof (id));
            return user;
        }

        public void CreateUser ( User user )
        {
            if ( user != null )
            {
                _userRepository.Add (user);
            }
        }
    }
}