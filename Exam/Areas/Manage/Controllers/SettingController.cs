using Exam.DAL;
using Exam.Models;
using Exam.ViewModels.Setting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    [Authorize]
    public class SettingController : Controller
    {
        AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Settings);
        }
         public IActionResult Update()
        {
            SettingVM setting = new SettingVM()
            {
                Twitter = _context.Settings.FirstOrDefault(s => s.Key == "Twitter").Value,
                Facebook = _context.Settings.FirstOrDefault(s => s.Key == "Facebook").Value,
                Linkedin = _context.Settings.FirstOrDefault(s => s.Key == "Linkedin").Value,
            };
            return View(setting);
        }

        [HttpPost]
         public IActionResult Update(SettingVM setting)
        {
           _context.Settings.FirstOrDefault(s => s.Key == "Twitter").Value = setting.Twitter;
            _context.Settings.FirstOrDefault(s => s.Key == "Facebook").Value = setting.Facebook;
           _context.Settings.FirstOrDefault(s => s.Key == "Linkedin").Value =setting.Linkedin;
           
            _context.SaveChanges(); 
            return RedirectToAction(nameof(Index));
        }

    }
}
