using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DbAccess
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task CreateAsync(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);
        Task<TEntity> FindById(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
        Task RemoveAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}