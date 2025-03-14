using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class StudentRepository(DataAccess context) : IStudentRepository
{
    public async Task CreateAsync(Student student, CancellationToken cancellationToken = default)
    {
        var studentEntity = student.Adapt<StudentEntity>();
        await context.Students.AddAsync(studentEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var studentEntites = await context.Students
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var students = studentEntites.Adapt<List<Student>>();
        return students;
    }

    public async Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
         var studentEntity = await context.Students.FindAsync( id, cancellationToken);
         if(studentEntity is null) throw new Exception("Student not found");
         
         var student = studentEntity.Adapt<Student>();
         
         return student;
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