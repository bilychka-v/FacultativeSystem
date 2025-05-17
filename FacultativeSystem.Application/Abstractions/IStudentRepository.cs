using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Application.Abstractions;

public interface IStudentRepository
{
    Task CreateAsync(StudentEntity studentEntity, CancellationToken cancellationToken = default);
    Task<List<StudentEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StudentEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> UpdateAsync(Guid id, string? name, CancellationToken cancellationToken = default);

    Task UpdateStudentCourse(Guid studentId, Guid courseId, CancellationToken cancellationToken = default);
}