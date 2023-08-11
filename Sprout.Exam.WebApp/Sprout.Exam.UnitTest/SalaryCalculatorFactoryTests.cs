using Moq;
using Sprout.Exam.Business.Factories;
using Sprout.Exam.Business.Interfaces;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Registries;
using System;
using Xunit;

namespace Sprout.Exam.UnitTest
{
    public class SalaryCalculatorFactoryTests
    {
        [Fact]
        public void GetCalculator_ReturnsCorrectCalculator()
        {
            // Arrange
            EmployeeType employeeType = EmployeeType.Contractual;
            ISalaryCalculator expectedCalculator = new Mock<ISalaryCalculator>().Object;
            SalaryCalculatorRegistry.AddCalculator(employeeType, expectedCalculator);

            // Act
            ISalaryCalculator actualCalculator = SalaryCalculatorFactory.GetCalculator(employeeType);

            // Assert
            Assert.Same(expectedCalculator, actualCalculator);
        }

        [Fact]
        public void GetCalculator_UnknownEmployeeType_ThrowsException()
        {
            // Arrange
            EmployeeType unknownEmployeeType = (EmployeeType)100; // Assuming this is an unknown type

            // Act & Assert
            Assert.Throws<ArgumentException>(() => SalaryCalculatorFactory.GetCalculator(unknownEmployeeType));
        }
    }
}
