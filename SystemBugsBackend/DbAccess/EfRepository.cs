using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DbAccess
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        public DbSet<TEntity> DbSet { get; }

        public EfRepository(DomainDbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            DbSet.Add(entity);
            _context.SaveChanges();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
            _context.SaveChanges();
        }


        public async Task<TEntity> FindById(int id)
        {
            var result = await DbSet.FindAsync(id);
            return result;
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}