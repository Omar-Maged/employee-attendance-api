using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController] //makes this an API controller, handles request data.
    [Route("api/[controller]")] //defines the URL path for this controller
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var response = await _service.GetAllEmployees();

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMessage);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var response = await _service.CreateEmployee(employeeDto);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMessage);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var response = await _service.GetEmployee(id);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMessage);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDto employeeDto)
        {
            var response = await _service.UpdateEmployee(id, employeeDto);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMessage);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var response = await _service.DeleteEmployee(id);

            if (response.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(response.ErrorMessage);
        }

    }
}
