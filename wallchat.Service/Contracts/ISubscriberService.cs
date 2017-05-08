
using System.Collections.Generic;
using wallchat.Model.App.DTO.Users;

namespace wallchat.Service.Contracts
{
    public interface ISubscriberService
    {
        void Subscribe(long subscriberId, long userId);
        void Unsubscribe(long subscriberId, long userId);
        void Unsubscribe(int subscribtionId);
        SubscriberDTO Find(int subscriptionId);
        List<SubscriberDTO> Find(long userId);
        List<SubscriberDTO> GetAll();


    }
}
