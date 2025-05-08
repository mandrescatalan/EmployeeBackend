using EmployeeBackend.Application.Common;
using EmployeeBackend.Application.Dtos;
using EmployeeBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBackend.Application.Interfaces.Service
{
    public interface IEmployeeService
    {
        Task<Response<IEnumerable<Employee>>> GetAllAsync();
        Task<Response<Employee?>> GetByIdAsync(int id);
        Task<Response<int>> CreateAsync(EmployeeDto dto);
        Task<Response<bool>> UpdateAsync(int id, EmployeeDto employeeDto);
        Task<Response<bool>> DeleteEmployeeAsync(int id);
        Task<Response<IEnumerable<Employee>>> GetHiredAfterAsync(DateTime hireDate);
    }
}
