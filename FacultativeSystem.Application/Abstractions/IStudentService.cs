using FacultativeSystem.Application.Models;

namespace FacultativeSystem.Application.Abstractions;

public interface IStudentService
{
    Task<List<Student>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> UpdateAsync(Guid id, string name, CancellationToken cancellationToken = default);
    Task<Student> UpdateStudentCourse(Guid studentId, Guid courseId, CancellationToken cancellationToken = default);
}