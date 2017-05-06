using System.Collections.Generic;
using wallchat.Model.App.DTO;

namespace wallchat.Service.Contracts
{
    public interface IArticleService
    {
        ArticleDTO Find ( int id );
        void Update ( ArticleDTO articleDto );
        void Delete (int id, long currentUserId );
        List<ArticleDTO> GetAllArticles ();
        void Create(RegisterArticleDTO article);
    }
}