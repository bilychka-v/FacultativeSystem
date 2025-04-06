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

    public async Task<List<CourseEntity>> GetAllCoursesAsync(CancellationToken cancellationToken = default)
    {
        var coursesEntities = await context.Courses
            .AsNoTracking()
            .Include(c=>c.Teacher)
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

    public async Task<CourseEntity?> UpdateAsync(CourseEntity courseEntity,
        CancellationToken cancellationToken = default)
    {
        var course = await GetByIdAsync(courseEntity.Id, cancellationToken);
        course.Name = courseEntity.Name;
        course.StartDate = courseEntity.StartDate;
        course.EndDate = courseEntity.EndDate;
        course.TeacherId = courseEntity.TeacherId;
        
        await context.SaveChangesAsync(cancellationToken);
        return course;
        
    }

    public async Task<CourseEntity?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var course = await GetByIdAsync(id, cancellationToken);

        if (course != null) context.Courses.Remove(course);
        await context.SaveChangesAsync(cancellationToken);
        return course;
    }

    public async Task<List<CourseEntity?>> GetCourseByTeacherId(Guid teacherId, CancellationToken cancellationToken = default)
    {
        var teacher = await context.Teachers.FindAsync(teacherId, cancellationToken);
        List<CourseEntity?> courses = await context.Courses.Where(c => c.TeacherId == teacher.Id).ToListAsync(cancellationToken);
        return courses;
    }
}