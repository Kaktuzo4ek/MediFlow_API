using DiplomaAPI.Services;
using DiplomaAPI.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _userService;
        private IMailService _mailService;
        private IConfiguration _configuration;

        public AuthController(IAuthService AuthService, IMailService mailService, IConfiguration configuration)
        {
            _userService = AuthService;
            _mailService = mailService;
            _configuration = configuration;
        }

        // api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);
                
                if(result.IsSuccess)
                    return Ok(result); // Status code:200

                return BadRequest(result);

            }

            return BadRequest("Some properties aren't valid"); // Status code: 400
        }

        // api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    await _mailService.SendEmailAsync(model.Email, "New login", "<h1>Hey! New login to your accont noticed</h1>" +
                        "<p>New login to your accont at " + DateTime.Now + "</p");
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties aren't valid");
        }

        // api/auth/CheckEmail
        [HttpPost("CheckEmail")]
        public async Task<IActionResult> CheckEmailAsync([FromBody] CheckEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CheckEmailAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties aren't valid");
        }

        // api/auth/confirmemail?useid&token
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfrirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {

                return Redirect("http://localhost:3000/confirmEmail");
            }

            return BadRequest(result);
        }

        // api/auth/forgetpassword
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();

            var result = await _userService.ForgetPasswordAsync(email);

            if (result.IsSuccess)
            {
                return Ok(result); // 200
            }

            return BadRequest(result); // 400
        }

        // api/auth/resetpassword
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm]ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // 200

                return BadRequest(result);
            }

            return BadRequest("Some properties aren't valid");
        }
    }
}
