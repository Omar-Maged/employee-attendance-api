using WebApplication1.DTOs;

public class EmployeeResponseDto
{
    public bool IsSuccess { get; set; }
    public EmployeeDto? Data { get; set; }
    public string? ErrorMessage { get; set; }
}