using DiplomaAPI.Data;
using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {

        private IPositionRepository _positionRepository;

        public PositionController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            try
            {
                return _positionRepository.getAll();
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
            try
            {
                return _positionRepository.getById(id);
            }
            catch (ForbiddenException)
            {
                return StatusCode(403);
            }
        }
    }
}
