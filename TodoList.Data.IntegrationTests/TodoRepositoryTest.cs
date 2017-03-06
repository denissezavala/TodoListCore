using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TodoList.Data.Models;
using Xunit;

namespace TodoList.Data.IntegrationTests
{
    public class TodoRepositoryTest
    {
        private readonly TodoListContext _context;
        private readonly TodoRepository _repository;

        public TodoRepositoryTest()
        {
            var builder = new DbContextOptionsBuilder<TodoListContext>();
            builder.UseInMemoryDatabase();

            _context = new TodoListContext(builder.Options);

            _repository = new TodoRepository(_context, Substitute.For<ILogger<TodoRepository>>());
        }
        
        [Fact]
        public async Task ReturnsAllTodos()
        {
            // Arrange
            var expected = new[]
            {
                new Todo {Title = "Get the milk!"},
                new Todo {Title = "Mail the letter"}
            };
            _context.Todos.AddRange(expected);
            _context.SaveChanges();

            // Act
            var todos = await _repository.GetAll();

            // Assert
            Assert.Equal(expected, todos);
        }
    }
}