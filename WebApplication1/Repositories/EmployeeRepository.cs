using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repositories
{


    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _db;

        public EmployeeRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var employees = await _db.Employees.ToListAsync();

            return (employees);
        }

        public async Task<Employee?> GetEmployee(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            return employee;
    
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            await _db.SaveChangesAsync();

            return employee;
        }

        public async Task DeleteEmployee(Employee employee)
        {
            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
        }

    }
    
}
