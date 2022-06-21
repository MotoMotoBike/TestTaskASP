using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TeledocTestTask.Models;

namespace TeledocTestTask.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        DataBaseContext _context;

        public HomeController(DataBaseContext context)
        {
            _context = context;
        }

        [Route("index")]
        public IActionResult Index()
        {
            return View(_context.clients);
        }
    }
}
