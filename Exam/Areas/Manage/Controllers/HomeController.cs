using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    [Authorize(Roles = "Admin")]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
