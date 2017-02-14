namespace TodoList.Data.Models
{
    public class Todo
    {
        public Todo()
        {
        }

        public Todo(string title) 
        {
            Title = title;
        }
        public string Title { get; set; }
    }
}
