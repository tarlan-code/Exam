using Exam.DAL;
using Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Areas.Manage.Controllers
{
    [Area(nameof(Manage))]
    public class PositionController : Controller
    {
        AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<IActionResult> Index()
        {
            return View(_context.Positions.Where(p=>p.Name != "Default"));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null || id <=0)return BadRequest();
            Position pos = await _context.Positions.FirstOrDefaultAsync(p=>p.Id== id);
            if (pos is null) return NotFound();

            int defid =  _context.Positions.FirstOrDefaultAsync(p => p.Name == "Default").Id;

            foreach (var item in _context.Employees.Where(e=>e.PositionId == id))
            {
                item.PositionId = defid;
            }

            _context.Positions.Remove(pos); 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(Position pos)
        {
            if(pos is null) return BadRequest();

            if (_context.Positions.Any(p => p.Name == pos.Name) || pos.Name == "Default") ModelState.AddModelError("Name", "Name is unique");
            if (!ModelState.IsValid) return View();

            await _context.Positions.AddAsync(pos);
            _context.SaveChangesAsync();    
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id<=0) return BadRequest();   
            Position pos = await _context.Positions.FirstOrDefaultAsync(p=>p.Id == id);
            if(pos is null || pos.Name == "Default") return NotFound();

            return View(pos);
        }
        [HttpPost]

        public async Task<IActionResult>  Update(int? id, Position pos)
        {
            if (id is null || id <= 0) return BadRequest();
            Position oldPos = await _context.Positions.FirstOrDefaultAsync(p=> p.Id == id);
            if(oldPos is null || oldPos.Name == "Default") return NotFound();
            if (_context.Positions.Any(p => p.Name == pos.Name) && pos.Name != oldPos.Name) ModelState.AddModelError("Name", "Name is unique");
            if (pos.Name == "Default") ModelState.AddModelError("Name", "Default is disabled");
            if (!ModelState.IsValid) return View();
            oldPos.Name = pos.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
