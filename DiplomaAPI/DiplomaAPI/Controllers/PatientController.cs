using DiplomaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using DiplomaAPI.Repositories.Interfaces;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            try
            {
                return _patientRepository.GetPatients();
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("searchPatients")]
        public async Task<ActionResult<IEnumerable<Patient>>> SearchPatients(string fullname)
        {
            try
            {
                return _patientRepository.SearchPatients(fullname);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
