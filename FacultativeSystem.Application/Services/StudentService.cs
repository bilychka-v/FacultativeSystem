using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;

namespace FacultativeSystem.Application.Services;

public class StudentService(IStudentRepository studentRepository) : IStudentService
{
    public async Task CreateAsync(Student student, CancellationToken cancellationToken = default)
    {
        var studentEntity = student.Adapt<StudentEntity>();
        await studentRepository.CreateAsync(studentEntity, cancellationToken);
    }

    public async Task<List<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
         var studentEntites = await studentRepository.GetAllAsync(cancellationToken);
         return studentEntites.Adapt<List<Student>>();
    }

    public async Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
         var studentEntity = await studentRepository.GetByIdAsync(id, cancellationToken);
         return studentEntity.Adapt<Student>();
        
    }

    public async Task<Guid> UpdateAsync(Guid id, string name, CancellationToken cancellationToken = default)
    {
        return await studentRepository.UpdateAsync(id, name, cancellationToken);
    }
}