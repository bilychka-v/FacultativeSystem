using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController(ITeacherService teacherService, ICourseService courseService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TeacherResponse>>> GetAllTeachers()
    {
        var teachers = await teacherService.GetAllAsync();

        var response = teachers.Select(t => new TeacherResponse
        (
            Id: t.Id,
            UserName: t.UserName,
            Courses: t.Courses.ToList()

        )).ToList();

        return Ok(response);

    }

    [HttpGet]
    [Route("{id:Guid}")]
    
    public async Task<ActionResult<TeacherResponse>> GetTeacher([FromRoute] Guid id)
    {
        var teacher = await teacherService.GetByIdAsync(id);
        if (teacher is null)
            return NotFound();
    
        var response = new TeacherResponse
        (
            Id : teacher.Id,
            UserName : teacher.UserName,
            Courses : teacher.Courses.ToList()
        );
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:Guid}/courses")]
    public async Task<ActionResult<List<CourseByTeacherResponse>>> GetCoursesByTeacherId([FromRoute] Guid id)
    {
        var course = await courseService.GetCourseByTeacherId(id);

        var response = course
            .Where(c => c != null)
            .Select(c => new CourseByTeacherResponse
            (
                c!.Id,
                c!.Name,
                c.StartDate,
                c.EndDate,
                c.HasUnmarkedStudents
            )
        ).ToList();
        return Ok(response);
    }
}