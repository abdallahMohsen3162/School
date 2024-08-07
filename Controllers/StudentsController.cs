using hendi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;
using School.ModelViews;
using System.Linq;
using System.Threading.Tasks;

namespace School.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.Students.ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Courses(int id)
        {
            var student = await _context.Students
                .Include(s => s.CourseStudents)
                .ThenInclude(cs => cs.Course)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var allCourses = await _context.Courses.Where(c => c.State != CourseState.Deleted).ToListAsync();
            var takenCourses = student.CourseStudents.Select(cs => cs.Course).ToList();
            var notTakenCourses = allCourses.Except(takenCourses).ToList();

            var viewModel = new studentViewModel
            {
                student = student,
                taken = takenCourses,
                notTaken = notTakenCourses
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollInCourses(int studentId, int[] courseIds)
        {
            var student = await _context.Students
                .Include(s => s.CourseStudents)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses
                .Where(c => courseIds.Contains(c.Id))
                .ToListAsync();

            foreach (var course in courses)
            {
                if (!student.CourseStudents.Any(cs => cs.CourseId == course.Id))
                {
                    student.CourseStudents.Add(new CourseStudent { CourseId = course.Id, StudentId = studentId });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Courses", new { id = studentId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCourses(int studentId, int[] courseIdsToRemove)
        {
            var student = await _context.Students
                .Include(s => s.CourseStudents)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound();
            }

            var coursesToRemove = await _context.CourseStudents
                .Where(cs => courseIdsToRemove.Contains(cs.CourseId) && cs.StudentId == studentId)
                .ToListAsync();

            _context.CourseStudents.RemoveRange(coursesToRemove);
            await _context.SaveChangesAsync();
            return RedirectToAction("Courses", new { id = studentId });
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
