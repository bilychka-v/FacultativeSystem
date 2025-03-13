using FacultativeSystem.Application.Models;

namespace FacultativeSystem.Application.Abstractions;

public interface ITeacherService
{
    Task CreateAsync(Teacher teacher, CancellationToken cancellationToken = default);
    Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Teacher> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}