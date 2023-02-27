using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstAndDepController : ControllerBase
    {
        private IDepartmentRepository _departmentRepository;

        public InstAndDepController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<InstitutionAndDepartment>>> GetInstitutionAndDepartment(int id)
        {
            try
            {
                return _departmentRepository.getInstitutionAndDepartment(id);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
