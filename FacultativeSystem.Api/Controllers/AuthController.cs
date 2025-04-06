using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Microsoft.AspNetCore.Identity.Data.LoginRequest;
using RegisterRequest = FacultativeSystem.Api.Contracts.RegisterRequest;

namespace FacultativeSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, ITeacherService teacherService, IStudentService studentService): ControllerBase
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
                    Roles: roles.ToList(),
                    Id: identity.Id
                );
                return Ok(response);
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return ValidationProblem(ModelState);
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