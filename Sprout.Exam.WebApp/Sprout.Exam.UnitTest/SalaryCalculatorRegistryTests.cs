using Moq;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Registries;
using Xunit;

namespace Sprout.Exam.UnitTest
{
    public class SalaryCalculatorRegistryTests
    {
        [Fact]
        public void AddCalculator_AddsCalculatorToRegistry()
        {
            // Arrange
            EmployeeType employeeType = EmployeeType.Contractual;
            ISalaryCalculator calculator = new Mock<ISalaryCalculator>().Object;

            // Act
            SalaryCalculatorRegistry.AddCalculator(employeeType, calculator);

            // Assert
            Assert.True(SalaryCalculatorRegistry.Calculators.ContainsKey(employeeType));
            Assert.Same(calculator, SalaryCalculatorRegistry.Calculators[employeeType]);
        }


        [Fact]
        public void GetCalculator_ReturnsCorrectCalculator()
        {
            // Arrange
            EmployeeType employeeType = EmployeeType.Contractual;
            ISalaryCalculator expectedCalculator = new Mock<ISalaryCalculator>().Object;
            SalaryCalculatorRegistry.AddCalculator(employeeType, expectedCalculator);

            // Act
            ISalaryCalculator actualCalculator = SalaryCalculatorRegistry.Calculators[employeeType];

            // Assert
            Assert.Same(expectedCalculator, actualCalculator);
        }
    }
}
