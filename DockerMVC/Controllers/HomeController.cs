using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DockerMVC.Services;
using DockerMVC.Models;

namespace DockerMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDockerWebApiService _apiService;

        public HomeController(IDockerWebApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var todos = await _apiService.GetTodos();
            var viewModel = new TodoViewModel(todos);
            return View(viewModel);
        }

        [HttpDelete]
        [Route("DeleteTodo")]
        public async Task<IEnumerable<Todo>> DeleteTodo([FromQuery]int id)
        {
            var todos = await _apiService.DeleteTodo(id);
            return todos;
        }

        [HttpPut]
        [Route("UpdateTodo")]
        public async Task<IEnumerable<Todo>> UpdateTodo([FromBody]Todo todo)
        {
            var todos = await _apiService.UpdateTodo(todo);
            return todos;
        }

        [HttpPost]
        [Route("AddTodo")]
        public async Task<IEnumerable<Todo>> AddTodo([FromBody]Todo todo)
        {
            var todos = await _apiService.AddTodo(todo);
            return todos;
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
