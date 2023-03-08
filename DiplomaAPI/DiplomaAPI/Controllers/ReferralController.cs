using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.Employee;
using DiplomaAPI.ViewModels.Referral;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralController : ControllerBase
    {
        private IReferralRepository _referralRepository;

        public ReferralController(IReferralRepository referralRepository)
        {
            _referralRepository = referralRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Referral>>> GetReferrals(int patientId)
        {
            try
            {
                return _referralRepository.getAll(patientId);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Referral>> GetReferral(string id)
        {
            try
            {
                return _referralRepository.getById(id);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ReferralViewModel), 200)]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateReferralViewModel data)
        {
            data.ReferralId = id;

            return Ok(_referralRepository.Update(data));
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(ReferralViewModel), 200)]
        public async Task<IActionResult> Post([FromBody] CreateReferralViewModel data)
        {
            try
            {
                return Ok(_referralRepository.Create(data));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ReferralViewModel), 200)]
        public async Task<IActionResult> Delete(string referralId)
        {
            try
            {
                return Ok(_referralRepository.Delete(referralId));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
