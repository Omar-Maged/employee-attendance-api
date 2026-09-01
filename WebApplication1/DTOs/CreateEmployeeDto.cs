namespace WebApplication1.DTOs
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }

        public string Department { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public DateOnly JoinDate { get; set; }
    }
}