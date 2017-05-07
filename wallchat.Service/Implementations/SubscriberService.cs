using System;
using System.Collections.Generic;
using NLog;
using wallchat.DAL.App.Contracts;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO.Users;
using wallchat.Repository.App.Authorization;
using wallchat.Repository.App;
using wallchat.Service.Contracts;

namespace wallchat.Service.Implementations
{
    public class SubscriberService : ISubscriberService
    {
        private readonly Logger _logger;
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberService(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public SubscriberDTO Find(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(SubscriberDTO subscriberDto)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            try
            {
                _logger.Info("Start deleting user with id " + id);
                _subscriberRepository.Delete(p => p.Id == id);
                _logger.Info("Delete User with id " + id);
            }
            catch (RepositoryException re)
            {
                throw new ServiceException("Repository ex: " + re.Message);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public List<SubscriberDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(SubscriberDTO subscriberDto)
        {
            throw new NotImplementedException();
        }
    }
}