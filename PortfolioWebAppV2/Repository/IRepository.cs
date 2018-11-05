using System.Collections.Generic;

namespace PortfolioWebAppV2.Repository
{
    public interface IRepository<TEnt, in TPk> where TEnt: class 
    {
        IEnumerable<TEnt> GetAll();
        TEnt Get(TPk id);
        bool Add(TEnt entity);
        bool Remove(TEnt entity);
        bool Update(TEnt entity);

    }
}
