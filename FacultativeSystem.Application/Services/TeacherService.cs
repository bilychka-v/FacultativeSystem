using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using MapsterMapper;

namespace FacultativeSystem.Application.Services;

public class TeacherService(ITeacherRepository teacherRepository):ITeacherService
{
    public async Task CreateAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        await teacherRepository.CreateAsync(teacher, cancellationToken);
    }

    public async Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await teacherRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Teacher> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await teacherRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<Guid> UpdateAsync(Guid id, string userName, List<Course>? listCourseNames,
        CancellationToken cancellationToken = default)
    {
        return await teacherRepository.UpdateAsync(id, userName, listCourseNames, cancellationToken);
    }
}