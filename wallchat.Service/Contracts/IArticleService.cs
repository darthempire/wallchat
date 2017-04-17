using System.Collections.Generic;
using wallchat.Model.App.DTO;

namespace wallchat.Service.Contracts
{
    public interface IArticleService
    {
        ArticleDTO Find ( long id );
        void Update ( ArticleDTO articleDto );
        void Delete ( long id );
        List<ArticleDTO> GetAllArticles ();
        void Create(RegisterArticleDTO article);
    }
}