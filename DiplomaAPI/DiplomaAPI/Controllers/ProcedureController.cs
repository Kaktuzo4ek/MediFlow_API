using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.Procedure;
using DiplomaAPI.ViewModels.Referral;
using DiplomaAPI.ViewModels.ReferralPackage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private readonly IProcedureRepository _procedureRepository;

        public ProcedureController(IProcedureRepository procedureRepository)
        {
            _procedureRepository = procedureRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Procedure>>> GetProcedures(int patientId)
        {
            try
            {
                return _procedureRepository.getAll(patientId);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Procedure>> GetProcedure(int id)
        {
            try
            {
                return _procedureRepository.getById(id);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(ProcedureViewModel), 200)]
        public async Task<IActionResult> Post([FromBody] CreateProcedureViewModel data)
        {
            try
            {
                return Ok(_procedureRepository.Create(data));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProcedureViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProcedureViewModel data)
        {
            data.ProcedureId = id;

            return Ok(_procedureRepository.Update(data));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProcedureViewModel), 200)]
        public async Task<IActionResult> Delete(int procedureId)
        {
            try
            {
                return Ok(_procedureRepository.Delete(procedureId));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
