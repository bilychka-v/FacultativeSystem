using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class StudentRepository(DataAccess context) : IStudentRepository
{
    public async Task CreateAsync(StudentEntity studentEntity, CancellationToken cancellationToken = default)
    {
        await context.Students.AddAsync(studentEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<StudentEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var studentEntites = await context.Students
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return studentEntites;
    }

    public async Task<StudentEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
         var studentEntity = await context.Students.FindAsync( id, cancellationToken);
         if(studentEntity is null) throw new Exception("Student not found");
         
         return studentEntity;
    }

    public async Task<Guid> UpdateAsync(Guid id, string name, CancellationToken cancellationToken = default)
    {
        var studentEntity = await context.Students.FindAsync( id, cancellationToken);
        if(studentEntity is null) throw new Exception("Student not found");

        studentEntity.UserName = name;
        await context.SaveChangesAsync(cancellationToken);
        return studentEntity.Id;
        
    }
}