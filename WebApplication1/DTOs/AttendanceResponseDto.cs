namespace WebApplication1.DTOs
{
    public class AttendanceResponseDto
    {
        public bool IsSuccess { get; set; }
        public AttendanceDto? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}