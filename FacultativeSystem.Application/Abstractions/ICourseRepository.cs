using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Application.Abstractions;

public interface ICourseRepository
{
    Task CreateAsync(CourseEntity courseEntity, CancellationToken cancellationToken = default);
    Task<IEnumerable<CourseEntity>> GetAllCoursesAsync(CancellationToken cancellationToken = default);
    Task<List<StudentEntity>> GetAllStudentsAsync(CancellationToken cancellationToken = default);
    Task<CourseEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<CourseEntity?> UpdateAsync(CourseEntity courseEntity, CancellationToken cancellationToken = default);
    // Task<Guid> UpdateAsync(Guid id, Guid idTeacher, CancellationToken cancellationToken = default);
    Task<CourseEntity?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}