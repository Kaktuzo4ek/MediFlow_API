using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisMKX10AMController : ControllerBase
    {
        private readonly IDiagnosisMKX10AMRepository _diagnosisRepository;

        public DiagnosisMKX10AMController(IDiagnosisMKX10AMRepository diagnosisRepository)
        {
            _diagnosisRepository = diagnosisRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiagnosisMKX10AM>>> GetAll()
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

        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosisMKX10AM>> GetDiagnosis(string id)
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
