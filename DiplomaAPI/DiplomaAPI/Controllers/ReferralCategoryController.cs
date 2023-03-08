using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralCategoryController : ControllerBase
    {
        private IReferralCategoryRepository _referralCategoryRepository;

        public ReferralCategoryController(IReferralCategoryRepository referralCategoryRepository)
        {
            _referralCategoryRepository = referralCategoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReferralCategory>>> GetReferralCategories()
        {
            try
            {
                return _referralCategoryRepository.getAll();
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReferralCategory>> GetReferralCategory(int id)
        {
            try
            {
                return _referralCategoryRepository.getById(id);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
