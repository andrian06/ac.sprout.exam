using Sprout.Exam.Common.Dto;

namespace Sprout.Exam.Business.Interfaces
{
    public interface ISalaryCalculator
    {
        decimal ComputeSalary(decimal workedOrAbsentDays);
    }

}
