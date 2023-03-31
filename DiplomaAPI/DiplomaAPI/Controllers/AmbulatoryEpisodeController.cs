using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.AmbulatoryEpisode;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.DiagnosisReport;
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
    public class AmbulatoryEpisodeController : ControllerBase
    {
        private readonly IAmbulatoryEpisodeRepository _episodeRepository;

        public AmbulatoryEpisodeController(IAmbulatoryEpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmbulatoryEpisode>>> GetEpisodes(int patientId)
        {
            try
            {
                return _episodeRepository.GetAll(patientId);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AmbulatoryEpisode>>> GetEpisode(int id)
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
        [ProducesResponseType(typeof(AmbulatoryEpisodeViewModel), 200)]
        public async Task<IActionResult> Post([FromBody] CreateAmbulatoryEpisodeViewModel model)
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
        [ProducesResponseType(typeof(AmbulatoryEpisodeViewModel), 200)]
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
        [ProducesResponseType(typeof(AmbulatoryEpisodeViewModel), 200)]
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
        [ProducesResponseType(typeof(AmbulatoryEpisodeViewModel), 200)]
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AmbulatoryEpisodeViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateAmbulatoryEpisodeViewModel model)
        {
            model.EpisodeId = id;

            return Ok(_episodeRepository.Update(model));
        }

        [HttpPut("UpdateDiagnosis")]
        [ProducesResponseType(typeof(AmbulatoryEpisodeViewModel), 200)]
        public async Task<IActionResult> Put(int episodeId, string diagnosisId)
        {
            return Ok(_episodeRepository.UpdateDiagnosis(episodeId, diagnosisId));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AmbulatoryEpisodeViewModel), 200)]
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
    }
}
