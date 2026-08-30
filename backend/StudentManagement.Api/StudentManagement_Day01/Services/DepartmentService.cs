using DepartmentApi.Models;
using StudentApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        // In-memory departments list
        private static readonly List<Department> _departments = new List<Department>
        {
            new Department { Id = 1, Name = "Computer Science" },
            new Department { Id = 2, Name = "Mathematics" },
            new Department { Id = 3, Name = "Physics" }
        };

        // We depend on IStudentService to get current students for statistics
        private readonly IStudentService _studentService;

        public DepartmentService(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IEnumerable<Department> GetAll() => _departments;

        public Department? GetById(int id) => _departments.FirstOrDefault(d => d.Id == id);

        public Department Create(Department department)
        {
            department.Id = _departments.Any() ? _departments.Max(d => d.Id) + 1 : 1;
            _departments.Add(department);
            return department;
        }

        public Department? Update(int id, Department department)
        {
            var existing = GetById(id);
            if (existing == null) return null;
            existing.Name = department.Name;
            return existing;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            _departments.Remove(existing);
            return true;
        }

        public IEnumerable<DepartmentStatisticsDto> GetStatistics()
        {
            var students = _studentService?.GetAll() ?? Enumerable.Empty<Student>();

            var stats = _departments.Select(d =>
            {
                var deptStudents = students.Where(s => s.DepartmentId == d.Id).ToList();
                return new DepartmentStatisticsDto
                {
                    DepartmentName = d.Name,
                    NumberOfStudents = deptStudents.Count,
                    AverageAge = deptStudents.Any() ? deptStudents.Average(s => s.Age) : 0,
                    OldestAge = deptStudents.Any() ? deptStudents.Max(s => s.Age) : 0,
                    YoungestAge = deptStudents.Any() ? deptStudents.Min(s => s.Age) : 0
                };
            });

            return stats;
        }

        public HighestLowestDto GetHighestLowest()
        {
            var stats = GetStatistics().ToList();
            if (!stats.Any()) return new HighestLowestDto();

            var highest = stats.OrderByDescending(s => s.NumberOfStudents).First();
            var lowest = stats.OrderBy(s => s.NumberOfStudents).First();

            return new HighestLowestDto
            {
                DepartmentWithHighestStudents = highest.DepartmentName,
                DepartmentWithLowestStudents = lowest.DepartmentName
            };
        }
    }
}
