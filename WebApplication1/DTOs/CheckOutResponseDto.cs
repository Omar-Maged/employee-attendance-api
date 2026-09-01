namespace WebApplication1.DTOs
{
    public class CheckOutResponseDto
    {
        public bool IsSuccess { get; set; }
        public CheckOutDto? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
