using DockerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace DockerWebAPI.Services
{
    public class TodoStoreService : ITodoStoreService
    {
        private readonly BehaviorSubject<IEnumerable<Todo>> _store;
        private int IdCount = 3;

        public TodoStoreService()
        {
            var initialTodos = new List<Todo>()
            {
                new Todo(1, "Complete homework"),
                new Todo(2, "Submit homework")
            };
            _store = new BehaviorSubject<IEnumerable<Todo>>(initialTodos);
        }

        public Task<IEnumerable<Todo>> Get()
        {
            return Task.Run(() => _store.Value);
        }
        public Task<Todo> Get(int id)
        {
            return Task.Run(() => _store.Value.FirstOrDefault(todo => todo.Id == id));
        }

        public Task<IEnumerable<Todo>> Add(Todo todo)
        {
            return Task.Run(() => {
                List<Todo> todos = _store.Value.ToList();
                todo.Id = IdCount;
                IdCount++;
                todos.Add(todo);
                _store.OnNext(todos);
                return _store.Value;
            });
        }

        public Task<IEnumerable<Todo>> Update(Todo todo)
        {
            return Task.Run(() => {
                IEnumerable<Todo> todos = _store.Value;
                todos = todos.Aggregate(new List<Todo>(), (newTodos, currTodo) =>
                {
                    if (currTodo.Id == todo.Id)
                    {
                        newTodos.Add(todo);
                    } 
                    else
                    {
                        newTodos.Add(currTodo);
                    }
                    return newTodos;
                });
                _store.OnNext(todos);
                return _store.Value;
            });
        }

        public Task<IEnumerable<Todo>> Delete(int id)
        {
            return Task.Run(() =>
            {
                IEnumerable<Todo> todos = _store.Value;

                todos = todos.Where(todo => todo.Id != id);
                _store.OnNext(todos);
                return _store.Value;
            });
        }
    }
}
