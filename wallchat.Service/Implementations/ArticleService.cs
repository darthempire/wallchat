using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NLog;
using wallchat.DAL.App.Contracts;
using wallchat.Helpers.Exceptions;
using wallchat.Model.App.DTO;
using wallchat.Model.App.Entity;
using wallchat.Repository.App.Authorization;
using wallchat.Repository.App.News;
using wallchat.Service.Contracts;

namespace wallchat.Service.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly Logger _logger;
        private readonly IUserRepository _userRepository;
        private readonly INewRepository _newRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArticleService(IUnitOfWork unitOfWork, INewRepository newRepository, IUserRepository userRepository)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _unitOfWork = unitOfWork;
            _newRepository = newRepository;
            _userRepository = userRepository;
        }

        public void Delete(int id, long currentUserId)
        {
            try
            {
                var user = _newRepository.GetAll().FirstOrDefault( p => p.Id == id && p.UserId == currentUserId);
                if (user == null)
                    throw new ServiceException("You don`t have permissions to make this action!");

                _logger.Info("Start deleting article with id " + id);
                _newRepository.Delete(p => p.Id == id);
                _logger.Info("Delete Article with id " + id);
            }
            catch( RepositoryException re )
            {
                throw new ServiceException("Repository ex: " + re.Message);
            }
            catch( Exception ex )
            {
                throw new ServiceException(ex.Message);
            }
        }

        public ArticleDTO Find(int id)
        {
            try
            {
                var article = _newRepository.GetById(id);
                Mapper.Initialize(
                    cfg => cfg.CreateMap<Article, ArticleDTO>());
                var articleDto = Mapper.Map<Article, ArticleDTO>(article);
                _logger.Info("Get Article: id = " + id);
                return articleDto;
            }
            catch( RepositoryException rep )
            {
                _logger.Error("Method: FindArticle ( long id )");
                _logger.Error(rep.Message);
                throw new ServiceException("Service exception: from repository ", rep);
            }
            catch( Exception ex )
            {
                _logger.Error("Method: FindArticle ( long id )", ex);
                throw new ServiceException("Method: FindArticle ( long id )", ex);
            }
        }

        public List<ArticleDTO> GetAllArticles()
        {
            try
            {
                _logger.Info("Start getting all articles");
                var news = _newRepository.GetAll();
                Mapper.Initialize(
                    cfg => cfg.CreateMap<Article, ArticleDTO>());
                var newDto = Mapper.Map<IEnumerable<Article>, List<ArticleDTO>>(news);
                _logger.Info("Get all articles");
                return newDto;
            }
            catch( RepositoryException re )
            {
                throw new ServiceException("Repository ex: " + re.Message);
            }
            catch( Exception ex )
            {
                throw new ServiceException(ex.Message);
            }
        }

        public void Create(RegisterArticleDTO articleDTO)
        {
            var article = new Article();

            if( articleDTO != null )
            {
                article.Text = articleDTO.Text;
                article.ImageUrl = articleDTO.ImageUrl;
                article.Header = articleDTO.Header;
                article.ShortDescription = articleDTO.ShortDescription;
                article.PublishDate = DateTime.Now;
                article.UserId = articleDTO.UserId;
                _newRepository.Add(article);
            }
        }

        public void Update(ArticleDTO articleDto)
        {
            try
            {
                var user = _newRepository.GetAll().FirstOrDefault(p => p.Id == articleDto.Id && p.UserId == articleDto.UserId);
                if (user == null)
                    throw new ServiceException("You don`t have permissions to make this action!");

                _logger.Info("Start update in article with id " + articleDto.Id);
                var article = _newRepository.GetById(articleDto.Id);
                if( article == null )
                    throw new ServiceException("No article with this id");

                Mapper.Initialize(
                    cfg => cfg.CreateMap<ArticleDTO, Article>());
                article.Id = articleDto.Id;
                article.Text = articleDto.Text;
                article.ImageUrl = articleDto.ImageUrl;
                article.Header = articleDto.Header;
                article.ShortDescription = articleDto.ShortDescription;
                article.PublishDate = DateTime.Now;
                _newRepository.Update(article);
                _logger.Info("Update article with id " + article.Id);
            }
            catch( RepositoryException re )
            {
                throw new ServiceException("Repositiry ex: " + re.Message);
            }
            catch( Exception ex )
            {
                throw new ServiceException(ex.Message);
            }
        }
    }
}