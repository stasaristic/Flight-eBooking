using Flight_eBooking.Areas.Identity.Data;
using Flight_eBooking.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Flight_eBooking.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected DbSet<TEntity> _db;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            await _db.AddRangeAsync(entities);
        }       

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> expression, List<string> includes = null)
        {
            IQueryable<TEntity> query = _db;
            
            if (includes != null) 
            {
                foreach (var includeProprety in includes)
                {
                    query = query.Include(includeProprety);
                }
            }


            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<string> includes = null)
        {
            IQueryable<TEntity> query = _db;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includes != null)
            {
                foreach (var includeProprety in includes)
                {
                    query = query.Include(includeProprety);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Remove(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _db.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
