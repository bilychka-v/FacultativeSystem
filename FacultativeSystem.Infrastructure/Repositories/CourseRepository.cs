using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class CourseRepository(DataAccess context) : ICourseRepository
{
    public async Task CreateAsync(CourseEntity course, CancellationToken cancellationToken = default)
    {
        await context.Courses.AddAsync(course, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CourseEntity>> GetAllCoursesAsync(CancellationToken cancellationToken = default)
    {
        return await context.Courses.ToListAsync(cancellationToken);
    }

    public async Task<List<StudentEntity>> GetAllStudentsAsync(CancellationToken cancellationToken = default)
    {
        return await context.Students.ToListAsync(cancellationToken);
    }

    public async Task<CourseEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var course = await context.Courses.FindAsync(id, cancellationToken);
        if(course is null) throw new Exception("Course not found");
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