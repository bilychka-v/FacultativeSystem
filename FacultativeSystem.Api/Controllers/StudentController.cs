using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;
[ApiController]
[Route("Student")]
public class StudentController:ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateStudent([FromBody] StudentRequest request)
    {
        var student = new Student()
        {
            Id = Guid.NewGuid(), 
            UserName = request.UserName
        };

        await _studentService.CreateAsync(student);

        var response = new StudentResponse
            (
                Id: student.Id, 
                UserName: student.UserName,
                new List<string>());

        return Ok(response);
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
    
}