using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using wallchat.Api.App.Filters;
using wallchat.Api.Models.Errors;
using wallchat.Api.Models.User;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO.Users;
using wallchat.Service.Contracts;

namespace wallchat.Api.Controllers
{
    [RoutePrefix("api/subscriptions")]
    public class SubscriptionsController : ApiController
    {
        private readonly ISubscriberService _subscriptionService;

        public SubscriptionsController(ISubscriberService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        private long CurrentUserId
        {
            get
            {
                var principal = RequestContext.Principal as ClaimsPrincipal;
                var userId = principal?.Claims.FirstOrDefault(c => c.Type == "userId");
                return userId != null ? Convert.ToInt64(userId?.Value) : 0;
            }
        }

        //все подписки текущего юзера
        // GET api/<controller>
        [Role("*")]
        public IHttpActionResult Get()
        {
            try
            {
                var subscribers = _subscriptionService.Find(Convert.ToInt64(CurrentUserId));
                Mapper.Initialize(
                    cfg => cfg.CreateMap<SubscriberDTO, SubscriberModel>());
                var viewSubscribers = Mapper.Map<List<SubscriberDTO>, List<SubscriberModel>>(subscribers);
                return Json(viewSubscribers);
            }
            catch (ServiceException se)
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json(error);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json(error);
            }

        }

        //все подписки
        // GET api/<controller>
        [Role("*")]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            try
            {
                var subscribers = _subscriptionService.GetAll();
                Mapper.Initialize(
                    cfg => cfg.CreateMap<SubscriberDTO, SubscriberModel>());
                var viewSubscribers = Mapper.Map<List<SubscriberDTO>, List<SubscriberModel>>(subscribers);
                return Json(viewSubscribers);
            }
            catch (ServiceException se)
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json(error);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json(error);
            }

        }

        //получить подписку
        // GET api/<controller>/5
        [Role("*")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var subscribers = _subscriptionService.Find ( id );
                Mapper.Initialize(
                    cfg => cfg.CreateMap<SubscriberDTO, SubscriberModel>());
                var viewSubscribers = Mapper.Map<SubscriberDTO, SubscriberModel>(subscribers);
                return Json(viewSubscribers);
            }
            catch (ServiceException se)
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json(error);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json(error);
            }
        }

        //получить все подписки этого юзера
        // GET api/<controller>/user/5
        [Role("*")]
        [Route("user/{userId:int}")]
        public IHttpActionResult GetByUser(int userId)
        {
            try
            {
                var subscribers = _subscriptionService.Find(Convert.ToInt64 (userId));
                Mapper.Initialize(
                    cfg => cfg.CreateMap<SubscriberDTO, SubscriberModel>());
                var viewSubscribers = Mapper.Map<List<SubscriberDTO>, List<SubscriberModel>>(subscribers);
                return Json(viewSubscribers);
            }
            catch (ServiceException se)
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json(error);
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json(error);
            }
        }


        //подписаться на userId
        // POST api/<controller>/subscribe
        // userId передовать в строке запроса
        [Route("subscribe/{userId:int}")]
        [Role("*")]
        public IHttpActionResult Subscribe(int userId)
        {
            try
            {
                _subscriptionService.Subscribe ( CurrentUserId, userId );
                return Ok();
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message
                };
                return Json(error);
            }
        }

        //отписаться от userId
        // POST api/<controller>/subscribe
        // userId передовать в строке запроса
        [Route("unsubscribe/{userId:int}")]
        [Role("*")]
        public IHttpActionResult Unsubscribe(int userId)
        {
            try
            {
                _subscriptionService.Unsubscribe(CurrentUserId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Message = ex.Message
                };
                return Json(error);
            }
        }

    }
}