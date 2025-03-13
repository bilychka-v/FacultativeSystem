using FacultativeSystem.Application.Models;

namespace FacultativeSystem.Application.Abstractions;

public interface IStudentService
{
    Task CreateAsync(Student student, CancellationToken cancellationToken = default);
    Task<List<Student>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}