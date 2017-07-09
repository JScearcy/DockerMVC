using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DockerWebAPI.Services;
using DockerWebAPI.Models;

namespace DockerWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Todo")]
    public class TodoController : Controller
    {
        private readonly ITodoStoreService _storeService;

        public TodoController(ITodoStoreService storeService)
        {
            _storeService = storeService;
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<IEnumerable<Todo>> Get()
        {
            IEnumerable<Todo> todos = await _storeService.Get();
            return todos;
        }

        // GET: api/Todo/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Todo> Get(int id)
        {
            Todo todo = await _storeService.Get(id);
            return todo;
        }
        
        // POST: api/Todo
        [HttpPost]
        public async Task<IEnumerable<Todo>> Post([FromBody]Todo value)
        {
            IEnumerable<Todo> todos = await _storeService.Add(value);
            return todos;
        }
        
        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IEnumerable<Todo>> Put(int id, [FromBody]Todo value)
        {
            IEnumerable<Todo> todos = await _storeService.Update(value);
            return todos;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IEnumerable<Todo>> Delete(int id)
        {
            IEnumerable<Todo> todos = await _storeService.Delete(id);
            return todos;
        }
    }
}
