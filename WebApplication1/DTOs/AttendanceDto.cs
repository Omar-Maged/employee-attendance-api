namespace WebApplication1.DTOs
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime SignInTime { get; set; }
        public DateTime? SignOutTime { get; set; }
    }
}
