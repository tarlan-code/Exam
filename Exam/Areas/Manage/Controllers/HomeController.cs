using Microsoft.AspNetCore.Mvc;

namespace Exam.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
