using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.AmbulatoryEpisode;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.Appointment;
using DiplomaAPI.ViewModels.Procedure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            try
            {
                return _appointmentRepository.GetAppointment(id);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("GetAppointments")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments(int episodeId)
        {
            try
            {
                return _appointmentRepository.GetAllAppointments(episodeId);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("CreateAppointment")]
        [ProducesResponseType(typeof(AppointmentViewModel), 200)]
        public async Task<IActionResult> Post([FromBody] CreateAppointmentViewModel model)
        {
            try
            {
                return Ok(_appointmentRepository.CreateAppointment(model));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AppointmentViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateAppointmentViewModel model)
        {
            return Ok(_appointmentRepository.Update(id, model));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AmbulatoryEpisodeViewModel), 200)]
        public async Task<IActionResult> Delete(int id, int episodeId)
        {
            try
            {
                return Ok(_appointmentRepository.Delete(episodeId, id));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
