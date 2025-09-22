using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcApp.Models;
using MvcApp.Interface;

namespace MvcApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepo;

        public CoursesController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepo.GetAllAsync();
            return View(courses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var course = await _courseRepo.GetByIdAsync(id.Value);
            if (course == null) return NotFound();

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,StudentsEnrolled")] Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseRepo.AddAsync(course);
                await _courseRepo.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var course = await _courseRepo.GetByIdAsync(id.Value);
            if (course == null) return NotFound();

            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseName,StudentsEnrolled")] Course course)
        {
            if (id != course.CourseId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _courseRepo.Update(course);
                    await _courseRepo.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _courseRepo.GetByIdAsync(course.CourseId) == null)
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var course = await _courseRepo.GetByIdAsync(id.Value);
            if (course == null) return NotFound();

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course != null)
            {
                _courseRepo.Delete(course);
                await _courseRepo.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
