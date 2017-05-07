using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wallchat.Model.App.DTO.Users;

namespace wallchat.Service.Contracts
{
    interface ISubscriberService
    {
        SubscriberDTO Find(int id);
        void Update(SubscriberDTO subscriberDto);
        void Delete(int id);
        List<SubscriberDTO> GetAll();
        void Add(SubscriberDTO subscriberDto);
    }
}
