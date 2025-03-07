using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class TeacherRepository(DataAccess context) : ITeacherRepository
{
    public async Task CreateAsync(TeacherEntity teacherEntity, CancellationToken cancellationToken = default)
    {
        await context.Teachers.AddAsync(teacherEntity, cancellationToken);
    }
    
    public async Task<List<TeacherEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Teachers.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<TeacherEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var teacher = await context.Teachers.FindAsync(id, cancellationToken);
        if(teacher is null) throw new Exception("Teacher not found");
        return teacher;
    }
    
}