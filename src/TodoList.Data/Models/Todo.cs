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

        public int ID { get; set; }
        public string Title { get; set; }
    }
}
