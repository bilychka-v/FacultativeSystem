using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class TeacherRepository(DataAccess context) : ITeacherRepository
{
    public async Task CreateAsync(TeacherEntity teacherEntity, CancellationToken cancellationToken = default)
    {
        // var teacherEntity = teacher.Adapt<TeacherEntity>();
        await context.Teachers.AddAsync(teacherEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<List<TeacherEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var teachersEntities = await context.Teachers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        // var teachers = teachersEntities.Adapt<List<Teacher>>();

        return teachersEntities.Adapt<List<TeacherEntity>>();
    }

    public async Task<TeacherEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var teacherEntity = await context.Teachers.FindAsync(id, cancellationToken);
        if(teacherEntity is null) throw new Exception("Teacher not found");
        
        // var teacher = teacherEntity.Adapt<Teacher>();
        return teacherEntity;
    }
    
}