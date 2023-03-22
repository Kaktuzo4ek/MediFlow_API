using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels;
using DiplomaAPI.ViewModels.Role;
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

    }
}
