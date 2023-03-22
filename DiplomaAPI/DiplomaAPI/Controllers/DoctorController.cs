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
    public class DoctorController : ControllerBase
    {
        private IDoctorRepository _doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository) 
        {
            _doctorRepository = doctorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetEmployees()
        {
            try
            {
                return _doctorRepository.getAll();
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
                return _doctorRepository.getById(id);
            }
            catch (NotFoundException)
            {
                return StatusCode(404);
            }
        }*/

        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetEmployeeByEmail(string email)
        {
            try
            {
                return _doctorRepository.getByEmail(email);
            }
            catch (NotFoundException)
            {
                return StatusCode(404);
            }
        }

        [HttpPost("{depId}")]
        public async Task<ActionResult<IEnumerable<Doctor>>> getDoctorsFromCertainDepartment(int depId)
        {
            try
            {
                return _doctorRepository.getDoctorsFromCertainDepartment(depId);
            }
            catch (NotFoundException)
            {
                return StatusCode(404);
            }
        }

        [HttpPost("ConfirmDoctor")]
        public async Task<ActionResult<Doctor>> ConfirmDoctor(int doctorId)
        {
            try
            {
                return _doctorRepository.ConfirmDoctor(doctorId);
            }
            catch (NotFoundException)
            {
                return StatusCode(404);
            }
        }

        [HttpPost("DeclineDoctor")]
        public async Task<ActionResult<Doctor>> DeclineDoctor(int doctorId)
        {
            try
            {
                return _doctorRepository.DeclineDoctor(doctorId);
            }
            catch (NotFoundException)
            {
                return StatusCode(404);
            }
        }

        [HttpPost("GetNotConfirmDoctors")]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetNotConfirmDoctors(int institutionId)
        {
            try
            {
                return _doctorRepository.GetNotConfirmDoctors(institutionId);
            }
            catch (NotFoundException)
            {
                return StatusCode(404);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DoctorViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateDoctorViewModel data)
        {
            data.Id = id;

            return Ok(_doctorRepository.Update(data));
        }
    }
}
