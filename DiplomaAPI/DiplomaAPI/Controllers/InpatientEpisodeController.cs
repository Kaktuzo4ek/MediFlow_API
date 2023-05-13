using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.DiagnosisReport;
using DiplomaAPI.ViewModels.AmbulatoryEpisode;
using DiplomaAPI.ViewModels.Procedure;
using DiplomaAPI.ViewModels.ReferralPackage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using DiplomaAPI.Repositories;
using DiplomaAPI.ViewModels.InpatientEpisode;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InpatientEpisodeController : ControllerBase
    {
        private readonly IInpatientEpisodeRepository _episodeRepository;

        public InpatientEpisodeController(IInpatientEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InpatientEpisode>>> GetEpisodes()
        {
            try
            {
                return _episodeRepository.GetAll();
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("GetEpisodesByPatientId")]
        public async Task<ActionResult<IEnumerable<InpatientEpisode>>> GetEpisodesByPatientId(int patientId)
        {
            try
            {
                return _episodeRepository.GetAllForPatient(patientId);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<InpatientEpisode>>> GetEpisode(int id)
        {
            try
            {
                return _episodeRepository.GetEpisode(id);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(InpatientEpisodeViewModel), 200)]
        public async Task<IActionResult> Post([FromBody] CreateInpatientEpisodeViewModel model)
        {
            try
            {
                return Ok(_episodeRepository.Create(model));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("CreateReferralPackage")]
        [ProducesResponseType(typeof(InpatientEpisodeViewModel), 200)]
        public async Task<IActionResult> Post(int episodeId, [FromBody] CreateReferralPackageViewModel model)
        {
            try
            {
                return Ok(_episodeRepository.CreateReferralPackage(episodeId, model));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("CreateProcedure")]
        [ProducesResponseType(typeof(InpatientEpisodeViewModel), 200)]
        public async Task<IActionResult> Post(int episodeId, [FromBody] CreateProcedureViewModel model)
        {
            try
            {
                return Ok(_episodeRepository.CreateProcedure(episodeId, model));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("CreateDiagnosticReport")]
        [ProducesResponseType(typeof(InpatientEpisodeViewModel), 200)]
        public async Task<IActionResult> Post(int episodeId, [FromBody] CreateDiagnosticReportViewModel model)
        {
            try
            {
                return Ok(_episodeRepository.CreateDiagnosticReport(episodeId, model));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPut("UpdateDiagnosticReport")]
        [ProducesResponseType(typeof(InpatientEpisodeViewModel), 200)]
        public async Task<IActionResult> Put(int episodeId, [FromBody] UpdateDiagnosticReportViewModel model)
        {
            return Ok(_episodeRepository.UpdateDiagnosticReport(episodeId, model));
        }

        [HttpDelete("DeleteDiagnosticReport")]
        [ProducesResponseType(typeof(InpatientEpisodeViewModel), 200)]
        public async Task<IActionResult> Delete(int episodeId, int reportId)
        {
            try
            {
                return Ok(_episodeRepository.DeleteDiagnosticReport(episodeId, reportId));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(InpatientEpisodeViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateInpatientEpisodeViewModel model)
        {
            model.EpisodeId = id;

            return Ok(_episodeRepository.Update(model));
        }

        [HttpPut("UpdateDiagnosis")]
        [ProducesResponseType(typeof(InpatientEpisodeViewModel), 200)]
        public async Task<IActionResult> Put(int episodeId, string diagnosisId)
        {
            return Ok(_episodeRepository.UpdateDiagnosis(episodeId, diagnosisId));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(InpatientEpisodeViewModel), 200)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(_episodeRepository.Delete(id));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("CompleteEpisode")]
        public async Task<IActionResult> CompleteEpisode(int episodeId)
        {
            try
            {
                return Ok(_episodeRepository.CompeleteEpisode(episodeId));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
