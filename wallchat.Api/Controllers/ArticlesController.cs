using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using wallchat.Api.Models.Errors;
using wallchat.Api.Models.News;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO;
using wallchat.Service.Contracts;

namespace wallchat.Api.Controllers
{
    [ RoutePrefix ( "api/articles" ) ]
    public class ArticlesController : ApiController
    {
        private readonly IArticleService _articleService;

        public ArticlesController ( IArticleService articleService )
        {
            _articleService = articleService;
        }

        // GET api/<controller>

        public IHttpActionResult Get()
        {
            try
            {
                var articles = _articleService.GetAllArticles( );
                Mapper.Initialize (
                    cfg => cfg.CreateMap<ArticleDTO, ArticleModel>( ));
                var viewNews = Mapper.Map<List<ArticleDTO>, List<ArticleModel>> (articles);
                return Json (viewNews);
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json (error);
            }
            catch( Exception ex )
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json (error);
            }
        }


        [ AllowAnonymous ]
        public async Task<IHttpActionResult> Create ( RegisterArticleModel articleModel )
        {
            if( !ModelState.IsValid )
                return BadRequest (ModelState);
            try
            {
                var article = new RegisterArticleDTO
                {
                    Text = articleModel.Text,
                    Header = articleModel.Header,
                    ImageUrl = articleModel.ImageUrl,
                    ShortDescription = articleModel.ShortDescription
                };
                _articleService.Create (article);
                return Ok( );
            }
            catch( Exception e )
            {
                return BadRequest (e.Message);
            }
        }

        // GET api/<controller>/5
        public IHttpActionResult Get ( int id )
        {
            try
            {
                var article = _articleService.Find (id);
                Mapper.Initialize (
                    cfg => cfg.CreateMap<ArticleDTO, ArticleModel>( ));
                var viewArticle = Mapper.Map<ArticleDTO, ArticleModel> (article);
                return Json (viewArticle);
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json (error);
            }
            catch( Exception ex )
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json (error);
            }
        }


        // PUT api/<controller>/5
        [ HttpPut ]
        public IHttpActionResult Update ( ArticleModel articleModel )
        {
            try
            {
                Mapper.Initialize (
                    cfg => cfg.CreateMap<ArticleModel, ArticleDTO>( ));
                var viewDto = Mapper.Map<ArticleModel, ArticleDTO> (articleModel);
                _articleService.Update (viewDto);
                return Ok( );
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json (error);
            }
            catch( Exception ex )
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json (error);
            }
        }

        [ HttpDelete ]
        public IHttpActionResult Delete ( int id )
        {
            try
            {
                _articleService.Delete (id);
                return Ok( );
            }
            catch( ServiceException se )
            {
                var error = new Error
                {
                    Message = se.Message,
                    Code = 12
                };
                return Json (error);
            }
            catch( Exception ex )
            {
                var error = new Error
                {
                    Message = ex.Message,
                    Code = 12
                };
                return Json (error);
            }
        }
    }
}