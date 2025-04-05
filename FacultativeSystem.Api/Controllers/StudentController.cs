using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Infrastructure;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class StudentController:ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IFeedbackGradeService _feedbackGradeService;

    public StudentController(IStudentService studentService, IFeedbackGradeService feedbackGradeService) 
    {
        _studentService = studentService;
        _feedbackGradeService = feedbackGradeService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<StudentResponse>>> GetStudents()
    {
        var students = await _studentService.GetAllAsync();
        var response = students.Select(student => new StudentResponse(
            Id:  student.Id,
            UserName: student.UserName,
            Courses: student.Courses ?? new List<string>()
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

    [HttpGet]
    [Route("{id:Guid}/grades")]
    public async Task<ActionResult<List<FeedbackResponse>>> GetStudentGrades([FromRoute] Guid id)
    {
        var feedbackGrades = await _feedbackGradeService.GetByStudentIdAsync(id);
        
        var response = feedbackGrades.Select(f => new FeedbackResponse
        (
            Course: f.Course?.Name ?? "Unknown Course",
            Grade: f.Grade,
            Feedback: f.Feedback
        )).ToList();
        
        return Ok(response);

    }
    
}