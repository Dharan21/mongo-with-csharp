using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDemo.Models;
using MongoDemo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDemo.Repositories
{
    public class ContractTypeRepository : GenericRepository<ContractType>, IContractTypeRepository
    {
        public ContractTypeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IList<ContractType>> GetAllActive()
        {
            return await Task.Run(() => this.collection.AsQueryable().Where(x => x.IsActive).OrderBy(x => x.Name).ToList());
        }
    }
}
