using System.Threading.Tasks;
using System.Collections.Generic;
using TodoList.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Data
{

    public interface ITodoRepository {
        Task<IEnumerable<Todo>> GetAll();
    }

    public class TodoRepository : ITodoRepository
    {
        private readonly TodoListContext _dbContext;
        private readonly ILogger _logger;

        public TodoRepository(TodoListContext dbContext, ILogger<TodoRepository> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Todo>> GetAll() {
            _logger.LogInformation("Getting all todo items");
            
            return await _dbContext.Todos.ToListAsync();
        }
    }
}
