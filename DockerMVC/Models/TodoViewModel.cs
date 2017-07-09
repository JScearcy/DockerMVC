using System.Collections.Generic;

namespace DockerMVC.Models
{
    public class TodoViewModel
    {
        public IEnumerable<Todo> Todos { get; set; }

        public TodoViewModel(IEnumerable<Todo> todos)
        {
            Todos = todos;
        }
    }
}
