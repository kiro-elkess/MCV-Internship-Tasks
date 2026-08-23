using Microsoft.AspNetCore.Mvc;
using DepartmentApi.Models;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var departments = _departmentService.GetAll();
            return Ok(departments);
        }

        // GET: api/departments/1
        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var department = _departmentService.GetById(id);
            if (department == null)
            {
                return NotFound(new { Message = $"Department with ID {id} was not found." });
            }
            return Ok(department);
        }

        // POST: api/departments
        [HttpPost]
        public IActionResult Create([FromBody] Department newDepartment)
        {
            if (newDepartment == null)
            {
                return BadRequest(new { Message = "Invalid department data." });
            }

            var created = _departmentService.Create(newDepartment);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/departments/1
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Department updatedDepartment)
        {
            var updated = _departmentService.Update(id, updatedDepartment);
            if (updated == null)
            {
                return NotFound(new { Message = $"Department with ID {id} was not found." });
            }

            return Ok(new { Message = "Department updated successfully.", Department = updated });
        }

        // DELETE: api/departments/1
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = _departmentService.Delete(id);
            if (!deleted)
            {
                return NotFound(new { Message = $"Department with ID {id} was not found." });
            }

            return Ok(new { Message = $"Department with ID {id} deleted successfully." });
        }

        // GET: api/departments/statistics
        [HttpGet("statistics")]
        public IActionResult Statistics()
        {
            var stats = _departmentService.GetStatistics();
            return Ok(stats);
        }

        // GET: api/departments/highest-lowest
        [HttpGet("highest-lowest")]
        public IActionResult HighestLowest()
        {
            var result = _departmentService.GetHighestLowest();
            return Ok(result);
        }
    }
}
