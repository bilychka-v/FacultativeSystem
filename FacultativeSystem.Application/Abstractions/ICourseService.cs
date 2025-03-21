using FacultativeSystem.Application.Models;

namespace FacultativeSystem.Application.Abstractions;

public interface ICourseService
{
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task CreateAsync(Course course, CancellationToken cancellationToken = default);
    Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default);
    Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> UpdateAsync(Guid id, Guid idTeacher, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

}