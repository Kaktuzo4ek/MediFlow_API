using DiplomaAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace DiplomaAPI.Services
{
    public interface ITokenService
    {
        JwtSecurityToken BuildToken(string key, string issuer, Doctor user);

        bool ValidateToken(string key, string issuer, string audience, string token);

        //void CheckAccess(string token, string type, int? expectedId = null);
    }
}
