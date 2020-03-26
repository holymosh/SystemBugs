using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;

namespace DbAccess
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task CreateAsync(TEntity entity);
        Task<TEntity> FindById(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate);
        Task RemoveAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}