using System;
using StudentApi.Models;

public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
}

public class CreateStudentDto
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public int DepartmentId { get; set; }
}

public class UpdateStudentDto
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public int DepartmentId { get; set; }
}

public class StudentDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Department { get; set; } = string.Empty;
}
