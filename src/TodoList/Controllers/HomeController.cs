using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Data;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private ITodoRepository _repository;

        public HomeController(ITodoRepository todoRepository)
        {
            _repository = todoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var todos = await _repository.GetAll();
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
