using Sprout.Exam.Business.Interfaces;
using System;

namespace Sprout.Exam.Business.Helpers
{
    public class RegularSalaryCalculator : ISalaryCalculator
    {
        public decimal ComputeSalary(decimal workedOrAbsentDays)
        {
            decimal salary = 0M;
            decimal taxRate = 0.12M;
            decimal basicSalary = 20000M;

            decimal taxDeduction = basicSalary * taxRate;
            salary = basicSalary - (basicSalary / 22) * workedOrAbsentDays - taxDeduction;


            return Math.Round(salary, 2);
        }
    }

}
