using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Dto;
using Sprout.Exam.Common.Interfaces;
using System;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Business.Factories;
using Sprout.Exam.WebApp.Dto;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employees = await _employeeRepository.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving employee(s)! errMsg: {ex.Message}");
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null) return NotFound("Employee not found");
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving employee! errMsg: {ex.Message}");
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EmployeeDto input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existing = await _employeeRepository.GetByIdAsync(input.Id);
                if (existing == null) return NotFound("Employee not found");

                existing.FullName = input.FullName;
                existing.Tin = input.Tin;
                existing.Birthdate = input.Birthdate;
                existing.TypeId = input.TypeId;

                // Save changes
                await _employeeRepository.UpdateAsync(existing);

                return Ok(existing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating employee! errMsg: {ex.Message}");
            }
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(EmployeeDto input)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _employeeRepository.AddAsync(input);

                return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating employee! errMsg: {ex.Message}");
            }
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existing = await _employeeRepository.GetByIdAsync(id);
                if (existing == null) return NotFound("Employee not found");

                await _employeeRepository.DeleteAsync(existing.Id);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting employee! errMsg: {ex.Message}");
            }
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
        {
            try
            {
                var employee = await _employeeRepository.GetByIdAsync(request.Id);
                if (employee == null)
                    return NotFound("Employee not found");

                var employeeType = (EmployeeType)employee.TypeId;

                decimal workedOrAbsentDays;

                switch (employeeType)
                {
                    case EmployeeType.Regular:
                        workedOrAbsentDays = request.AbsentDays;
                        break;
                    case EmployeeType.Contractual:
                        workedOrAbsentDays = request.WorkedDays;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(employeeType), "Employee Type not found");
                }

                // Use factory to get salary calculator
                ISalaryCalculator calculator = SalaryCalculatorFactory
                  .GetCalculator(employeeType);

                // Compute salary
                decimal salary = calculator.ComputeSalary(workedOrAbsentDays);

                return Ok(Math.Round(salary, 2));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calculating employee salary! errMsg: {ex.Message}");
            }

        }

    }
}
