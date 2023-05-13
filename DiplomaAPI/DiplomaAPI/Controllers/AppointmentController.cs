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

        [HttpPost("GetAppointmentsFromAmbulatory")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsFromAmbulatory(int episodeId)
        {
            try
            {
                return _appointmentRepository.GetAllAppointmentsFromAmbulatory(episodeId);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("CreateAppointmentInAmbulatory")]
        [ProducesResponseType(typeof(AppointmentViewModel), 200)]
        public async Task<IActionResult> PostInAmbulatory([FromBody] CreateAppointmentViewModel model)
        {
            try
            {
                return Ok(_appointmentRepository.CreateAppointmentInAmbulatory(model));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPut("InAmbulatory/{id}")]
        [ProducesResponseType(typeof(AppointmentViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateAppointmentViewModel model)
        {
            return Ok(_appointmentRepository.UpdateInAmbulatory(id, model));
        }

        [HttpDelete("InAmbulatory/{id}")]
        [ProducesResponseType(typeof(AmbulatoryEpisodeViewModel), 200)]
        public async Task<IActionResult> DeleteInAmbulatory(int id, int episodeId)
        {
            try
            {
                return Ok(_appointmentRepository.DeleteInAmbulatory(episodeId, id));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("GetAppointmentsFromInpatient")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsFromInpatient(int episodeId)
        {
            try
            {
                return _appointmentRepository.GetAllAppointmentsFromInpatient(episodeId);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("CreateAppointmentInInpatient")]
        [ProducesResponseType(typeof(AppointmentViewModel), 200)]
        public async Task<IActionResult> PostInInpatient([FromBody] CreateAppointmentViewModel model)
        {
            try
            {
                return Ok(_appointmentRepository.CreateAppointmentInInpatient(model));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPut("InInpatient/{id}")]
        [ProducesResponseType(typeof(AppointmentViewModel), 200)]
        public async Task<IActionResult> PutInInpatient(int id, [FromBody] UpdateAppointmentViewModel model)
        {
            return Ok(_appointmentRepository.UpdateInInpatient(id, model));
        }

        [HttpDelete("InInpatient/{id}")]
        [ProducesResponseType(typeof(AmbulatoryEpisodeViewModel), 200)]
        public async Task<IActionResult> DeleteInInpatient(int id, int episodeId)
        {
            try
            {
                return Ok(_appointmentRepository.DeleteInInpatient(episodeId, id));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
