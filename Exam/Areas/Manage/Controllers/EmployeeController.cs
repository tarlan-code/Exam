using Exam.DAL;
using Exam.Models;
using Exam.Utilies.Extensions;
using Exam.ViewModels.Employee;
using Exam.ViewModels.Paginate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Exam.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    [Authorize(Roles ="Admin")]
    public class EmployeeController : Controller
    {
        AppDbContext _context;
        IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {

            PaginateVM<Employee> paginet = new PaginateVM<Employee>();
            paginet.MaxPageCount = (int)Math.Ceiling((decimal)_context.Employees.Count() / 5);
            if (paginet.MaxPageCount <= 0) paginet.MaxPageCount = 1;
            paginet.CurrentPage = page;

            if (page > paginet.MaxPageCount || page < 1) return BadRequest();


            paginet.Data = _context.Employees.Skip((page-1)*5).Take(5).Include(p => p.Position);


            return View(paginet);
        }
         public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Employee emp = await _context.Employees.FirstOrDefaultAsync(e=>e.Id == id);
            if(emp is null) return NotFound();

         if(emp.ImgUrl is not null)
            {
                emp.ImgUrl.DeleteFile(_env.WebRootPath, Path.Combine("exam", "assets", "img", "employee"));
            }

            _context.Employees.Remove(emp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
         public async Task<IActionResult> Create()
        {
            ViewBag.Positions = new SelectList(_context.Positions.Where(p => p.Name != "Default"), nameof(Position.Id), nameof(Position.Name));


            return View();
        }

        [HttpPost]
         public async Task<IActionResult> Create(CreateEmployeeVM emp)
        {

            if(emp is null) return BadRequest();

            IFormFile? img = emp?.Image;
            

            if(img is not null)
            {
                string result = img.CheckValidate("image", 300);
                if (result.Length > 0)
                    ModelState.AddModelError("Image", result);
            }

            IEnumerable<Position> poss =  _context.Positions;

            if (!poss.Any(p => p.Id == emp.PositionId) || poss.FirstOrDefault(p => p.Id == emp.PositionId).Name == "Default") ModelState.AddModelError("PositionId", "Bad position id");

            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions.Where(p => p.Name != "Default"), nameof(Position.Id), nameof(Position.Name));

                return View();
            }


            Employee newEmp = new Employee()
            {
                Name = emp.Name,
                Surname = emp.Surname,
                Facebook = emp.Facebook,
                Linkedin = emp.Linkedin,
                Twitter = emp.Twitter,
                PositionId = emp.PositionId,
                ImgUrl = img.SaveFile(Path.Combine(_env.WebRootPath, "exam", "assets", "img", "employee"))
        };
         
            await _context.AddAsync(newEmp);

           await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


       
        public async Task<IActionResult> Update(int? id)
        {
            if(id is null || id<=0) return BadRequest();    
            Employee emp = await _context.Employees.FirstOrDefaultAsync(e=>e.Id== id);
            if (emp is null) return NotFound();
            ViewBag.Positions = new SelectList(_context.Positions.Where(p => p.Name != "Default"), nameof(Position.Id), nameof(Position.Name));
            UpdateEmployeeVM oldEmp = new UpdateEmployeeVM
            {
                Name = emp.Name,
                Surname = emp.Surname,
                Facebook = emp.Facebook,
                Linkedin = emp.Linkedin,
                Twitter = emp.Twitter,
                PositionId = emp.PositionId,
                ImgUrl = emp.ImgUrl,
            };
            return View(oldEmp);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id,UpdateEmployeeVM emp)
        {
            if(id is null || id<=0 || emp is null) return BadRequest();
            Employee oldEmp = await _context.Employees.FirstOrDefaultAsync(e=>e.Id == id);
            if (oldEmp is null) return NotFound();


            IFormFile? img = emp?.Image;

            if (img is not null)
            {
                string result = img.CheckValidate("image", 300);
                if (result.Length > 0)
                    ModelState.AddModelError("Image", result);
            }


            IEnumerable<Position> poss = _context.Positions;

            if(oldEmp.PositionId != emp.PositionId)
            {
                if (!poss.Any(p => p.Id == emp.PositionId) || poss.FirstOrDefault(p => p.Id == emp.PositionId).Name == "Default") ModelState.AddModelError("PositionId", "Bad position id");
            }


            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions.Where(p=>p.Name != "Default"), nameof(Position.Id), nameof(Position.Name));
                ViewBag.Image = oldEmp.ImgUrl;
                return View();
            }


            oldEmp.Name = emp.Name;
            oldEmp.Surname= emp.Surname;
            oldEmp.PositionId = emp.PositionId;
            oldEmp.Twitter = emp.Twitter;
            oldEmp.Linkedin = emp.Linkedin;
            oldEmp.Facebook = emp.Facebook;
            
            if(img is not null){
                img.SaveFileWithName(Path.Combine(_env.WebRootPath, "exam", "assets", "img", "employee"), oldEmp.ImgUrl);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }




    }
}
