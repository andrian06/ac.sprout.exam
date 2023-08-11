using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.Enums;
using System.Collections.Generic;

namespace Sprout.Exam.Common.Registries
{
    public static class SalaryCalculatorRegistry
    {
        public static Dictionary<EmployeeType, ISalaryCalculator> Calculators { get; } =
          new Dictionary<EmployeeType, ISalaryCalculator>();

        public static void AddCalculator<T>(EmployeeType type, T calculator)
          where T : ISalaryCalculator
        {
            Calculators[type] = calculator;
        }
    }
}
