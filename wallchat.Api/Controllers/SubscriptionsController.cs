using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wallchat.Api.Models.Errors;
using wallchat.Api.Models.User;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO.Users;
using wallchat.Service.Contracts;

namespace wallchat.Api.Controllers
{
    public class SubscriptionsController : ApiController
    {
        private readonly ISubscriberService _subscriptionService;

        public SubscriptionsController(ISubscriberService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }
        // GET api/<controller>
        public IHttpActionResult Get()
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

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var subscribers = _subscriptionService.GetAll();
                //Mapper.Initialize(
                //    cfg => cfg.CreateMap<SubscriberDTO, SubscriberModel>());
                //var viewSubscribers = Mapper.Map<List<SubscriberDTO>, List<SubscriberModel>>(users);
                return Json("");
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

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}