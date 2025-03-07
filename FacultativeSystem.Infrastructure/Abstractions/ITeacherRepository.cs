using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Infrastructure.Abstractions;

public interface ITeacherRepository
{
    Task CreateAsync(TeacherEntity teacherEntity, CancellationToken cancellationToken = default);
    Task<List<TeacherEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TeacherEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}