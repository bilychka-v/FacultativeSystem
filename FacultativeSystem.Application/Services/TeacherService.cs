using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;
using MapsterMapper;

namespace FacultativeSystem.Application.Services;

public class TeacherService(ITeacherRepository teacherRepository):ITeacherService
{
    public async Task CreateAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        var teacherEntity = teacher.Adapt<TeacherEntity>();
        await teacherRepository.CreateAsync(teacherEntity, cancellationToken);
    }

    public async Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var teachersEntities = await teacherRepository.GetAllAsync(cancellationToken);
        return teachersEntities.Adapt<List<Teacher>>(); 
    }

    public async Task<Teacher> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var teacherEntity = await teacherRepository.GetByIdAsync(id, cancellationToken);
        return teacherEntity.Adapt<Teacher>();
    }
    
}