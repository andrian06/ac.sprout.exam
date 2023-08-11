using Sprout.Exam.Business.Interfaces;
using System;

namespace Sprout.Exam.Business.Helpers
{
    public class ContractualSalaryCalculator : ISalaryCalculator
    {
        public decimal ComputeSalary(decimal workedOrAbsentDays)
        {
            decimal salary = 0M;
            decimal dailySalary = 500M;

            salary = dailySalary * workedOrAbsentDays;

            return Math.Round(salary, 2);
        }
    }
}
