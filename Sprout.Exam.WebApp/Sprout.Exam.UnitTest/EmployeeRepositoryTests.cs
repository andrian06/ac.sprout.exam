using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Common.Dto;
using Sprout.Exam.Common.Models;
using Sprout.Exam.DataAccess.DataContext;
using Sprout.Exam.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.UnitTest
{
    public class EmployeeRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsEmployeeDtos()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                // Initialize in-memory database with test data
                dbContext.Employee.AddRange(new List<Employee>
                {
                    new Employee { Id = 1, FullName = "John Doe", Birthdate = DateTime.Now.AddYears(-30), TIN = "123456789", EmployeeTypeId = 1, IsDeleted = false },
                    new Employee { Id = 2, FullName = "Jane Smith", Birthdate = DateTime.Now.AddYears(-25), TIN = "987654321", EmployeeTypeId = 2, IsDeleted = false },
                });
                await dbContext.SaveChangesAsync();
            }

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                var repository = new EmployeeRepository(dbContextOptions);

                // Act
                List<EmployeeDto> employeeDtos = await repository.GetAllAsync();

                // Assert
                Assert.Equal(2, employeeDtos.Count);
            }
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectEmployeeDto()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                // Initialize in-memory database with test data
                dbContext.Employee.Add(new Employee { Id = 1, FullName = "John Doe", Birthdate = DateTime.Now.AddYears(-30), TIN = "123456789", EmployeeTypeId = 1, IsDeleted = false });
                await dbContext.SaveChangesAsync();
            }

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                var repository = new EmployeeRepository(dbContextOptions);

                // Act
                EmployeeDto employeeDto = await repository.GetByIdAsync(1);

                // Assert
                Assert.NotNull(employeeDto);
                Assert.Equal("John Doe", employeeDto.FullName);
            }
        }

        [Fact]
        public async Task AddAsync_AddsEmployeeToDatabase()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                var repository = new EmployeeRepository(dbContextOptions);

                // Act
                await repository.AddAsync(new EmployeeDto
                {
                    FullName = "New Employee",
                    Birthdate = DateTime.Now.AddYears(-25),
                    Tin = "555555555",
                    TypeId = 1
                });
            }

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                // Assert
                Assert.Equal(1, dbContext.Employee.Count());
            }
        }

        [Fact]
        public async Task DeleteAsync_SetsIsDeletedToTrue()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                // Initialize in-memory database with test data
                dbContext.Employee.Add(new Employee { Id = 1, FullName = "John Doe", Birthdate = DateTime.Now.AddYears(-30), TIN = "123456789", EmployeeTypeId = 1, IsDeleted = false });
                await dbContext.SaveChangesAsync();
            }

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                var repository = new EmployeeRepository(dbContextOptions);

                // Act
                await repository.DeleteAsync(1);
            }

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                // Assert
                Employee deletedEmployee = dbContext.Employee.FirstOrDefault(e => e.Id == 1);
                Assert.NotNull(deletedEmployee);
                Assert.True(deletedEmployee.IsDeleted);
            }
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEmployeeData()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<EmployeeDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                // Initialize in-memory database with test data
                dbContext.Employee.Add(new Employee { Id = 1, FullName = "John Doe", Birthdate = DateTime.Now.AddYears(-30), TIN = "123456789", EmployeeTypeId = 1, IsDeleted = false });
                await dbContext.SaveChangesAsync();
            }

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                var repository = new EmployeeRepository(dbContextOptions);

                // Act
                await repository.UpdateAsync(new EmployeeDto
                {
                    Id = 1,
                    FullName = "Updated Employee",
                    Birthdate = DateTime.Now.AddYears(-25),
                    Tin = "555555555",
                    TypeId = 2,
                    IsDeleted = false
                });
            }

            using (var dbContext = new EmployeeDbContext(dbContextOptions))
            {
                // Assert
                Employee updatedEmployee = dbContext.Employee.FirstOrDefault(e => e.Id == 1);
                Assert.NotNull(updatedEmployee);
                Assert.Equal("Updated Employee", updatedEmployee.FullName);
            }
        }
    }
}
