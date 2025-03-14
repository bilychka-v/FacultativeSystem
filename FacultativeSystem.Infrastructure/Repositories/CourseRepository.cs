using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class CourseRepository(DataAccess context) : ICourseRepository
{
    public async Task CreateAsync(Course course, CancellationToken cancellationToken = default)
    {
        if (course.Students != null)
        {
            var courseEntity = course.Students.Adapt<CourseEntity>();
            await context.Courses.AddAsync(courseEntity, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Course>> GetAllCoursesAsync(CancellationToken cancellationToken = default)
    {
        var coursesEntities = await context.Courses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        var courses = coursesEntities.Adapt<List<Course>>();
        return courses;
    }

    public async Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default)
    {
        var studentEntities = await context.Students
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        var students = studentEntities.Adapt<List<Student>>();
        return students;
    }

    public async Task<Course> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var courseEntity = await context.Courses.FindAsync(id, cancellationToken);
        if(courseEntity is null) throw new Exception("Course not found");
        
        var course = courseEntity.Adapt<Course>();
        return course;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var course = await context.Courses.FindAsync(id, cancellationToken);
        if(course is null) throw new Exception("Course not found");
        
        context.Courses.Remove(course);
        await context.SaveChangesAsync(cancellationToken);

    }
}