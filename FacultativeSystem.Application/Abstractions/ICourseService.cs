using System.Collections;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Application.Abstractions;

public interface ICourseService
{
    Task<IEnumerable<Course>> GetAllCoursesAsync(string? query = null, string? sortBy = null, string? sortDirection = null);
    Task CreateAsync(Course course, CancellationToken cancellationToken = default);
    Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default);
    Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Course?> UpdateAsync(Course course, CancellationToken cancellationToken = default);
    Task<Course?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Course?>> GetCourseByTeacherId(Guid teacherId, CancellationToken cancellationToken = default);
    Task<Course?> GetCourseByFeedbackId(Guid feedbackId, CancellationToken cancellationToken = default);

}