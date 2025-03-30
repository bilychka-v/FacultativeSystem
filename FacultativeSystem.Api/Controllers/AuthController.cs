using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Microsoft.AspNetCore.Identity.Data.LoginRequest;
using RegisterRequest = FacultativeSystem.Api.Contracts.RegisterRequest;

namespace FacultativeSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository): ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        var identity = await userManager.FindByEmailAsync(request.Email);
        if (identity is not null)
        {
            var checkPassword = await userManager.CheckPasswordAsync(identity, request.Password);
            
            if (checkPassword)
            {
                var roles = await userManager.GetRolesAsync(identity);
                
                var jwtToken = tokenRepository.CreateJwtToken(roles.ToList(), identity);
                
                var response = new LoginResponse(
                    Email: request.Email,
                    Token: jwtToken,
                    Roles: roles.ToList()
                );
                return Ok(response);
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return ValidationProblem(ModelState);
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var user = new IdentityUser
        {
            UserName = request.Email?.Trim(),
            Email = request.Email?.Trim()

        };
        
        var identityResult = userManager.CreateAsync(user, request.Password);

        if (identityResult.Result.Succeeded)
        {
            identityResult = Task.FromResult(await userManager.AddToRoleAsync(user, "student"));

            if (identityResult.Result.Succeeded)
            {
                return Ok();
            }
            else
            {
                if (identityResult.Result.Errors.Any())
                {
                    foreach (var error in identityResult.Result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
        }
        else
        {
            if (identityResult.Result.Errors.Any())
            {
                foreach (var error in identityResult.Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return ValidationProblem(ModelState);
    }
    
}