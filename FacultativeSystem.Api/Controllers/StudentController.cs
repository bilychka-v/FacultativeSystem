using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentController:ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<StudentResponse>>> GetStudents()
    {
        var students = await _studentService.GetAllAsync();
        var response = students.Select(student => new StudentResponse(
            Id:  student.Id,
            UserName: student.UserName,
            Courses: student.Courses
        )).ToList(); 
        
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateStudents(Guid id, [FromBody] StudentRequest request)
    {
        var studentId = await _studentService.UpdateAsync(id, request.UserName);
        return Ok(studentId);
    }

    [HttpPut("{id:guid}/course")]
    public async Task<ActionResult> UpdateStudent(Guid id, [FromBody] StudentJoinCourse request)
    {
        await _studentService.UpdateStudentCourse(id, request.CourseId);
        return Ok();
    }
    
}