using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDemo.Models;
using MongoDemo.Repositories;
using MongoDemo.UOW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDemo.UOW
{
    public class Uow : IUow
    {
        private Dictionary<string, dynamic> _repositories;
        private IMongoDatabase database;
        private MongoClient client;
        private readonly IConfiguration _configuration;
        private IClientSessionHandle session;

        public Uow(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.GetDatabase();
        }

        public void BeginTransaction()
        {
            session = client.StartSession();
            session.StartTransaction();
        }
        public void Rollback()
        {
            session.AbortTransaction();
        }

        public void Commit()
        {
            session.CommitTransaction();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, dynamic>();
            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
                return (IGenericRepository<TEntity>)_repositories[type];
            var collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
            _repositories.Add(type, new GenericRepository<TEntity>(collection));
            return _repositories[type];
        }

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
    }
}
