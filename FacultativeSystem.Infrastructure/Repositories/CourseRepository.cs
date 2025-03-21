using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class CourseRepository(DataAccess context) : ICourseRepository
{
    public async Task CreateAsync(CourseEntity courseEntity, CancellationToken cancellationToken = default)
    {
        await context.Courses.AddAsync(courseEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CourseEntity>> GetAllCoursesAsync(CancellationToken cancellationToken = default)
    {
        var coursesEntities = await context.Courses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return coursesEntities;
    }

    public async Task<List<StudentEntity>> GetAllStudentsAsync(CancellationToken cancellationToken = default)
    {
        var studentEntities = await context.Students
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        return studentEntities;
    }

    public async Task<CourseEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var courseEntity = await context.Courses.FindAsync(id, cancellationToken);
        if(courseEntity is null) throw new Exception("Course not found");
        
        return courseEntity;
    }

    public async Task<Guid> UpdateAsync(Guid id, Guid idTeacher, CancellationToken cancellationToken = default)
    {
        var courseEntity = await context.Courses.FindAsync(id, cancellationToken);
        if(courseEntity is null) throw new Exception("Course not found");

        courseEntity.TeacherId = idTeacher;
        await context.SaveChangesAsync(cancellationToken);
        return courseEntity.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var course = await context.Courses.FindAsync(id, cancellationToken);
        if(course is null) throw new Exception("Course not found");
        
        context.Courses.Remove(course);
        await context.SaveChangesAsync(cancellationToken);

    }
}