using DockerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerWebAPI.Services
{
    public interface ITodoStoreService
    {
        Task<IEnumerable<Todo>> Get();
        Task<Todo> Get(int id);
        Task<IEnumerable<Todo>> Add(Todo todo);
        Task<IEnumerable<Todo>> Update(Todo todo);
        Task<IEnumerable<Todo>> Delete(int id);
    }
}
