namespace WebApplication1.DTOs
{
    public class PresenceDto
    {
        public int NumberOfPresences { get; set; }
        public List<DateTime> CheckInDates { get; set; } = new();
        public List<DateTime> CheckOutDates { get; set; } = new();
    }
}
