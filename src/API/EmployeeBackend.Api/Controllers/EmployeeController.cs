using EmployeeBackend.Application.Dtos;
using EmployeeBackend.Application.Interfaces.Service;
using EmployeeBackend.Application.Services;
using EmployeeBackend.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var response = await _employeeService.GetAllAsync();

            if (!response.Success)
                return NotFound(new { message = response.Message });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var response = await _employeeService.GetByIdAsync(id); 

            if (!response.Success) 
                return NotFound(new { message = response.Message});

            return Ok(response); 
        }
            

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDto dto)
        {
            var response = await _employeeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data }, new { id = response.Data, message = response.Message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto dto)
        {
            var response = await _employeeService.UpdateAsync(id, dto);
            if (!response.Success)
                return NotFound(new { message = response.Message});

            return Ok(new { message = response.Message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _employeeService.DeleteEmployeeAsync(id);
            if (!response.Success)
                return NotFound(new { message = response.Message});

            return Ok(new { message = response.Message });
        }

        [HttpGet("hired-after/{date}")]
        public async Task<IActionResult> GetHiredAfter(DateTime date)
        {
            var response = await _employeeService.GetHiredAfterAsync(date);
            if (!response.Success)
                return NotFound(new { message = response.Message });

            return Ok(response);
        }
            
    }
}
