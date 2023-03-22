using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels.Employee;
using DiplomaAPI.ViewModels.Referral;
using DiplomaAPI.ViewModels.ReferralPackage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralPackageController : ControllerBase
    {
        private IReferralPackageRepository _referralPackageRepository;

        public ReferralPackageController(IReferralPackageRepository referralPackageRepository)
        {
            _referralPackageRepository = referralPackageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReferralPackage>>> GetReferralPackages(int patientId)
        {
            try
            {
                return _referralPackageRepository.getAll(patientId);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("GetMyReferrals")]
        public async Task<ActionResult<IEnumerable<ReferralPackage>>> GetMyReferrals(int doctorId)
        {
            try
            {
                return _referralPackageRepository.getMyReferrals(doctorId);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ReferralPackage>>> GetReferralPackage(string id)
        {
            try
            {
                return _referralPackageRepository.getById(id);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(ReferralPackageViewModel), 200)]
        public async Task<IActionResult> Post([FromBody] CreateReferralPackageViewModel data)
        {
            try
            {
                return Ok(_referralPackageRepository.Create(data));
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
