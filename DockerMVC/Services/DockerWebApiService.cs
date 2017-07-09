using DockerMVC.Enums;
using DockerMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DockerMVC.Services
{
    public class DockerWebApiService : IDockerWebApiService
    {
        private readonly string baseUrl = "http://dockerwebapi";
        
        public async Task<IEnumerable<Todo>> GetTodos()
        {
            List<Todo> deserializedTodos = await WebApiRequest<List<Todo>>(RequestType.GetTodos);
            return deserializedTodos;
        }

        public async Task<Todo> GetTodo(int id)
        {
            Todo deserializedTodo = await WebApiRequest<Todo>(RequestType.GetTodo, id);
            return deserializedTodo;
        }

        public async Task<IEnumerable<Todo>> UpdateTodo(Todo todo)
        {
            List<Todo> deserializedTodos = await WebApiRequest<List<Todo>>(RequestType.UpdateTodo, todo);
            return deserializedTodos;

        }

        public async Task<IEnumerable<Todo>> AddTodo(Todo todo)
        {
            List<Todo> deserializedTodos = await WebApiRequest<List<Todo>>(RequestType.AddTodo, todo);
            return deserializedTodos;
        }

        public async Task<IEnumerable<Todo>> DeleteTodo(int id)
        {
            List<Todo> deserializedTodos = await WebApiRequest<List<Todo>>(RequestType.DeleteTodo, id);
            return deserializedTodos;
        }

        private async Task<T> WebApiRequest<T>(RequestType requestType, object data = null) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = null;
                switch (requestType)
                {
                    case RequestType.AddTodo:
                        response = await client.PostAsync("api/Todo", SerializeRequestData(data));
                        break;
                    case RequestType.DeleteTodo:
                        response = await client.DeleteAsync($"api/Todo/{data}");
                        break;
                    case RequestType.GetTodo:
                        response = await client.GetAsync($"api/Todo/{data}");
                        break;
                    case RequestType.GetTodos:
                        response = await client.GetAsync("api/Todo");
                        break;
                    case RequestType.UpdateTodo:
                        var todo = (Todo)data;
                        response = await client.PutAsync($"api/Todo/{todo.Id}", SerializeRequestData(data));
                        break;
                }
                string responseString = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(responseString);
            }
        }

        private HttpContent SerializeRequestData(object data)
        {
            var httpData = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(httpData);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }
    }
}
