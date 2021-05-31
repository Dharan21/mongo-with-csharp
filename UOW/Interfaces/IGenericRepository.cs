using MongoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDemo.UOW.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Guid id);
        Task<IList<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindOne(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
    }
}
