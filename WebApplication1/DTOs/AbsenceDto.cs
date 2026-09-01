namespace WebApplication1.DTOs
{
    public class AbsenceDto
    {
        public int NumberOfAbsences { get; set; }
        public List<DateOnly> Dates { get; set; } = new();
    }
}
