using FacultativeSystem.Api.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;
[ApiController]
[Route("Teacher")]
public class TeacherController(DataAccess context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTeacher(Teacher teacher)
    {
        var teacherEntity = new TeacherEntity{Id = teacher.Id, Name = teacher.Name};
        context.Teachers.Add(teacherEntity);
        
        await context.SaveChangesAsync();
        return Ok(teacherEntity);
    }
}