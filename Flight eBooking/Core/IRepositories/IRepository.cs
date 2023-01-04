using System.Linq.Expressions;

namespace Flight_eBooking.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> GetAll(
            Expression<Func<TEntity, bool>> expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<String> includes = null);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> expression, List<string> includes = null);   

        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        Task Remove(int id);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    }
}
