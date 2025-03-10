using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;
[ApiController]
[Route("Student")]
public class StudentController(DataAccess context):ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateStudent([FromBody] Student student)
    {
        var studentEntity = new StudentEntity{Id = student.Id, UserName = student.UserName};
        context.Students.Add(studentEntity);
        
        await context.SaveChangesAsync();
        return Ok(studentEntity);
    }
}