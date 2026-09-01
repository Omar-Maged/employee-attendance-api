namespace WebApplication1.DTOs
{
    public class CheckInResponseDto
    {
        public bool IsSuccess { get; set; }
        public CheckInDto? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
