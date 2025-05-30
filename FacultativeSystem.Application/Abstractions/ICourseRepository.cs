using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Application.Abstractions;

public interface ICourseRepository
{
    Task CreateAsync(CourseEntity courseEntity, CancellationToken cancellationToken = default);
    Task<IEnumerable<CourseEntity>> GetAllCoursesAsync(string? query, string? sortBy, string sortDirection, CancellationToken cancellationToken = default);
    Task<List<StudentEntity>> GetAllStudentsAsync(CancellationToken cancellationToken = default);
    Task<CourseEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<CourseEntity?> UpdateAsync(CourseEntity courseEntity, CancellationToken cancellationToken = default);
    Task<CourseEntity?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<CourseEntity?>> GetCourseByTeacherId(Guid teacherId, CancellationToken cancellationToken = default);
    Task<CourseEntity?> GetCourseByFeedbackId(Guid feedbackId, CancellationToken cancellationToken = default);
}