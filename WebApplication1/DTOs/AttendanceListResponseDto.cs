namespace WebApplication1.DTOs
{
    public class AttendanceListResponseDto
    {
        public bool IsSuccess { get; set; }
        public List <AttendanceDto>? Data {  get; set; }
        public string? ErrorMessage { get; set; }
    }
}
