namespace WebApplication1.DTOs
{
    public class CheckInDto
    {
        public int Checkin {  get; set; }
        public List<DateTime> Dates { get; set; } = new();

    }
}
