using wallchat.DAL.App.Contracts;
using wallchat.Model.App.Entity;
using wallchat.Repository.App.Authorization;
using wallchat.Service.Contracts;

namespace wallchat.Service.Implementations
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
            return user;
        }

        public void CreateUser ( User user )
        {
            _userRepository.Add (user);
        }
    }
}