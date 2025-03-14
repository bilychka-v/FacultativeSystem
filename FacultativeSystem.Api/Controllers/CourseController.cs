using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;


[ApiController]
[Route("Course")]

public class CourseController(ICourseService courseService) : ControllerBase
{
    [HttpPost("AddCourse")]
    public async Task<ActionResult<Guid>> CreateCourse([FromBody] CourseRequest courseRequest)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Name = courseRequest.Name,
            StartDate = courseRequest.StartDate,
            EndDate = courseRequest.EndDate
        };
        await courseService.CreateAsync(course);
         return Ok(course.TeacherId);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CourseResponse>> GetCourse(Guid id)
    {
        var course = await courseService.GetByIdAsync(id);
        var response = course.Adapt<CourseResponse>();
        return Ok(response);
    }

    [HttpPut("/AddTeacher")]
    public async Task<ActionResult<Guid>> AddTeacherToCourse(Guid id, [FromBody] CourseRequest courseRequest)
    {
        var courseId = await courseService.UpdateAsync(id, courseRequest.Id);
        return Ok(courseId);
    }
}