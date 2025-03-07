using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Infrastructure.Abstractions;

public interface ICourseRepository
{
    Task CreateAsync(CourseEntity course, CancellationToken cancellationToken = default);
    Task<List<CourseEntity>> GetAllCoursesAsync(CancellationToken cancellationToken = default);
    Task<List<StudentEntity>> GetAllStudentsAsync(CancellationToken cancellationToken = default);
    Task<CourseEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}