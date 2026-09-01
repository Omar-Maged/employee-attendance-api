namespace WebApplication1.DTOs
{
    public class AbsenceResponseDto
    {
        public bool IsSuccess { get; set; }
        public AbsenceDto? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
