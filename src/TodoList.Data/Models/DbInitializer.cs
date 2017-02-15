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

            var todos = new Todo[] 
            {
                new Todo("Buy Milk"),
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