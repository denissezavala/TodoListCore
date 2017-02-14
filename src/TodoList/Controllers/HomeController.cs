using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        TodoRepository repository = new TodoRepository();

        public IActionResult Index()
        {
            var todos = repository.GetAll();
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
