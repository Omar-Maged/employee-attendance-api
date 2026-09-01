using WebApplication1.DTOs;

public class EmployeeListResponseDto
{
    public bool IsSuccess { get; set; }
    public List<EmployeeDto>? Data { get; set; }
    public string? ErrorMessage { get; set; }
}