using EmployeeBackend.Application.Dtos;
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
    public class EmployeeUpdateTest
    {
        private readonly Mock<IEmployeeRepository> _repositoryMock;
        private readonly EmployeeService _service;

        public EmployeeUpdateTest()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_repositoryMock.Object);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccess_WhenEmployeeExists()
        {
            // Arrange
            var employeeId = 1;
            var employeeDto = new EmployeeDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "123456789",
                HireDate = DateTime.Now
            };

            var existingEmployee = new Employee
            {
                Id = employeeId,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Phone = "987654321",
                HireDate = DateTime.Now.AddYears(-1)
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(employeeId)).ReturnsAsync(existingEmployee);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateAsync(employeeId, employeeDto);

            // Assert
            Assert.True(result.Success);
            Assert.True(result.Data);
            Assert.Equal("Empleado actualizado exitosamente.", result.Message);
            _repositoryMock.Verify(r => r.UpdateAsync(It.Is<Employee>(e =>
                e.Id == employeeId &&
                e.FirstName == employeeDto.FirstName &&
                e.LastName == employeeDto.LastName &&
                e.Email == employeeDto.Email &&
                e.Phone == employeeDto.Phone &&
                e.HireDate == employeeDto.HireDate
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFailure_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = 1;
            var employeeDto = new EmployeeDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "123456789",
                HireDate = DateTime.Now
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(employeeId)).ReturnsAsync((Employee?)null);

            // Act
            var result = await _service.UpdateAsync(employeeId, employeeDto);

            // Assert
            Assert.False(result.Success);
            Assert.False(result.Data);
            Assert.Equal($"Empleado con ID {employeeId} no encontrado.", result.Message);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Employee>()), Times.Never);
        }
    }
}
