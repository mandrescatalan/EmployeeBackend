using EmployeeBackend.Application.Dtos;
using EmployeeBackend.Application.Interfaces.Persistence;
using EmployeeBackend.Application.Interfaces.Service;
using EmployeeBackend.Application.Services;
using EmployeeBackend.Domain.Entities;
using Moq;
using Xunit;

namespace EmployeeBackend.Tests
{
    public class EmployeeAddTest
    {
        private readonly Mock<IEmployeeRepository> _repositoryMock;
        private readonly EmployeeService _employeeService;

        public EmployeeAddTest()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnSuccessResponse_WhenEmployeeIsCreated()
        {
            // Arrange
            var employeeDto = new EmployeeDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "123456789",
                HireDate = DateTime.UtcNow
            };

            var createdEmployee = new Employee
            {
                Id = 1,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                HireDate = employeeDto.HireDate
            };

            _repositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Employee>()))
                .Callback<Employee>(e => e.Id = createdEmployee.Id)
                .Returns(Task.CompletedTask);

            // Act
            var response = await _employeeService.CreateAsync(employeeDto);

            // Assert
            Assert.True(response.Success);
            Assert.Equal(createdEmployee.Id, response.Data);
            Assert.Equal("Empleado creado exitosamente.", response.Message);

            _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Employee>()), Times.Once);
        }

    }
}
