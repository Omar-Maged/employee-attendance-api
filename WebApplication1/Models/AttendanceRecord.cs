namespace WebApplication1.Models
{
    public class AttendanceRecord
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime SignInTime { get; set; }

        public DateTime? SignOutTime { get; set; }

        public Employee Employee { get; set; }
    }
}
