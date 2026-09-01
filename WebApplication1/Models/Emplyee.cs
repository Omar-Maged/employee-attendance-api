namespace WebApplication1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Department { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public DateOnly JoinDate { get; set; }

        public List<AttendanceRecord> AttendanceRecords { get; set; } = new();
    }
}
