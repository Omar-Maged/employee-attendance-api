using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.DTOs;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost("checkin/{employeeId}")]
        public async Task<IActionResult> CheckIn(int employeeId)
        {
            var response =  await _attendanceService.CheckIn(employeeId);

            if(response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.ErrorMessage);
        }

        [HttpPost("checkout/{employeeId}")]
        public async Task<IActionResult> CheckOut(int employeeId)
        {
            var response = await _attendanceService.CheckOut(employeeId);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.ErrorMessage);

        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetAttendanceByEmployee(int employeeId)
        {
            var response = await _attendanceService.GetAttendanceByEmployee(employeeId);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.ErrorMessage);
        }

        [HttpGet("employee/{employeeId}/date/{date}")]
        public async Task<IActionResult> GetAttendanceByEmployeeAndDate(int employeeId,  DateOnly date)
        {
            var response = await _attendanceService.GetAttendanceByEmployeeAndDate(employeeId, date);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.ErrorMessage);
        }

        [HttpGet("employee/{employeeId}/absences/{year}/{month}")]
        public async Task<IActionResult> GetAbsencesByMonth(int employeeId, int year, int month)
        {
            var response =
                await _attendanceService.GetAbsencesByMonth(employeeId, year, month);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMessage);
        }

        [HttpGet("employee/{employeeId}/presances/{year}/{month}")]
        public async Task<IActionResult> GetPresanceByMonth(int employeeId, int year, int month)
        {
            var response =
                await _attendanceService.GetPresanceByMonth(employeeId, year, month);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMessage);
        }

        [HttpGet("employee/{employeeId}/CheckIns/{year}/{month}")]
        public async Task<IActionResult> GetCheckInsByMonth(int employeeId, int year, int month)
        {
            var response =
                await _attendanceService.GetCheckInsByMonth(employeeId, year, month);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMessage);
        }

        [HttpGet("employee/{employeeId}/CheckOuts/{year}/{month}")]
        public async Task<IActionResult> GetCheckOutsByMonth(int employeeId, int year, int month)
        {
            var response =
                await _attendanceService.GetCheckOutsByMonth(employeeId, year, month);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMessage);
        }

    }
}
