using MongoDB.Bson;
using MongoDB.Driver;
using MongoDemo.Models;
using MongoDemo.UOW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDemo.UOW
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        private IMongoCollection<TEntity> collection;
        public GenericRepository()
        { }

        public GenericRepository(IMongoCollection<TEntity> collection)
        {
            this.collection = collection;
        }

        public async Task Insert(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            await collection.InsertOneAsync(entity);
        }

        public async Task Update(TEntity entity)
        {
            await collection.ReplaceOneAsync(
                new BsonDocument("_id", entity.Id),
                entity,
                new ReplaceOptions { IsUpsert = true }
            );
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            await collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id));
        }

        public async Task<IList<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => collection
            .AsQueryable<TEntity>()
            .Where(predicate.Compile())
            .ToList());
        }

        public async Task<TEntity> FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => collection
            .AsQueryable<TEntity>()
            .Where(predicate.Compile())
            .ToList().FirstOrDefault());
        }

        public async Task<IList<TEntity>> GetAll()
        {
            return await Task.Run(() => collection.AsQueryable<TEntity>().ToList());
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await Task.Run(() => collection.AsQueryable().Where(x => x.Id == id).FirstOrDefault());
        }
    }
}
