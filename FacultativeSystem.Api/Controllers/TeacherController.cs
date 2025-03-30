using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController(ITeacherService teacherService) : ControllerBase
{
    // [HttpPost]
    // public async Task<ActionResult> CreateTeacher([FromBody] TeacherRequest request)
    // {
    //     var teacher = new Teacher
    //     {
    //         Id = Guid.NewGuid(),
    //         UserName = request.UserName
    //     };
    //     await teacherService.CreateAsync(teacher);
    //
    //     var response = new TeacherResponse
    //     (
    //         teacher.Id,
    //         teacher.UserName,
    //         new List<string>());
    //
    //     return Ok(response);
    // }

    [HttpGet]
    public async Task<ActionResult<List<TeacherResponse>>> GetAllTeachers()
    {
        var teachers = await teacherService.GetAllAsync();

        var response = teachers.Select(t => new TeacherResponse
        (
            Id: t.Id,
            UserName: t.UserName,
            Courses: t.Courses.ToList()

        )).ToList();

        return Ok(response);

    }
}