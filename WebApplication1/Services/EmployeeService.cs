using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;


namespace WebApplication1.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EmployeeListResponseDto> GetAllEmployees()
        {
            var response = new EmployeeListResponseDto();

            var employees = await _repository.GetAllEmployees();

            response.IsSuccess = true;
            response.Data = _mapper.Map<List<EmployeeDto>>(employees);
            response.ErrorMessage = null;

            return response;
        }

        public async Task<EmployeeResponseDto?> GetEmployee(int id)
        {
            var response = new EmployeeResponseDto();

            var employee = await _repository.GetEmployee(id);

            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found.";

                return response;
            }
            response.IsSuccess = true;
            response.Data = _mapper.Map<EmployeeDto>(employee);
            response.ErrorMessage = null;

            return response;
        }

        public async Task<EmployeeResponseDto> CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var response = new EmployeeResponseDto();

            var employee = _mapper.Map<Employee>(employeeDto);

            var createdEmployee = await _repository.CreateEmployee(employee);

            response.IsSuccess = true;
            response.Data = _mapper.Map<EmployeeDto>(createdEmployee);
            response.ErrorMessage = null;

            return response;
        }

        public async Task<EmployeeResponseDto> UpdateEmployee(int id, UpdateEmployeeDto employeeDto)
        {
            var response = new EmployeeResponseDto();

            var employee = await _repository.GetEmployee(id);

            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found.";

                return response;
            }

            _mapper.Map(employeeDto, employee);

            var updatedEmployee = await _repository.UpdateEmployee(employee);

            response.IsSuccess = true;
            response.Data = _mapper.Map<EmployeeDto>(updatedEmployee);
            response.ErrorMessage = null;

            return response;
        }

        public async Task<DeleteEmployeeResponseDto> DeleteEmployee(int id)
        {
            var response = new DeleteEmployeeResponseDto();

            var employee = await _repository.GetEmployee(id);

            if (employee == null)
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Employee not found.";

                return response;
            }

            await _repository.DeleteEmployee(employee);

            response.IsSuccess = true;
            response.ErrorMessage = null;

            return response;
        }
    }
}
