using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;
[ApiController]
[Route("Teacher")]
public class TeacherController(ITeacherService teacherService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateTeacher([FromBody] TeacherRequest request)
    {
        var teacher = new Teacher
        {
            Id = Guid.NewGuid(), 
            UserName = request.UserName
        };
        await teacherService.CreateAsync(teacher);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<TeacherResponse>>> GetAllTeachers()
    {
        var teachers = await teacherService.GetAllAsync();

        var response = teachers.Select(t => new TeacherResponse(
            TeacherId: t.Id,
            TeacherName: t.UserName,
            CourseName: t.Courses?.Select(c=>c.Name).ToList() ?? new List<string>()))
            .ToList();
        return Ok(response);
    }
}