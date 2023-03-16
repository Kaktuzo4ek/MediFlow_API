using DiplomaAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace DiplomaAPI.Services
{
    public interface IAuthService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);

        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);

        Task<UserManagerResponse> CheckEmailAsync(CheckEmailViewModel model);

        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);

        Task<UserManagerResponse> ForgetPasswordAsync(string email);

        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);
    }
}
