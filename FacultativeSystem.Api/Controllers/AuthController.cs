using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Application.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Microsoft.AspNetCore.Identity.Data.LoginRequest;
using RegisterRequest = FacultativeSystem.Api.Contracts.RegisterRequest;

namespace FacultativeSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, ITeacherService teacherService, IStudentService studentService, IConfiguration configuration): ControllerBase
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
                
                var (accessToken, refreshToken) = tokenRepository.CreateJwtToken(roles.ToList(), identity);
                
                
                Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(7)
                });
                
                var response = new LoginResponse(
                    Email: request.Email,
                    Token: accessToken,
                    Roles: roles.ToList(),
                    Id: identity.Id
                );
                return Ok(response);
            }
        }
        return Unauthorized(new { message = "Invalid email or password." });
    }
    
    [HttpPost("refresh-token")]
    public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized("Refresh token is missing.");
            }
            
            var principal = tokenRepository.GetPrincipalFromExpiredToken(refreshToken);
            if (principal == null)
            {
                return Unauthorized("Invalid refresh token.");
            }

            var email = principal.Identity.Name;
            var user = userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            var roles = userManager.GetRolesAsync(user).Result.ToList();
            var (newAccessToken, newRefreshToken) = tokenRepository.CreateJwtToken(roles, user);
            
            Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7),
                Secure = true
            });

            return Ok(new
            {
                token = newAccessToken,
                refreshToken = newRefreshToken
            });
        }
        catch
        {
            return BadRequest("Invalid token");
        }
    }
    
    [HttpPost("loginWithGoogle")]
public async Task<ActionResult<LoginResponse>> LoginWithGoogle([FromBody] GoogleLoginRequest request)
{
    if (string.IsNullOrWhiteSpace(request?.Credential))
        return BadRequest("Credential is missing.");

    Console.WriteLine($"Received credential: {request.Credential}");

    var settings = new GoogleJsonWebSignature.ValidationSettings
    {
        Audience = new List<string> { configuration["Google:ClientId"]! }
    };

    GoogleJsonWebSignature.Payload payload;
    try
    {
        payload = await GoogleJsonWebSignature.ValidateAsync(request.Credential, settings);
    }
    catch (InvalidJwtException ex)
    {
        return Unauthorized($"Invalid Google token: {ex.Message}");
    }

    var email = payload?.Email?.Trim();
    if (string.IsNullOrEmpty(email))
        return BadRequest("Email not found in Google payload.");

    var user = await userManager.FindByEmailAsync(email);
    


    var roles = await userManager.GetRolesAsync(user);
    var (accessToken, refreshToken) = tokenRepository.CreateJwtToken(roles.ToList(), user);

    Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
    {
        HttpOnly = true,
        Secure = false,
        SameSite = SameSiteMode.Strict,
        Expires = DateTime.UtcNow.AddDays(7)
    });

    var response = new LoginResponse(
        Email: user.Email,
        Token: accessToken,
        Roles: roles.ToList(),
        Id: user.Id
    );

    return Ok(response);
}

    
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> RegisterStudent([FromBody] RegisterRequest request)
    {
        var user = new IdentityUser
        {
            UserName = request.UserName?.Trim(),
            Email = request.Email?.Trim()

        };
        
        var identityResult = userManager.CreateAsync(user, request.Password);

        if (identityResult.Result.Succeeded)
        {
            identityResult = Task.FromResult(await userManager.AddToRoleAsync(user, "student"));

            if (identityResult.Result.Succeeded)
            {
                var student = new Student()
                {
                    UserName = user.UserName,
                    Id = new Guid(user.Id),
                    Courses = new List<string>()
                };
                await studentService.CreateAsync(student);
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
    
    [HttpPost]
    [Route("register/teacher")]
    public async Task<ActionResult> RegisterTeacher([FromBody] RegisterRequest request)
    {
        var user = new IdentityUser
        {
            UserName = request.UserName,
            Email = request.Email?.Trim()

        };
        
        var identityResult = userManager.CreateAsync(user, request.Password);

        if (identityResult.Result.Succeeded)
        {
            identityResult = Task.FromResult(await userManager.AddToRoleAsync(user, "teacher"));

            if (identityResult.Result.Succeeded)
            {
                var teacher = new Teacher()
                {
                    UserName = user.UserName,
                    Id = new Guid(user.Id),
                    Courses = new List<string>()
                };
                await teacherService.CreateAsync(teacher);
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