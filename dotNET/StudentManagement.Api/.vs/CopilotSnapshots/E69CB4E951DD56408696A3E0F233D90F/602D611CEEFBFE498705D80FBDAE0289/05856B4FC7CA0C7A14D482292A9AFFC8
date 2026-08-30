using System;
using System.Collections.Generic;
using System.Linq;
using StudentApi.Models;

namespace StudentApi.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAll();
        Student? GetById(int id);
        IEnumerable<Student> SearchByName(string name);
        IEnumerable<Student> FilterByAge(int? minAge, int? maxAge);
        Student Create(Student student);
        Student? Update(int id, Student student);
        bool Delete(int id);
    }

    public class StudentService : IStudentService
    {
        // In-memory students list
        private static readonly List<Student> _students = new List<Student>
        {
            new Student { Id = 1, Name = "Ahmed Ali", Age = 20, DepartmentId = 1 },
            new Student { Id = 2, Name = "Sara Mohamed", Age = 22, DepartmentId = 2 },
            new Student { Id = 3, Name = "Omar Hassan", Age = 21, DepartmentId = 1 },
            new Student { Id = 4, Name = "Mona Ibrahim", Age = 23, DepartmentId = 3 }
        };

        public IEnumerable<Student> GetAll() => _students;

        public Student? GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

        public IEnumerable<Student> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return Enumerable.Empty<Student>();
            return _students.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Student> FilterByAge(int? minAge, int? maxAge)
        {
            var query = _students.AsQueryable();
            if (minAge.HasValue) query = query.Where(s => s.Age >= minAge.Value);
            if (maxAge.HasValue) query = query.Where(s => s.Age <= maxAge.Value);
            return query.ToList();
        }

        public Student Create(Student student)
        {
            student.Id = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
            _students.Add(student);
            return student;
        }

        public Student? Update(int id, Student student)
        {
            var existing = GetById(id);
            if (existing == null) return null;
            existing.Name = student.Name;
            existing.Age = student.Age;
            existing.DepartmentId = student.DepartmentId;
            return existing;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            _students.Remove(existing);
            return true;
        }
    }
}
