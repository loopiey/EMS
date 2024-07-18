using Microsoft.AspNetCore.Mvc;
using EMS.API.Data;
using EMS.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly EmsContext _context;

        public CoursesController(EmsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<Course>> CreateCourse(Course course)
        {
            // Get the authenticated teacher's ID
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
            course.TeacherId = teacherId;

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> EditCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            // Ensure the course belongs to the authenticated teacher
            var teacherId = int.Parse(User.FindFirst(ClaimTypes.Name)?.Value);
            var existingCourse = await _context.Courses.FindAsync(id);

            if (existingCourse == null || existingCourse.TeacherId != teacherId)
            {
                return Forbid();
            }

            existingCourse.Name = course.Name;
            existingCourse.Explanation = course.Explanation;
            existingCourse.IsMandatory = course.IsMandatory;
            existingCourse.Credit = course.Credit;

            _context.Entry(existingCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }

}
