using EmployeeBackend.Application.Interfaces.Persistence;
using EmployeeBackend.Domain.Entities;
using EmployeeBackend.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;


namespace EmployeeBackend.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetHiredAfterAsync(DateTime date)
        {
            return await _context.Employees
                                 .FromSqlRaw("EXEC GetEmployeesHiredAfter @p0", date)
                                 .ToListAsync();
        }
      
    }
}
