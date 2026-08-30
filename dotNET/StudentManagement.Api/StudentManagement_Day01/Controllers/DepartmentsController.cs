using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Departments.ToListAsync());
        }

        // GET: api/departments/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return NotFound("Department not found.");
            return Ok(dept);
        }

        // POST: api/departments
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            // Validation: Department name should not be duplicated (Task 14)
            if (await _context.Departments.AnyAsync(d => d.Name.ToLower() == department.Name.ToLower()))
            {
                return BadRequest("Department name already exists.");
            }

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        // PUT: api/departments/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Department department)
        {
            if (id != department.Id) return BadRequest("ID mismatch.");

            // Validation: Duplicate check excluding current ID
            if (await _context.Departments.AnyAsync(d => d.Name.ToLower() == department.Name.ToLower() && d.Id != id))
            {
                return BadRequest("Department name already exists.");
            }

            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Department updated successfully.");
        }

        // DELETE: api/departments/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null) return NotFound("Department not found.");

            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();
            return Ok("Department deleted successfully.");
        }

        // Task 12: Department Statistics
        // GET: api/departments/statistics
        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var stats = await _context.Departments
                .Where(d => d.Students.Any()) // Only calculate for departments that have students
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    StudentCount = d.Students.Count,
                    AverageAge = d.Students.Average(s => s.Age),
                    OldestStudentAge = d.Students.Max(s => s.Age),
                    YoungestStudentAge = d.Students.Min(s => s.Age)
                })
                .ToListAsync();

            return Ok(stats);
        }

        // Task 13: Highest and Lowest Department
        // GET: api/departments/highest-lowest
        [HttpGet("highest-lowest")]
        public async Task<IActionResult> GetHighestAndLowest()
        {
            var deptCounts = await _context.Departments
                .Select(d => new { DepartmentName = d.Name, StudentCount = d.Students.Count })
                .ToListAsync();

            if (!deptCounts.Any()) return NotFound("No departments found.");

            var maxCount = deptCounts.Max(d => d.StudentCount);
            var minCount = deptCounts.Min(d => d.StudentCount);

            var result = new
            {
                Highest = deptCounts.Where(d => d.StudentCount == maxCount).ToList(),
                Lowest = deptCounts.Where(d => d.StudentCount == minCount).ToList()
            };

            return Ok(result);
        }
    }
}
