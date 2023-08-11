using Sprout.Exam.Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.Common.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetByIdAsync(int id);
        Task AddAsync(EmployeeDto employeeDto);
        Task UpdateAsync(EmployeeDto employeeDto);
        Task DeleteAsync(int id);
    }
}
