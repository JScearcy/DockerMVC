using DockerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerMVC.Services
{
    public interface IDockerWebApiService
    {
        Task<IEnumerable<Todo>> GetTodos();
        Task<Todo> GetTodo(int id);
        Task<IEnumerable<Todo>> UpdateTodo(Todo todo);
        Task<IEnumerable<Todo>> AddTodo(Todo todo);
        Task<IEnumerable<Todo>> DeleteTodo(int id);
    }
}
