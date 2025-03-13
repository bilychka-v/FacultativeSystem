using FacultativeSystem.Application.Models;

namespace FacultativeSystem.Application.Abstractions;

public interface ICourseService
{
    Task<List<Course>> GetAllCoursesAsync();
    Task CreateAsync(Course course, CancellationToken cancellationToken = default);
    Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default);
    Task<Course> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

}