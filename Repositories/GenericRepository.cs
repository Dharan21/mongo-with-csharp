using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDemo.Models;
using MongoDemo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDemo.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        private IMongoDatabase database;
        protected MongoClient client;
        protected IMongoCollection<TEntity> collection;
        private readonly IConfiguration _configuration;

        public GenericRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            GetDatabase();
            GetCollection();
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
            await collection .DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id));
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

        #region Private Methods  
        private void GetDatabase()
        {
            client = new MongoClient(GetConnectionString());
            database = client.GetDatabase(GetDatabaseName());
        }

        private string GetConnectionString()
        {
            return this._configuration
            .GetValue<string>("MongoDb:ConnectionString")
            .Replace("{ DB_NAME}", GetDatabaseName());
        }

        private string GetDatabaseName()
        {
            return this._configuration
            .GetValue<string>("MongoDb:DatabaseName");
        }

        private void GetCollection()
        {
            collection = database
            .GetCollection<TEntity>(typeof(TEntity).Name);
        }
        #endregion
    }
}
