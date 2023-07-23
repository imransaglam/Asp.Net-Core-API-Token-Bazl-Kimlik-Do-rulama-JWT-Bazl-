using SharedLibrary.Dtos;
using System.Linq.Expressions;

namespace AuthServer.Core.Services
{
    public interface IServiceGeneric<TEntity,TDto > where TEntity : class where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(int id); //id ile getirme
        Task<Response<IEnumerable<TDto>>> GetAllAsync();//listeleme
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> prediction);//Sorgu yazıp listelemek
        Task<Response<TDto>> AddAsync(TEntity entity);
        Task<Response<NoDataDto>> Remove(TEntity entity);//silinen data clienta dönmesinin bir anlamı yok o yüzden NoDataDto alıyor ve içinde herhangi bir property tanımlı değil
        Task<Response<NoDataDto>> Update(TEntity entity);
    }
}
