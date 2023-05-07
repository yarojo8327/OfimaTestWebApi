using Microsoft.EntityFrameworkCore;
using Ofima.TechnicalTest.Infraestructure.Interfaces;
using System.Linq.Expressions;

namespace Ofima.TechnicalTest.Infraestructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DataBaseContext _context;
        internal DbSet<TEntity> dbSet;

        public Repository(DataBaseContext context)
        {
            this._context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return this.dbSet.Where(expression).AsQueryable().AsNoTracking();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            try
            {
                return await this.dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                IQueryable<TEntity> query = this.dbSet.Where(where).AsQueryable().AsNoTracking();
                query = PerformInclusions(includeProperties, query);
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                IQueryable<TEntity> query = this.dbSet.AsQueryable().AsNoTracking();
                query = PerformInclusions(includeProperties, query);
                return query.FirstOrDefault(where);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual TEntity Last()
        {
            try
            {
                return this.dbSet.Last();

            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            try
            {
                IQueryable<TEntity> query = this.dbSet.AsQueryable().AsNoTracking();
                query = PerformInclusions(includeProperties, query);
                return query.LastOrDefault(where);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            try
            {
                return this.dbSet;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = this.dbSet.AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query;
        }

        public virtual async void AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await this.dbSet.AddAsync(entity);

            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                this.dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                this.dbSet.AddRange(entities);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entities)} could not be saved: {ex.Message}");
            }
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                entities.ToList().ForEach(e =>
                {
                    dbSet.Attach(e);
                    _context.Entry(e).State = EntityState.Modified;
                });

            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entities)} could not be saved: {ex.Message}");
            }
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        public virtual async void Delete(int id)
        {
            try
            {
                var entity = await this.dbSet.FindAsync(id);

                if (entity != null)
                {
                    this.dbSet.Remove(entity);
                    _context.Entry(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            try
            {
                entities.ToList().ForEach(e =>
                {
                    this.dbSet.Remove(e);
                    _context.Entry(e).State = EntityState.Deleted;
                });

            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }


        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteEntity(int id)
        {
            try
            {
                var entity = this.dbSet.Find(id);

                if (entity != null)
                {
                    this.dbSet.Remove(entity);
                    _context.Entry(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        private IQueryable<TEntity> PerformInclusions(IEnumerable<Expression<Func<TEntity, object>>> includeProperties,
            IQueryable<TEntity> query)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
