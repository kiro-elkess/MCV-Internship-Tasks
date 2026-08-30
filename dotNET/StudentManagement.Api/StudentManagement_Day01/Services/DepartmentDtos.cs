namespace StudentApi.Services
{
    public class DepartmentStatisticsDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int NumberOfStudents { get; set; }
        public double AverageAge { get; set; }
        public int OldestAge { get; set; }
        public int YoungestAge { get; set; }
    }

    public class HighestLowestDto
    {
        public string DepartmentWithHighestStudents { get; set; } = string.Empty;
        public string DepartmentWithLowestStudents { get; set; } = string.Empty;
    }
}
