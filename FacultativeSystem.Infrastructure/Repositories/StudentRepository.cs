using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Infrastructure.Abstractions;
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
        return await context.Students.ToListAsync(cancellationToken);
    }

    public async Task<StudentEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
         var student = await context.Students.FindAsync( id, cancellationToken);
         if(student is null) throw new Exception("Student not found");
         return student;
    }
}