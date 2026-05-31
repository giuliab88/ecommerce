using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SampleCommerce.Context;
using System.Linq.Expressions;

namespace SampleCommerce.Repositories
{
    public abstract class BaseRepo<TEntity, TId> where TEntity : class
    {
        protected readonly EcommerceDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepo(EcommerceDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual bool Create(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                return _context.SaveChanges() > 0;
            }
            catch { throw; }
        }

        public virtual List<TEntity> GetAll() => _dbSet.AsNoTracking().ToList();

        public virtual TEntity? GetById(TId id) => _dbSet.Find(id);

        public IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>()
                .Where(predicate)
                .ToList();
        }

        public virtual bool Update(TEntity entity)
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch { throw; }
        }

        public virtual bool Delete(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);
                return _context.SaveChanges() > 0;
            }
            catch { throw; }
        }

        public IDbContextTransaction BeginTransaction()
        {
           return _context.Database.BeginTransaction();
        } 
    }
}
