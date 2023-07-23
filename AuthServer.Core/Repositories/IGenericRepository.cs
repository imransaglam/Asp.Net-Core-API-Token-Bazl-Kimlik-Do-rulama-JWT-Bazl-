using System.Linq.Expressions;

namespace AuthServer.Core.Repositories
{
    //Entitylerde CRUD İşlemleri için
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id); //id ile getirme
        Task<IEnumerable<TEntity>> GetAllAsync();//listeleme
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>>prediction);//Sorgu yazıp listelemek
        Task AddAsync(TEntity entity);  
        void Remove(TEntity entity);
        TEntity Update(TEntity entity);
    }
}
