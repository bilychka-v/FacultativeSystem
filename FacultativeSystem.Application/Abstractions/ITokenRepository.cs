using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace FacultativeSystem.Application.Abstractions;

public interface ITokenRepository
{
    (string accessToken, string refreshToken) CreateJwtToken(List<string> roles, IdentityUser user);
    string CreateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}