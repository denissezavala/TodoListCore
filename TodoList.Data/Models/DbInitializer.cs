using System.Linq;

namespace TodoList.Data.Models 
{
    public static class DbInitializer
    {
        public static void Initialize(TodoListContext context)
        {
            context.Database.EnsureCreated();

            if (context.Todos.Any()) 
            {
                return;
            }

            var todos = new [] 
            {
                new Todo("Buy milk"),
                new Todo("Get stamps")
            };

            foreach (var todo in todos)
            {
                context.Todos.Add(todo);
            }
            
            context.SaveChanges();
        }
    }

}