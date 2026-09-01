namespace WebApplication1.DTOs
{
    public class CheckOutDto
    {
        public int CheckOut { get; set; }
        public List<DateTime> Dates { get; set; } = new();
    }
}
