namespace WebApplication1.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public DateOnly JoinDate { get; set; }
    }
}