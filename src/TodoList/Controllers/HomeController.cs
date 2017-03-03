using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Data.Models;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITodoRepository _repository;

        public HomeController()
        {
//            _repository = todoRepository;
        }

        public IActionResult Index()
        {
//            var todos = await _repository.GetAll();
            var todos = new[] {new Todo {Title = "Hello"}};
            return View(todos);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
