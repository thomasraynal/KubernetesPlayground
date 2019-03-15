using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KubernetesPlayground.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace KubernetesPlayground.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private ITestRepository _repository;

        public ItemsController(ITestRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDocument>>> Get()
        {
            return await _repository.GetAllAsync<ItemDocument>(_ => true);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDocument>> Get(Guid id)
        {
            return await _repository.GetOneAsync<ItemDocument>(item => item.Id == id);
        }

        [HttpPut("{id}")]
        public async Task Put(ItemDocument value)
        {
             await _repository.AddOneAsync(value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _repository.DeleteOneAsync<ItemDocument>(item=> item.Id ==  id);
        }
    }
}
