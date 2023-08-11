using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Common.Dto;
using Sprout.Exam.Common.Interfaces;
using Sprout.Exam.Common.Models;
using Sprout.Exam.DataAccess.DataContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeDbContext _dbContext;
        public EmployeeRepository(DbContextOptions<EmployeeDbContext> dbOptions)
        {
            _dbContext = new EmployeeDbContext(dbOptions);
        }
        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var employees = await _dbContext.Employee
             .Where(e => !e.IsDeleted)
             .ToListAsync();

            var employeeDtos = employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FullName = e.FullName,
                Birthdate = e.Birthdate,
                sBirthdate = e.Birthdate.ToString("yyyy-MM-dd"),
                Tin = e.TIN,
                TypeId = e.EmployeeTypeId
            }).ToList();

            return employeeDtos;
        }
        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            Employee employee = await GetEmployeeByIdAsync(id);
            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Birthdate = employee.Birthdate,
                sBirthdate = employee.Birthdate.ToString("yyyy-MM-dd"),
                Tin = employee.TIN,
                TypeId = employee.EmployeeTypeId,
                IsDeleted = employee.IsDeleted
            };
            return employeeDto;
        }

        private async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _dbContext.Employee.FindAsync(id);
        }

        public async Task AddAsync(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                EmployeeTypeId = employeeDto.TypeId,
                Birthdate = employeeDto.Birthdate,
                FullName = employeeDto.FullName,
                TIN = employeeDto.Tin
            };
            await _dbContext.Employee.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var employeeDto = await GetByIdAsync(id);
            employeeDto.IsDeleted = true;
            await UpdateAsync(employeeDto);
        }
        public async Task UpdateAsync(EmployeeDto employeeDto)
        {
            Employee existingEmployee = await GetEmployeeByIdAsync(employeeDto.Id);


            existingEmployee.EmployeeTypeId = employeeDto.TypeId;
            existingEmployee.Birthdate = employeeDto.Birthdate;
            existingEmployee.FullName = employeeDto.FullName;
            existingEmployee.TIN = employeeDto.Tin;
            existingEmployee.IsDeleted = employeeDto.IsDeleted;


            _dbContext.Employee.Update(existingEmployee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
