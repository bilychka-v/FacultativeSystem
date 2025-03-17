using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;


[ApiController]
[Route("api/[controller]")]

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

        var response = new CourseResponse
        (
            Id : course.Id,
            Name : course.Name,
            StartDate : course.StartDate,
            EndDate : course.EndDate
        );
         return Ok(response);
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