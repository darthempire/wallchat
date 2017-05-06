using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using wallchat.Api.App.Filters;
using wallchat.Api.Models.Errors;
using wallchat.Api.Models.News;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO;
using wallchat.Service.Contracts;

namespace wallchat.Api.Controllers
{
    [ RoutePrefix("api/articles") ]
    public class ArticlesController : ApiController
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET api/<controller>
        [ Role("*") ]
        public IHttpActionResult Get()
        {
            try
            {
                var articles = _articleService.GetAllArticles();
                Mapper.Initialize(
                    cfg => cfg.CreateMap<ArticleDTO, ArticleModel>());
                var viewNews = Mapper.Map<List<ArticleDTO>, List<ArticleModel>>(articles);
                return Json(viewNews);
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json(error);
            }
            catch( Exception ex )
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
        [Role("*")]    
        public async Task<IHttpActionResult> Create(RegisterArticleModel articleModel)
        {
            if( !ModelState.IsValid )
                return BadRequest(ModelState);
            try
            {
                var article = new RegisterArticleDTO
                {
                    Text = articleModel.Text,
                    Header = articleModel.Header,
                    ImageUrl = articleModel.ImageUrl,
                    ShortDescription = articleModel.ShortDescription,
                    UserId = CurrentUserId
                };
                _articleService.Create(article);
                return Ok();
            }
            catch( Exception e )
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<controller>/5
        [Role("*")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var article = _articleService.Find(id);
                Mapper.Initialize(
                    cfg => cfg.CreateMap<ArticleDTO, ArticleModel>());
                var viewArticle = Mapper.Map<ArticleDTO, ArticleModel>(article);
                return Json(viewArticle);
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json(error);
            }
            catch( Exception ex )
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json(error);
            }
        }


        // PUT api/<controller>/5
        [ HttpPut ]
        [ Role("*") ]
        public IHttpActionResult Update(ArticleModel articleModel)
        {
            try
            {
                Mapper.Initialize(
                    cfg => cfg.CreateMap<ArticleModel, ArticleDTO>());
                var viewDto = Mapper.Map<ArticleModel, ArticleDTO>(articleModel);
                viewDto.UserId = CurrentUserId;
                _articleService.Update(viewDto);
                return Ok();
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json(error);
            }
            catch( Exception ex )
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json(error);
            }
        }

        [ HttpDelete ]
        [ Role("*") ]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _articleService.Delete(id, CurrentUserId);
                return Ok();
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json(error);
            }
            catch( Exception ex )
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json(error);
            }
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
    }
}