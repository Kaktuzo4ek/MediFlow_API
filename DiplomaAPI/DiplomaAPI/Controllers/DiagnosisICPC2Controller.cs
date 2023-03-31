using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisICPC2Controller : ControllerBase
    {
        private readonly IDiagnosisICPC2Repository _diagnosisRepository;

        public DiagnosisICPC2Controller(IDiagnosisICPC2Repository diagnosisRepository)
        {
            _diagnosisRepository = diagnosisRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiagnosisICPC2>>> GetAll()
        {
            try
            {
                return _diagnosisRepository.GetAll();
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("GetReasons")]
        public async Task<ActionResult<IEnumerable<DiagnosisICPC2>>> GetReasons()
        {
            try
            {
                return _diagnosisRepository.GetReasons();
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosisICPC2>> GetDiagnosis(int id)
        {
            try
            {
                return _diagnosisRepository.GetDiagnosis(id);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
