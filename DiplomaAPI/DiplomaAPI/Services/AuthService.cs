using DiplomaAPI.Models;
using DiplomaAPI.Repositories;
using DiplomaAPI.Repositories.Interfaces;
using DiplomaAPI.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;


namespace DiplomaAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Doctor> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly ITokenService _tokenService;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IInstitutionRepository _institutionRepository;


        public AuthService(UserManager<Doctor> userManager, IConfiguration configuration, IMailService mailService, ITokenService tokenService, IDoctorRepository doctorRepository, IInstitutionRepository institutionRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _tokenService = tokenService;
            _doctorRepository = doctorRepository;
            _institutionRepository = institutionRepository;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var institution = _institutionRepository.getById(model.InstitutionId);
            var isConfirmed = false;

            if (model.RoleId == 1 && institution.Certificate.CertificateNumber != model.Certificate)
                return new UserManagerResponse
                {
                    Message = "Certificate doesn`t match",
                    IsSuccess = true,
                };
            else
                isConfirmed = true;

                var identityUser = new Doctor
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Institution = _doctorRepository.setInstitition(model.InstitutionId),
                    Department = _doctorRepository.setDepartment(model.DepartmentId),
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic,
                    PhoneNumber = model.PhoneNumber,
                    DateOfBirth = model.DateOfBirth.Date,
                    Position = _doctorRepository.setPosition(model.PositionId),
                    Gender = model.Gender,
                    Role = _doctorRepository.setRole(model.RoleId),
                    IsConfirmed = isConfirmed
                };

            
            var result = await _userManager.CreateAsync(identityUser, model.Password);


            if (result.Succeeded)
            {
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["AppUrl"]}/api/Auth/ConfirmEmail?userid={identityUser.Id}&token={validEmailToken}";

                await _mailService.SendEmailAsync(identityUser.Email, "Confirm your email", "<h1>Welcome to Auth Demo</h1>" 
                    + $"<p>Please confirm your email by <a href='{url}'>Click here</a></p>");        

                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "User didn't create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
             Doctor user = null;

             user = await _userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with that Email adress" + "     ",
                    IsSuccess = false,
                };
            }

            if (user.EmailConfirmed == false)
            {
                return new UserManagerResponse
                {
                    Message = "Confrirm email to login",
                    IsSuccess = false,
                };
            }

            if (!user.IsConfirmed)
            {
                return new UserManagerResponse
                {
                    Message = "Not Confirmed",
                    IsSuccess = false,
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if(!result)
                return new UserManagerResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };

            var token = _tokenService.BuildToken(_configuration["AuthSettings:Key"], _configuration["AuthSettings:Issuer"], user);

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate= token.ValidTo
            };
        }

        public async Task<UserManagerResponse> CheckEmailAsync(CheckEmailViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with that Email adress",
                    IsSuccess = false,
                };
            }

            return new UserManagerResponse
            {
                Message = model.Email,
                IsSuccess = true,
            };
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "User not found",
                    IsSuccess = false,
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                Message = "Email didn't confirm",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            var user =  await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "No user associated with email",
                    IsSuccess = false,
                };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            await _mailService.SendEmailAsync(email, "Reset Password", "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password copy token: {validToken}</p>");

            return new UserManagerResponse
            {
                Message = "Reset email password has been successfuly sent!",
                IsSuccess = true,
            };

        }

        public async Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "No user associated with email",
                    IsSuccess = false,
                };

            if (model.NewPassword != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Password doesn't match its confrirmation",
                    IsSuccess = false,
                };

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result  = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);


            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Password has been reset successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                Message = "Something went wrong",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
    }
}
