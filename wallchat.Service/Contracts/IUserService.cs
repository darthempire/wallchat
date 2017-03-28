using wallchat.Model.App.Entity;

namespace wallchat.Service.Contracts
{
    public interface IUserService
    {
        User FindUser ( long id );
        void CreateUser ( User user );
    }
}