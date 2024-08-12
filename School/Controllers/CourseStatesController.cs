//using hendi.Models.Entities;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using School.Data;

//namespace School.Controllers
//{
//    public class CourseStatesController : Controller
//    {

//        private readonly ApplicationDbContext _context;
//        public CourseStatesController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            var model = _context.CourseStates.ToList();
//            return View(model);
//        }



//        public IActionResult Create()
//        {
//            return View();
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(CourseState courseState)
//        {

//            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
//            {
//                Console.WriteLine(error.ErrorMessage);
//            }


//            if (ModelState.IsValid)
//            {
//                _context.Add(courseState);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(courseState);
//        }

//        public async Task<IActionResult> Delete(int id)
//        {
//            var courseState = await _context.CourseStates
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (courseState == null)
//            {
//                return NotFound();
//            }
//            _context.CourseStates.Remove(courseState);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}

