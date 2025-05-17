using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;
using MapsterMapper;

namespace FacultativeSystem.Application.Services;

public class TeacherService(ITeacherRepository teacherRepository, IMapper mapper):ITeacherService
{
    public async Task CreateAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        var teacherEntity = mapper.Map<TeacherEntity>(teacher);
        await teacherRepository.CreateAsync(teacherEntity, cancellationToken);
    }

    public async Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var teacherEntities = await teacherRepository.GetAllAsync(cancellationToken);
        return teacherEntities.Adapt<List<Teacher>>();
    }

    public async Task<Teacher> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var teacherEntity = await teacherRepository.GetByIdAsync(id, cancellationToken);
        return mapper.Map<Teacher>(teacherEntity);
    }
    
}