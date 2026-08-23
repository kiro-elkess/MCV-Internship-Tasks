using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // Simple In-Memory List (Static so data persists during runtime)
        private static List<Student> _students = new List<Student>
        {
            new Student { Id = 1, Name = "Ahmed Ali", Age = 20, Department = "Computer Science" },
            new Student { Id = 2, Name = "Sara Mohamed", Age = 22, Department = "Information Systems" },
            new Student { Id = 3, Name = "Omar Hassan", Age = 21, Department = "Computer Science" },
            new Student { Id = 4, Name = "Mona Ibrahim", Age = 23, Department = "Software Engineering" }
        };

        // 1. Get all students
        // GET: api/students
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_students);
        }

        // 2. Get student by ID
        // GET: api/students/1
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound(new { Message = $"Student with ID {id} was not found." });
            }
            return Ok(student);
        }

        // 3. Search students by name (Query String)
        // GET: api/students/search?name=Ahmed
        [HttpGet("search")]
        public IActionResult SearchByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { Message = "Search parameter 'name' is required." });
            }

            var results = _students
                .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(results);
        }

        // 4. Filter students by age (Query String)
        // GET: api/students/filter?minAge=20&maxAge=22
        [HttpGet("filter")]
        public IActionResult FilterByAge([FromQuery] int? minAge, [FromQuery] int? maxAge)
        {
            var query = _students.AsQueryable();

            if (minAge.HasValue)
            {
                query = query.Where(s => s.Age >= minAge.Value);
            }

            if (maxAge.HasValue)
            {
                query = query.Where(s => s.Age <= maxAge.Value);
            }

            return Ok(query.ToList());
        }

        // 5. Add a new student (Request Body)
        // POST: api/students
        [HttpPost]
        public IActionResult Create([FromBody] Student newStudent)
        {
            if (newStudent == null)
            {
                return BadRequest(new { Message = "Invalid student data." });
            }

            // Auto-increment ID logic for in-memory list
            newStudent.Id = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
            _students.Add(newStudent);

            return CreatedAtAction(nameof(GetById), new { id = newStudent.Id }, newStudent);
        }

        // 6. Edit student data (Route + Request Body)
        // PUT: api/students/1
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Student updatedStudent)
        {
            var existingStudent = _students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                return NotFound(new { Message = $"Student with ID {id} was not found." });
            }

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.Department = updatedStudent.Department;

            return Ok(new { Message = "Student updated successfully.", Student = existingStudent });
        }

        // 7. Delete a student (Route)
        // DELETE: api/students/1
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound(new { Message = $"Student with ID {id} was not found." });
            }

            _students.Remove(student);
            return Ok(new { Message = $"Student with ID {id} deleted successfully." });
        }
    }
}