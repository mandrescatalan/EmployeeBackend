using EmployeeBackend.Application.Interfaces.Persistence;
using EmployeeBackend.Application.Services;
using EmployeeBackend.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeBackend.Tests
{
    public class EmployeeGetHiredAfterTest
    {
        private readonly Mock<IEmployeeRepository> _repositoryMock;
        private readonly EmployeeService _service;

        public EmployeeGetHiredAfterTest()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetHiredAfterAsync_ShouldReturnEmployees_WhenEmployeesExist()
        {
            // Arrange
            var hireDate = new DateTime(2023, 1, 1);
            var employees = new List<Employee>
            {
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "1234567890", HireDate = new DateTime(2023, 2, 1) },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Phone = "0987654321", HireDate = new DateTime(2023, 3, 1) }
            };

            _repositoryMock.Setup(repo => repo.GetHiredAfterAsync(hireDate)).ReturnsAsync(employees);

            // Act
            var response = await _service.GetHiredAfterAsync(hireDate);

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response.Data);
            Assert.Equal(2, response.Data.Count());
            Assert.Equal("Lista de empleados obtenida exitosamente.", response.Message);
        }

        [Fact]
        public async Task GetHiredAfterAsync_ShouldReturnEmpty_WhenNoEmployeesExist()
        {
            // Arrange
            var hireDate = new DateTime(2023, 1, 1);
            var employees = new List<Employee>();

            _repositoryMock.Setup(repo => repo.GetHiredAfterAsync(hireDate)).ReturnsAsync(employees);

            // Act
            var response = await _service.GetHiredAfterAsync(hireDate);

            // Assert
            Assert.False(response.Success);
            Assert.Empty(response.Data);
            Assert.Equal("No hay empleados contratados en ese rango de fecha.", response.Message);
        }
    }
}
