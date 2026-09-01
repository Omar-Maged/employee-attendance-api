using AutoMapper;
using Azure;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public AttendanceService(
            IAttendanceRepository attendanceRepository,
            IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<AttendanceResponseDto> CheckIn(int employeeId)
        {
            var response = new AttendanceResponseDto();
            var employee = await _employeeRepository.GetEmployee(employeeId);

            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found.";

                return response;
            }

            var currentTime = DateTime.Now;

            if (currentTime.TimeOfDay < TimeSpan.FromHours(9) ||
                currentTime.TimeOfDay > TimeSpan.FromHours(16))
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Check-in is only allowed between 09:00 and 16:00.";

                return response;
            }

            var todayRecord =
                await _attendanceRepository.GetTodayAttendanceRecord(employeeId);

            if (todayRecord != null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee has already checked in today.";

                return response;
            }

            var record = new AttendanceRecord
            {
                EmployeeId = employeeId,
                SignInTime = currentTime,
                SignOutTime = null
            };

            var createdRecord = await _attendanceRepository.CreateAttendanceRecord(record);

            response.Data = _mapper.Map<AttendanceDto>(createdRecord);

            response.IsSuccess = true;
            response.ErrorMessage = null;

            return response;
        }

        public async Task<AttendanceResponseDto> CheckOut(int employeeId)
        {
            var response = new AttendanceResponseDto();
            var employee = await _employeeRepository.GetEmployee(employeeId);

            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee Not Found";
                return response;
            }

            var currentTime = DateTime.Now;

            if (currentTime.TimeOfDay < TimeSpan.FromHours(9) ||
                currentTime.TimeOfDay > TimeSpan.FromHours(16))
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Check-out is only allowed between 09:00 and 16:00.";

                return response;

            }

            var todayRecord =
                await _attendanceRepository.GetTodayAttendanceRecord(employeeId);

            if (todayRecord == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee has not checked in today.";

                return response;
            }

            if (todayRecord.SignOutTime != null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee has already checked out today.";

                return response;
            }

            todayRecord.SignOutTime = currentTime;


            var updatedRecord = await _attendanceRepository.UpdateAttendanceRecord(todayRecord);

            response.Data = _mapper.Map<AttendanceDto>(updatedRecord);

            response.IsSuccess = true;
            response.ErrorMessage = null;

            return response;
        }

        public async Task<AttendanceListResponseDto> GetAttendanceByEmployee(int employeeId)
        {
            var response = new AttendanceListResponseDto();
            var employee = await _employeeRepository.GetEmployee(employeeId);

            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found.";
            }

            var records = await _attendanceRepository.GetAttendanceByEmployee(employeeId);

            response.Data = _mapper.Map<List<AttendanceDto>>(records);
            response.IsSuccess = true;
            response.ErrorMessage = null;

            return response;

        }

        public async Task<AttendanceListResponseDto> GetAttendanceByEmployeeAndDate(int employeeId, DateOnly date)
        {
            var response = new AttendanceListResponseDto();
            var employee = await _employeeRepository.GetEmployee(employeeId);

            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found.";

                return response;
            }

            var records = await _attendanceRepository.GetAttendanceByEmployeeAndDate(employeeId, date);

            response.Data = _mapper.Map<List<AttendanceDto>>(records);
            response.IsSuccess = true;
            response.ErrorMessage = null;

            return response;

        }

        public async Task<AbsenceResponseDto> GetAbsencesByMonth(int employeeId, int year, int month)
        {
            var response = new AbsenceResponseDto();

            var employee = await _employeeRepository.GetEmployee(employeeId);

            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found.";
                return response;
            }

            if (month < 1 || month > 12)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Invalid month.";
                return response;
            }

            var startDate = new DateOnly(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            if (employee.JoinDate > startDate)
            {
                startDate = employee.JoinDate;
            }

            var today = DateOnly.FromDateTime(DateTime.Now);

            if (startDate > today)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "The selected month is in the future.";
                return response;
            }

            if (year == today.Year &&
                month == today.Month)
            {
                endDate = today;
            }

            var records = await _attendanceRepository.GetAttendanceByEmployeeAndMonth(employeeId, year, month);

            int absences = 0;
            var absenceDates = new List<DateOnly>();

            for (var currentDate = startDate;
                 currentDate <= endDate;
                 currentDate = currentDate.AddDays(1))
            {
                if (currentDate.DayOfWeek == DayOfWeek.Saturday ||
                    currentDate.DayOfWeek == DayOfWeek.Friday)
                {
                    continue;
                }

                var record = records.FirstOrDefault(record =>DateOnly.FromDateTime(record.SignInTime) == currentDate);

                if (record == null || record.SignOutTime == null)
                {
                    absences++;
                    absenceDates.Add(currentDate);
                }
            }

            response.IsSuccess = true;
            response.Data = new AbsenceDto
            {
                NumberOfAbsences = absences,
                Dates = absenceDates
            };
            response.ErrorMessage = null;

            return response;
        }

        public async Task<PresenceResponseDto> GetPresanceByMonth(int employeeId, int year, int month)
        {
            var response = new PresenceResponseDto();

            var employee = await _employeeRepository.GetEmployee(employeeId);

            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found.";
                return response;
            }

            if (month < 1 || month > 12)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Invalid month.";
                return response;
            }

            var startDate = new DateOnly(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            if (employee.JoinDate > startDate)
            {
                startDate = employee.JoinDate;
            }

            var today = DateOnly.FromDateTime(DateTime.Now);

            if (startDate > today)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "The selected month is in the future.";
                return response;
            }

            if (year == today.Year &&
                month == today.Month)
            {
                endDate = today;
            }

            var records = await _attendanceRepository.GetAttendanceByEmployeeAndMonth(employeeId, year, month);

            int Presence = 0;
            var PresenceCheckInDates = new List<DateTime>();
            var PresenceCheckOutDates = new List<DateTime>();


            for (var currentDate = startDate;
                 currentDate <= endDate;
                 currentDate = currentDate.AddDays(1))
            {
                if (currentDate.DayOfWeek == DayOfWeek.Saturday ||
                    currentDate.DayOfWeek == DayOfWeek.Friday)
                {
                    continue;
                }

                var record = records.FirstOrDefault(record => DateOnly.FromDateTime(record.SignInTime) == currentDate);

                if (record != null && record.SignOutTime != null)
                {

                    Presence++;

                    PresenceCheckInDates.Add(record.SignInTime);
                    PresenceCheckOutDates.Add(record.SignOutTime.Value);

                }
            }

            response.IsSuccess = true;
            response.Data = new PresenceDto
            {
                NumberOfPresences = Presence,
                CheckInDates = PresenceCheckInDates,
                CheckOutDates = PresenceCheckOutDates
            };
            response.ErrorMessage = null;

            return response;
        }

        public async Task<CheckInResponseDto> GetCheckInsByMonth(int employeeId, int year, int month)
        {
            var response = new CheckInResponseDto();

            var employee = await _employeeRepository.GetEmployee(employeeId);

            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found.";
                return response;
            }

            if (month < 1 || month > 12)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Invalid month.";
                return response;
            }

            var startDate = new DateOnly(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            if (employee.JoinDate > startDate)
            {
                startDate = employee.JoinDate;
            }

            var today = DateOnly.FromDateTime(DateTime.Now);

            if (startDate > today)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "The selected month is in the future.";
                return response;
            }

            if (year == today.Year &&
                month == today.Month)
            {
                endDate = today;
            }

            var records = await _attendanceRepository.GetAttendanceByEmployeeAndMonth(employeeId, year, month);

            int CheckIns = 0;
            var CheckInDates = new List<DateTime>();


            for (var currentDate = startDate;
                 currentDate <= endDate;
                 currentDate = currentDate.AddDays(1))
            {
                if (currentDate.DayOfWeek == DayOfWeek.Saturday ||
                    currentDate.DayOfWeek == DayOfWeek.Friday)
                {
                    continue;
                }

                var record = records.FirstOrDefault(record => DateOnly.FromDateTime(record.SignInTime) == currentDate);

                if (record != null)
                {

                    CheckIns++;

                    CheckInDates.Add(record.SignInTime);

                }
            }

            response.IsSuccess = true;
            response.Data = new CheckInDto
            {
                Checkin = CheckIns,
                Dates = CheckInDates
            };
            response.ErrorMessage = null;

            return response;

        }

        public async Task<CheckOutResponseDto> GetCheckOutsByMonth(int employeeId, int year, int month)
        {
            var response = new CheckOutResponseDto();

            var employee = await _employeeRepository.GetEmployee(employeeId);

            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found.";
                return response;
            }

            if (month < 1 || month > 12)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Invalid month.";
                return response;
            }

            var startDate = new DateOnly(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            if (employee.JoinDate > startDate)
            {
                startDate = employee.JoinDate;
            }

            var today = DateOnly.FromDateTime(DateTime.Now);

            if (startDate > today)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "The selected month is in the future.";
                return response;
            }

            if (year == today.Year &&
                month == today.Month)
            {
                endDate = today;
            }

            var records = await _attendanceRepository.GetAttendanceByEmployeeAndMonth(employeeId, year, month);

            int CheckOuts = 0;
            var CheckOutDates = new List<DateTime>();


            for (var currentDate = startDate;
                 currentDate <= endDate;
                 currentDate = currentDate.AddDays(1))
            {
                if (currentDate.DayOfWeek == DayOfWeek.Saturday ||
                    currentDate.DayOfWeek == DayOfWeek.Friday)
                {
                    continue;
                }

                var record = records.FirstOrDefault(record => DateOnly.FromDateTime(record.SignInTime) == currentDate);

                if (record != null && record.SignOutTime != null)
                {

                    CheckOuts++;

                    CheckOutDates.Add(record.SignOutTime.Value);

                }
            }

            response.IsSuccess = true;
            response.Data = new CheckOutDto
            {
                CheckOut = CheckOuts,
                Dates = CheckOutDates
            };
            response.ErrorMessage = null;

            return response;

        }
    }
}
