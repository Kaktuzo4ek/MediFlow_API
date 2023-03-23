using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.AmbulatoryEpisode;
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AmbulatoryEpisodeViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateAmbulatoryEpisodeViewModel model)
        {
            model.EpisodeId = id;

            return Ok(_episodeRepository.Update(model));
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
