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
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCourse([FromBody] CourseRequest courseRequest)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Name = courseRequest.Name,
            StartDate = courseRequest.StartDate.ToUniversalTime(),
            EndDate = courseRequest.EndDate.ToUniversalTime()
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

    [HttpGet]
    public async Task<ActionResult<List<CourseListItemDto>>> GetCourses()
    {
        var courses = await courseService.GetAllCoursesAsync();
        
        var response = courses.Select
        (
            c=> new CourseListItemDto
            (
                Id: c.Id, 
                Name: c.Name,
                IsActive: c.EndDate > DateTime.Now
            )
        ).ToList();
        
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<ActionResult<CourseResponse>> GetCourse([FromRoute] Guid id)
    {
        var course = await courseService.GetByIdAsync(id);
        if (course is null)
            return NotFound();
        
        var response = new CourseResponse
        (
            Id:course.Id, 
            Name:course.Name, 
            StartDate:course.StartDate, 
            EndDate:course.EndDate
        );
        
        return Ok(response);
    }

    // [HttpPut("/AddTeacher")]
    // public async Task<ActionResult<Guid>> AddTeacherToCourse(Guid id, [FromBody] CourseRequest courseRequest)
    // {
    //     var courseId = await courseService.UpdateAsync(id, courseRequest.Id);
    //     return Ok(courseId);
    // }
}