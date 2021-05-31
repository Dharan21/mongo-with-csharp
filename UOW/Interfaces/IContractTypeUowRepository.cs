using MongoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDemo.UOW.Interfaces
{
    public interface IContractTypeUowRepository 
    {
        Task<IList<ContractType>> GetAllActive();
        Task Insert(ContractType entity);
        Task Update(ContractType entity);
        Task Delete(Guid id);
        Task<IList<ContractType>> GetAll();
        Task<ContractType> GetById(Guid id);
        Task<ContractType> GetByName(string name);
    }
}
