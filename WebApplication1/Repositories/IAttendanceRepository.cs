using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IAttendanceRepository
    {
        Task<AttendanceRecord?> GetTodayAttendanceRecord(int employeeId);
        Task<AttendanceRecord> CreateAttendanceRecord(AttendanceRecord record);
        Task<AttendanceRecord> UpdateAttendanceRecord(AttendanceRecord record);
        Task<List<AttendanceRecord>> GetAttendanceByEmployee(int employeeId);
        Task<List<AttendanceRecord>> GetAttendanceByEmployeeAndDate(int employeeId, DateOnly date);
        Task<List<AttendanceRecord>> GetAttendanceByEmployeeAndMonth(int employeeId, int year, int month);
        Task<List<AttendanceRecord>> GetAttendanceByMonth(int year, int month);

    }
}
