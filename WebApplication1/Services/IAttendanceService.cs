using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public interface IAttendanceService
    {
        Task<AttendanceResponseDto> CheckIn(int employeeId);

        Task<AttendanceResponseDto> CheckOut(int employeeId);

        Task<AttendanceListResponseDto> GetAttendanceByEmployee(int employeeId);
        Task<AttendanceListResponseDto> GetAttendanceByEmployeeAndDate(int employeeId, DateOnly date);
        Task<AbsenceResponseDto> GetAbsencesByMonth(int employeeId, int year, int month);
        Task<PresenceResponseDto> GetPresanceByMonth(int employeeId, int year, int month);
        Task<CheckInResponseDto> GetCheckInsByMonth(int employeeId, int year, int month);
        Task<CheckOutResponseDto> GetCheckOutsByMonth(int employeeId, int year, int month);
    }
}