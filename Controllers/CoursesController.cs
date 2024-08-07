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

            //Console.WriteLine("HELLO");
            var courses = await _context.Courses.OrderBy(c => c.Sort).ToListAsync();
            
            return View(courses);
        }

        public IActionResult Create()
        {
            ViewBag.CourseStates = new SelectList(Enum.GetValues(typeof(CourseState)).Cast<CourseState>().Select(s => new { Value = s, Text = s.ToString() }), "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            Console.WriteLine("HELLO from post");
            //foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //{
            //    Console.WriteLine(error.ErrorMessage);
            //}

            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CourseStates = new SelectList(Enum.GetValues(typeof(CourseState)).Cast<CourseState>().Select(s => new { Value = s, Text = s.ToString() }), "Value", "Text", course.State);
            return View(course);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            course.State = CourseState.Deleted;
            //_context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            ViewBag.CourseStates = new SelectList(Enum.GetValues(typeof(CourseState)).Cast<CourseState>().Select(s => new { Value = s, Text = s.ToString() }), "Value", "Text", course.State);
            return View(course);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {

            if (ModelState.IsValid)
            {
                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }


    }
}
