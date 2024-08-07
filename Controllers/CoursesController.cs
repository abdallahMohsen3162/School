using hendi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Data;

namespace School.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //sorted by Sort
            var courses = (_context.Courses.OrderBy(c => c.Sort).Include(c => c.CourseState)).ToList();
            return View(courses);
        }

        public IActionResult Create()
        {
            ViewBag.CourseStates = new SelectList(_context.CourseStates, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
  
            return View(course);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
       
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Console.WriteLine("edit");

            var course = await _context.Courses.FindAsync(id);


            ViewBag.CourseStates = new SelectList(_context.CourseStates, "Id", "Name", course.StateId);
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Sort,StateId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                _context.Update(course);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CourseStates = new SelectList(_context.CourseStates, "Id", "Name", course.StateId);
            return View(course);
        }

    }
}
