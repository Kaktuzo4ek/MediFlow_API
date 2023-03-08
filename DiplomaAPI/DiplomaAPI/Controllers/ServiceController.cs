using DiplomaAPI.Models;
using DiplomaAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            try
            {
                return _serviceRepository.getAll();
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(string id)
        {
            try
            {
                return _serviceRepository.getById(id);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
