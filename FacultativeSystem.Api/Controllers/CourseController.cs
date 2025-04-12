using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace FacultativeSystem.Api.Controllers;


[ApiController]
[Route("api/[controller]")]

public class CourseController(ICourseService courseService, IFeedbackGradeService feedbackGradeService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCourse([FromBody] CourseRequest courseRequest)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Name = courseRequest.Name,
            StartDate = courseRequest.StartDate.ToUniversalTime(),
            EndDate = courseRequest.EndDate.ToUniversalTime(),
            TeacherId = courseRequest.TeacherId
        };
        await courseService.CreateAsync(course);

        var response = new CourseResponse
        (
            Id : course.Id,
            Name : course.Name,
            StartDate : course.StartDate,
            EndDate : course.EndDate,
            TeacherId: course.TeacherId
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
                IsActive: c.EndDate > DateTime.Now && c.StartDate < DateTime.Now,
                TeacherName: c.Teacher?.UserName
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
            EndDate:course.EndDate,
            TeacherId: course.TeacherId
        );
        
        return Ok(response);
    }

    [HttpPut]
    [Route("{id:Guid}")]

    public async Task<ActionResult> EditCourse([FromRoute] Guid id, [FromBody] CourseRequest courseRequest)
    {
        var course = new Course()
        {
            Id = id,
            Name = courseRequest.Name,
            StartDate = courseRequest.StartDate.ToUniversalTime(),
            EndDate = courseRequest.EndDate.ToUniversalTime(),
            TeacherId = courseRequest.TeacherId
        };
        
        var courseUpdated = await courseService.UpdateAsync(course);
        var response = courseUpdated.Adapt<CourseResponse>();
        return Ok(response);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<ActionResult> DeleteCourse([FromRoute] Guid id)
    {
        var course =  await courseService.DeleteAsync(id);
        var response = course.Adapt<CourseResponse>();
        
        return Ok(response);
    }

    [HttpGet]
    [Route("{courseId:Guid}/grades")]
    public async Task<ActionResult<List<StudentGrades>>> GetStudentGradesByCourseId(Guid courseId)
    {
        var grades = await feedbackGradeService.GetGradesByCourseIdAsync(courseId);

        var response = grades.Select(g => new StudentGrades(
            StudentName: g.StudentName,
            Grade: g.Grade,
            Feedback: g.Feedback,
            FeedbackId: g.Id
        )).ToList();

        return Ok(response);
    }
    
    [HttpGet]
    [Route("{feedbackId:Guid}/feedback")]
    public async Task<ActionResult<FeedbackResponse>> GetFeedback([FromRoute] Guid feedbackId)
    {
        var feedback = await feedbackGradeService.GetByIdAsync(feedbackId);
        if (feedback is null)
            return NotFound();
        
        var response = new FeedbackResponse
        (
           Course: feedback.Course?.Name ?? "Unknown Course",
           Grade: feedback.Grade,
           Feedback: feedback.Feedback
        );
        
        return Ok(response);
    }


    [HttpPut]
    [Route("{feedbackId:Guid}/grades")]

    public async Task<ActionResult> UpdateStudentGrades(Guid feedbackId, [FromBody] Grades grade)
    {
        
        var course = await courseService.GetCourseByFeedbackId(feedbackId);
        if (course == null)
            return NotFound("Course not found");
        
        if (course.EndDate > DateTime.UtcNow)
            return BadRequest("Cannot submit grades until the course is finished.");
        
        var grades = new FeedbackGrade()
        {
            Id = feedbackId,
            Grade = grade.Grade,
            Feedback = grade.Feedback
        };
        
        var gradesUpdate = await feedbackGradeService.UpdateGrades(grades);
        var response = gradesUpdate.Adapt<Grades>();
        return Ok(response);
    }

}