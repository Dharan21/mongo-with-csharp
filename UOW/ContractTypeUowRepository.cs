using MongoDemo.Models;
using MongoDemo.UOW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDemo.UOW
{
    public class ContractTypeUowRepository : IContractTypeUowRepository
    {
        private readonly IUow uow;

        public ContractTypeUowRepository(IUow uow)
        {
            this.uow = uow;
        }
        public async Task Delete(Guid id)
        {
            await this.uow.Repository<ContractType>().Delete(id);
        }

        public async Task<IList<ContractType>> GetAll()
        {
            return await this.uow.Repository<ContractType>().GetAll();
        }

        public async Task<IList<ContractType>> GetAllActive()
        {
            return (await this.uow.Repository<ContractType>().FindAll(x => x.IsActive)).OrderBy(x => x.Name).ToList();
        }

        public async Task<ContractType> GetById(Guid id)
        {
            return await this.uow.Repository<ContractType>().GetById(id);
        }

        public async Task<ContractType> GetByName(string name)
        {
            return await this.uow.Repository<ContractType>().FindOne(x => x.Name == name);
        }

        public async Task Insert(ContractType entity)
        {
            await this.uow.Repository<ContractType>().Insert(entity);
        }

        public async Task Update(ContractType entity)
        {
            this.uow.BeginTransaction();
            try
            {
                await this.uow.Repository<ContractType>().Update(entity);
                this.uow.Commit();
            }
            catch (Exception)
            {
                this.uow.Rollback();
            }
        }
    }
}
