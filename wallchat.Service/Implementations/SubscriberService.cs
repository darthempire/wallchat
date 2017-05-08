using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NLog;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO.Users;
using wallchat.Model.App.Entity;
using wallchat.Repository.App;
using wallchat.Service.Contracts;

namespace wallchat.Service.Implementations
{
    public class SubscriberService : ISubscriberService
    {
        private readonly Logger _logger;
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberService ( ISubscriberRepository subscriberRepository )
        {
            _subscriberRepository = subscriberRepository;
            _logger = LogManager.GetCurrentClassLogger ();
        }

        public SubscriberDTO Find ( int subscriptionId )
        {
            try
            {
                var subscriber = _subscriberRepository.GetById ( subscriptionId );
                Mapper.Initialize (
                    cfg => cfg.CreateMap<Subscriber, SubscriberDTO> () );
                var subscriberDto = Mapper.Map<Subscriber, SubscriberDTO> ( subscriber );
                _logger.Info ( "Get Subscriber: id = " + subscriptionId );
                return subscriberDto;
            }
            catch( RepositoryException rep )
            {
                _logger.Error ( "Method: Find Subscriber ( int subscriptionId )" );
                _logger.Error ( rep.Message );
                throw new ServiceException ( "Service exception: from repository ", rep );
            }
            catch( Exception ex )
            {
                _logger.Error ( "Method: Find Subscriber ( int subscriptionId )", ex );
                throw new ServiceException ( "Method: Find Subscriber ( int subscriptionId )", ex );
            }
        }

        public List<SubscriberDTO> Find ( long userId )
        {
            try
            {
                var subscribers = _subscriberRepository.GetAll ().Where ( p => p.SubscriberId == userId );
                Mapper.Initialize (
                    cfg => cfg.CreateMap<Subscriber, SubscriberDTO> () );
                var subscriberDto = Mapper.Map<IEnumerable<Subscriber>, List<SubscriberDTO>> ( subscribers );
                _logger.Info ( "Get Subscribers by subscriber: id = " + userId );
                return subscriberDto;
            }
            catch( RepositoryException rep )
            {
                _logger.Error ( "Method: Find Subscriber ( int subscriptionId )" );
                _logger.Error ( rep.Message );
                throw new ServiceException ( "Service exception: from repository ", rep );
            }
            catch( Exception ex )
            {
                _logger.Error ( "Method: Find Subscriber ( int subscriptionId )", ex );
                throw new ServiceException ( "Method: Find Subscriber ( int subscriptionId )", ex );
            }
        }

        public List<SubscriberDTO> GetAll ()
        {
            try
            {
                var subscribers = _subscriberRepository.GetAll ();
                Mapper.Initialize (
                    cfg => cfg.CreateMap<Subscriber, SubscriberDTO> () );
                var subscriberDto = Mapper.Map<IEnumerable<Subscriber>, List<SubscriberDTO>> ( subscribers );
                _logger.Info ( "Get Subscribers" );
                return subscriberDto;
            }
            catch( RepositoryException rep )
            {
                _logger.Error ( "Method: Find Subscriber ( int subscriptionId )" );
                _logger.Error ( rep.Message );
                throw new ServiceException ( "Service exception: from repository ", rep );
            }
            catch( Exception ex )
            {
                _logger.Error ( "Method: Find Subscriber ( int subscriptionId )", ex );
                throw new ServiceException ( "Method: Find Subscriber ( int subscriptionId )", ex );
            }
        }

        public void Subscribe ( long subscriberId, long userId )
        {
            try
            {
                var subscribers = _subscriberRepository.GetAll ()
                    .FirstOrDefault ( p => p.SubscriberId == subscriberId && p.UserId == userId );
                if( subscribers != null )
                    throw new ServiceException ( "This variant of subscribe also created" );
                if(_subscriberRepository.GetById (userId) != null)
                    _subscriberRepository.Add ( new Subscriber
                    {
                        SubscriberId = subscriberId,
                        UserId = userId,
                        DateCreation = DateTime.Now
                    } );
                throw new ServiceException("No this user");
            }
            catch( RepositoryException rep )
            {
                throw new ServiceException ( "", rep );
            }
            catch( Exception ex )
            {
                throw new ServiceException ( "", ex );
            }
        }

        public void Unsubscribe ( long subscriberId, long userId )
        {
            try
            {
                var subscribe = _subscriberRepository.GetAll ()
                    .FirstOrDefault ( p => p.SubscriberId == subscriberId && p.UserId == userId );
                if( subscribe == null )
                    throw new ServiceException ( "No this subscribe" );
                _subscriberRepository.Delete ( subscribe );
            }
            catch( RepositoryException rep )
            {
                throw new ServiceException ( "", rep );
            }
            catch( Exception ex )
            {
                throw new ServiceException ( "", ex );
            }
        }

        public void Unsubscribe ( int subscribtionId )
        {
            try
            {
                var subscribe = _subscriberRepository.GetAll ()
                    .FirstOrDefault ( p => p.Id == subscribtionId );
                if( subscribe == null )
                    throw new ServiceException ( "No this subscribe" );
                _subscriberRepository.Delete ( subscribe );
            }
            catch( RepositoryException rep )
            {
                throw new ServiceException ( "", rep );
            }
            catch( Exception ex )
            {
                throw new ServiceException ( "", ex );

            }
        }
    }
}