using System.Collections;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FacultativeSystem.Infrastructure.Repositories;

public class CourseRepository(DataAccess context) : ICourseRepository
{
    public async Task CreateAsync(CourseEntity courseEntity, CancellationToken cancellationToken = default)
    {
        await context.Courses.AddAsync(courseEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CourseEntity>> GetAllCoursesAsync(string? query, string? sortBy, string? sortDirection, CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow;
        var queryCourses = context.Courses
            .Include(c=> c.Teacher)
            .AsQueryable();

        if (string.IsNullOrEmpty(query) == false)
        {
            queryCourses = queryCourses
                .Where(course => course.Name.Contains(query));
        }

        if (string.IsNullOrEmpty(sortBy) == false)
        {
            if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase) )
            {
                var isAsc = string.Equals(sortDirection, "ASC", StringComparison.OrdinalIgnoreCase) ? true : false;
                queryCourses = isAsc ? queryCourses.OrderBy(c => c.Name) : queryCourses.OrderByDescending(c => c.Name);
            }

            if (string.Equals(sortBy, "isActive", StringComparison.OrdinalIgnoreCase))
            {
                var isAsc = string.Equals(sortDirection, "ASC", StringComparison.OrdinalIgnoreCase) ? true : false;
                queryCourses = isAsc ? queryCourses.OrderBy(c => c.EndDate > utcNow && c.StartDate < utcNow) : queryCourses.OrderBy(c => c.EndDate < utcNow );
            }
        }
        return await queryCourses.ToListAsync(cancellationToken);
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
        List<CourseEntity?> courses = await context.Courses
            .Where(c => c.TeacherId == teacher.Id)
            .Include(c => c.Teacher)
            .Select(c => new CourseEntity
            {
                Id = c.Id,
                Name = c.Name,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                HasUnmarkedStudents = context.FeedbackGradeEntities
                    .Where(g => g.CourseId == c.Id)
                    .Any(cs =>
                        cs.CourseId == c.Id && (
                        cs.Grade == 0 ||
                        string.IsNullOrEmpty(cs.Feedback)))
            })
            .ToListAsync(cancellationToken);
        return courses;
    }

    public async Task<CourseEntity?> GetCourseByFeedbackId(Guid feedbackId,
        CancellationToken cancellationToken = default)
    {
        var feedback = await context.FeedbackGradeEntities
            .Include(c => c.Course)
            .FirstOrDefaultAsync(f=>f.Id == feedbackId, cancellationToken);
        return feedback?.Course;
    }
}