namespace WebApplication1.DTOs
{
    public class PresenceResponseDto
    {
        public bool IsSuccess { get; set; }
        public PresenceDto? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
