using System.Collections.Generic;

namespace PortfolioWebAppV2.Repository
{
    public interface IRepository<TEnt, in TPk> where TEnt: class 
    {
        IEnumerable<TEnt> Get();
        TEnt Get(TPk id);
        void Add(TEnt entity);
        void Remove(TEnt entity);
        bool Update(TEnt entity);

    }
}
