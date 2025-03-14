using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Application.Abstractions;

public interface ICourseRepository
{
    Task CreateAsync(Course course, CancellationToken cancellationToken = default);
    Task<List<Course>> GetAllCoursesAsync(CancellationToken cancellationToken = default);
    Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default);
    Task<Course> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> UpdateAsync(Guid id, Guid idTeacher, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}