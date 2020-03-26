using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DbAccess
{
    public class EfGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EfGenericRepository(DomainDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }


        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> FindById(int id)
        {
            var result = await _dbSet.FindAsync(id);
            return result;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}