using MongoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDemo.UOW.Interfaces
{
    public interface IUow
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;
    }
}
