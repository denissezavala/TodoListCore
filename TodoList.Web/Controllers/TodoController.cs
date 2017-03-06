using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data;
using TodoList.Data.Models;

namespace TodoList.Web.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Todo>> Get()
        {
            return await _todoRepository.GetAll();
        }

        [HttpPost]
        public async Task<HttpStatusCode> Create([FromBody] Todo todo)
        {
            await _todoRepository.Add(todo);
            return HttpStatusCode.OK;
        }

    }
}
