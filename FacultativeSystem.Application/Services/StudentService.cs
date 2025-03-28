using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using MapsterMapper;

namespace FacultativeSystem.Application.Services;

public class StudentService(IStudentRepository studentRepository, IMapper mapper) : IStudentService
{
    public async Task CreateAsync(Student student, CancellationToken cancellationToken = default)
    {
        var studentEntity = mapper.Map<StudentEntity>(student);
        await studentRepository.CreateAsync(studentEntity, cancellationToken);
    }

    public async Task<List<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
         var studentEntites = await studentRepository.GetAllAsync(cancellationToken);
         return mapper.Map<List<Student>>(studentEntites);
    }

    public async Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
         var studentEntity = await studentRepository.GetByIdAsync(id, cancellationToken);
         return mapper.Map<Student>(studentEntity);
        
    }

    public async Task<Guid> UpdateAsync(Guid id, string name, CancellationToken cancellationToken = default)
    {
        return await studentRepository.UpdateAsync(id, name, cancellationToken);
    }
}