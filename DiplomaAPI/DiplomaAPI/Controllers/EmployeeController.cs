using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels;
using DiplomaAPI.ViewModels.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                return _employeeRepository.getAll();
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        /*[HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            try
            {
                return _employeeRepository.getById(id);
            }
            catch (NotFoundException)
            {
                return StatusCode(404);
            }
        }*/

        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeByEmail(string email)
        {
            try
            {
                return _employeeRepository.getByEmail(email);
            }
            catch (NotFoundException)
            {
                return StatusCode(404);
            }
        }

        [HttpPost("{depId}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeFromCertainDepartment(int depId)
        {
            try
            {
                return _employeeRepository.getEmployeeFromCertainDepartment(depId);
            }
            catch (NotFoundException)
            {
                return StatusCode(404);
            }
        }

        [HttpPost("FilterEmployees")]
        public ListEmployees FilterEmployees(string filter, string filterBy, int depId)
        {
                return _employeeRepository.FilterEmployees(filter, filterBy, depId);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EmployeeViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateEmployeeViewModel data)
        {
            data.Id = id;

            return Ok(_employeeRepository.Update(data));
        }
    }
}
