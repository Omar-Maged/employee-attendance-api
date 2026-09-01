using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _db;

        public AttendanceRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<AttendanceRecord?> GetTodayAttendanceRecord(int employeeId)
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var record = await _db.AttendanceRecords
                .FirstOrDefaultAsync(record =>
                    record.EmployeeId == employeeId &&
                    record.SignInTime >= today &&
                    record.SignInTime < tomorrow);

            return record;
        }

        public async Task<AttendanceRecord> CreateAttendanceRecord(AttendanceRecord record)
        {
            _db.AttendanceRecords.Add(record);
            await _db.SaveChangesAsync();

            return record;
        }

        public async Task<AttendanceRecord> UpdateAttendanceRecord(AttendanceRecord record)
        {
            await _db.SaveChangesAsync();

            return record;
        }

        public async Task<List<AttendanceRecord>> GetAttendanceByEmployee(int employeeId)
        {
            var records = await _db.AttendanceRecords
                .Where(record => record.EmployeeId == employeeId)
                .ToListAsync();

            return records;
        }

        public async Task<List<AttendanceRecord>> GetAttendanceByEmployeeAndDate (int employeeId, DateOnly date)
        {
            var startDate = date.ToDateTime(TimeOnly.MinValue);
            var tomorrow = startDate.AddDays(1);
            var records = await _db.AttendanceRecords.Where(record => 
            record.EmployeeId == employeeId && 
            record.SignInTime >= startDate && 
            record.SignInTime < tomorrow ).ToListAsync();

            return records;
        }

        public async Task<List<AttendanceRecord>> GetAttendanceByEmployeeAndMonth(int employeeId, int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var records = await _db.AttendanceRecords
                .Where(record =>
                    record.EmployeeId == employeeId &&
                    record.SignInTime >= startDate &&
                    record.SignInTime < endDate)
                .ToListAsync();

            return records;
        }

        public async Task<List<AttendanceRecord>> GetAttendanceByMonth(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);

            var records = await _db.AttendanceRecords
                .Where(record =>
                    record.SignInTime >= startDate &&
                    record.SignInTime < endDate)
                .ToListAsync();

            return records;
        }


    }
}