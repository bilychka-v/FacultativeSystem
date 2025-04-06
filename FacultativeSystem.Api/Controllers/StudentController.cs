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
    private readonly ILogger<StudentController> _logger;

    public StudentController(IStudentService studentService, IFeedbackGradeService feedbackGradeService, ILogger<StudentController> logger) 
    {
        _studentService = studentService;
        _feedbackGradeService = feedbackGradeService;
        _logger = logger;
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

    [HttpPut("{studentId:guid}/course")]
    public async Task<ActionResult> UpdateStudent(Guid studentId, [FromBody] StudentJoinCourse request)
    {
        try
        {
            await _studentService.UpdateStudentCourse(studentId, request.CourseId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while updating student course: {ex.Message}", ex);
            return StatusCode(500, "Internal Server Error");
        }
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