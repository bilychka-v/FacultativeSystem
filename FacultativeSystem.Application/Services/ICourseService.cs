using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Application.Services;

public interface ICourseService
{
    Task<List<Course>> GetAllCoursesAsync();
    Task CreateAsync(Course course, CancellationToken cancellationToken = default);
    Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default);
    Task<Course> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

}