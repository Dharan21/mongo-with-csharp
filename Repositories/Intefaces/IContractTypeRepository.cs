using MongoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDemo.Repositories.Interfaces
{
    public interface IContractTypeRepository : IGenericRepository<ContractType>
    {
        Task<IList<ContractType>> GetAllActive();
    }
}
