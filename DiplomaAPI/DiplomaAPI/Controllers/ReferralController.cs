using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
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
        private readonly IReferralRepository _referralRepository;

        public ReferralController(IReferralRepository referralRepository)
        {
            _referralRepository = referralRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Referral>>> GetReferrals()
        {
            try
            {
                return _referralRepository.getAll();
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Referral>> GetReferral(int id)
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
        public async Task<IActionResult> Put(int id, [FromBody] UpdateReferralViewModel data)
        {
            data.ReferralId = id;

            return Ok(_referralRepository.Update(data));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ReferralViewModel), 200)]
        public async Task<IActionResult> Delete(int referralId, string referralPackageId)
        {
            try
            {
                return Ok(_referralRepository.Delete(referralId, referralPackageId));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpDelete("Ambulatory/{id}")]
        [ProducesResponseType(typeof(ReferralViewModel), 200)]
        public async Task<IActionResult> DeleteInAmbulatory(int referralId, string referralPackageId)
        {
            try
            {
                return Ok(_referralRepository.DeleteInAmbulatory(referralId, referralPackageId));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpDelete("Inpatient/{id}")]
        [ProducesResponseType(typeof(ReferralViewModel), 200)]
        public async Task<IActionResult> DeleteInInpatient(int referralId, string referralPackageId)
        {
            try
            {
                return Ok(_referralRepository.DeleteInInpatient(referralId, referralPackageId));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
