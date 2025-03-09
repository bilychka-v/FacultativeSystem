using FacultativeSystem.Api.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;


[ApiController]
[Route("Course")]

public class CourseController(DataAccess context) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] Course course)
    {
            var courseEntity = new CourseEntity
            {
                Name = course.Name,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                StudentCourseGradeEntities = course.StudentCourseGrades
                    .Adapt<ICollection<StudentCourseGradeEntity>>(),
                TeacherId = course.TeacherId,
            };
            context.Courses.Add(courseEntity);
            await context.SaveChangesAsync();

        return Ok(courseEntity);
    }
}