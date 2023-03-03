using DiplomaAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository) 
        {
            _adminRepository = adminRepository;
        }

        [HttpPost("/seed")]
        public void SeedDB()
        {
            _adminRepository.seedDb();
        }
    }
}
