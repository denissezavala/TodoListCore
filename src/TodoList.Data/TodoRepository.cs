using System.Collections.Generic;
using TodoList.Data.Models;

namespace TodoList.Data
{
    public class TodoRepository
    {
        public IEnumerable<Todo> GetAll() {
            return new List<Todo>() {
                new Todo("Get milk"),
                new Todo("Pay rent"),
            };
        }
    }
}
