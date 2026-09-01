using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public interface IEmployeeService
    {


        Task<EmployeeListResponseDto> GetAllEmployees();

        Task<EmployeeResponseDto?> GetEmployee(int id);

        Task<EmployeeResponseDto> CreateEmployee(CreateEmployeeDto employeeDto);

        Task<EmployeeResponseDto> UpdateEmployee(int id, UpdateEmployeeDto employeeDto);
        Task<DeleteEmployeeResponseDto> DeleteEmployee(int id);
    }
}
