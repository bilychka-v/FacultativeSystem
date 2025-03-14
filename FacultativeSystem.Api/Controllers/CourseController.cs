using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;


[ApiController]
[Route("Course")]

public class CourseController(ICourseService courseService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateCourse([FromBody] CourseRequest courseRequest)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Name = courseRequest.Name,
            StartDate = courseRequest.StartDate,
            EndDate = courseRequest.EndDate
        };
        await courseService.CreateAsync(course);
        return Ok();
    }
}