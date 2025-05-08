using EmployeeBackend.Application.Common;
using EmployeeBackend.Application.Dtos;
using EmployeeBackend.Application.Interfaces.Persistence;
using EmployeeBackend.Application.Interfaces.Service;
using EmployeeBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeBackend.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<IEnumerable<Employee>>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();

            return new Response<IEnumerable<Employee>>
            {
                Data = employees,
                Success = employees.Any(),
                Message = employees.Any() ? "Lista de empleados obtenida exitosamente." : "No hay empleados registrados."
            };
        }


        public async Task<Response<Employee?>> GetByIdAsync(int id){
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
            {
                return new Response<Employee?>
                {
                    Success = false,
                    Message = "Empleado no encontrado."
                };
            }

            return new Response<Employee?>
            {
                Data = employee,
                Success = true,
                Message = "Empleado encontrado exitosamente."
            };

        }
            

        public async Task<Response<int>> CreateAsync(EmployeeDto dto)
        {
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                HireDate = dto.HireDate
            };

            await _repository.AddAsync(employee);
            return new Response<int>(true, employee.Id, "Empleado creado exitosamente.");
        }

        public async Task<Response<bool>> UpdateAsync(int id, EmployeeDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return new Response<bool>(false, false, $"Empleado con ID {id} no encontrado.");

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            existing.HireDate = dto.HireDate;

            await _repository.UpdateAsync(existing);
            return new Response<bool>(true, true, "Empleado actualizado exitosamente.");
        }

        public async Task<Response<bool>> DeleteEmployeeAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
            {
                return new Response<bool>(false, false, "Empleado no encontrado.");
            }

            await _repository.DeleteAsync(employee);
            return new Response<bool>(true, true, "Empleado eliminado exitosamente.");
        }

        public async Task<Response<IEnumerable<Employee>>> GetHiredAfterAsync(DateTime hireDate) 
        {
           var employees = await _repository.GetHiredAfterAsync(hireDate);

            return new Response<IEnumerable<Employee>>
            {
                Data = employees,
                Success = employees.Any(),
                Message = employees.Any() ? "Lista de empleados obtenida exitosamente." : "No hay empleados contratados en ese rango de fecha."
            };
        }
        
    }
}
