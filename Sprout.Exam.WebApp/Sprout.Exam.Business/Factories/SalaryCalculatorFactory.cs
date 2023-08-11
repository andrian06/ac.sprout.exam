using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Registries;
using System;

namespace Sprout.Exam.Business.Factories
{
    public static class SalaryCalculatorFactory
    {
        public static ISalaryCalculator GetCalculator(EmployeeType type)
        {
            if (SalaryCalculatorRegistry.Calculators.TryGetValue(type, out ISalaryCalculator calculator))
            {
                return calculator;
            }

            throw new ArgumentException($"No calculator found for EmployeeType {type}");
        }
    }

}
