using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using DepartmentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;

        public StudentsController(IStudentService studentService, IDepartmentService departmentService)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
        }

        // 1. Get all students
        // GET: api/students
        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _studentService.GetAll();
            return Ok(students);
        }

        // 2. Get student by ID
        // GET: api/students/1
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var student = _studentService.GetById(id);
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

            var results = _studentService.SearchByName(name);
            return Ok(results);
        }

        // 4. Filter students by age (Query String)
        // GET: api/students/filter?minAge=20&maxAge=22
        [HttpGet("filter")]
        public IActionResult FilterByAge([FromQuery] int? minAge, [FromQuery] int? maxAge)
        {
            var results = _studentService.FilterByAge(minAge, maxAge);
            return Ok(results);
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

            // Validate department exists before adding
            var dept = _departmentService.GetById(newStudent.DepartmentId);
            if (dept == null)
            {
                return BadRequest(new { Message = $"Department with ID {newStudent.DepartmentId} does not exist." });
            }

            var created = _studentService.Create(newStudent);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // 6. Edit student data (Route + Request Body)
        // PUT: api/students/1
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null)
            {
                return BadRequest(new { Message = "Invalid student data." });
            }

            // Validate department exists before updating
            var dept = _departmentService.GetById(updatedStudent.DepartmentId);
            if (dept == null)
            {
                return BadRequest(new { Message = $"Department with ID {updatedStudent.DepartmentId} does not exist." });
            }

            var updated = _studentService.Update(id, updatedStudent);
            if (updated == null)
            {
                return NotFound(new { Message = $"Student with ID {id} was not found." });
            }

            return Ok(new { Message = "Student updated successfully.", Student = updated });
        }

        // 7. Delete a student (Route)
        // DELETE: api/students/1
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = _studentService.Delete(id);
            if (!deleted)
            {
                return NotFound(new { Message = $"Student with ID {id} was not found." });
            }

            return Ok(new { Message = $"Student with ID {id} deleted successfully." });
        }
    }
}
