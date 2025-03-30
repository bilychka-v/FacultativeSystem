using Microsoft.AspNetCore.Identity;

namespace FacultativeSystem.Application.Abstractions;

public interface ITokenRepository
{
    string CreateJwtToken(List<string> roles, IdentityUser user);
}