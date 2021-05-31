using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDemo.Models;
using MongoDemo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractTypeController : ControllerBase
    {
        private readonly IContractTypeRepository _repository;

        public ContractTypeController(IContractTypeRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var contractTypes = await this._repository.GetAll();
            return Ok(contractTypes);
        }

        [HttpGet]
        [Route("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var contractTypes = await this._repository.GetAllActive();
            return Ok(contractTypes);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var contractType = await this._repository.GetById(new Guid(id));
            return Ok(contractType);
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public async Task<IActionResult> GetbyName(string name)
        {
            var contractType = await this._repository.FindOne(x => x.Name == name);
            return Ok(contractType);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(ContractType contractType)
        {
            await this._repository.Insert(contractType);
            return Ok(new { status = true });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(ContractType contractType)
        {
            await this._repository.Update(contractType);
            return Ok(new { status = true });
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            await this._repository.Delete(new Guid(id));
            return Ok(new { status = true });
        }
    }
}
