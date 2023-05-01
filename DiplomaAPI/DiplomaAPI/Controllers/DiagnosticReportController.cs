using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.AmbulatoryEpisode.DiagnosisReport;
using DiplomaAPI.ViewModels.Procedure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticReportController : ControllerBase
    {
        private readonly IDiagnosticReportRepository _diagnosticReportRepository;

        public DiagnosticReportController(IDiagnosticReportRepository diagnosticReportRepository)
        {
            _diagnosticReportRepository = diagnosticReportRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiagnosticReport>>> GetAll(int patientId)
        {
            try
            {
                return _diagnosticReportRepository.GetAll(patientId);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiagnosticReport>> GetById(int id)
        {
            try
            {
                return _diagnosticReportRepository.GetById(id);
            }
            catch (NotFoundException)
            {
                return StatusCode(404);
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(DiagnosticReportViewModel), 200)]
        public async Task<IActionResult> Post([FromBody] CreateDiagnosticReportViewModel data)
        {
            try
            {
                return Ok(_diagnosticReportRepository.Create(data));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DiagnosticReportViewModel), 200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateDiagnosticReportViewModel data)
        {
            data.ReportId = id;

            return Ok(_diagnosticReportRepository.Update(data));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DiagnosticReportViewModel), 200)]
        public async Task<IActionResult> Delete(int reportId)
        {
            try
            {
                return Ok(_diagnosticReportRepository.Delete(reportId));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
