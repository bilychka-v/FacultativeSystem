using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;

namespace FacultativeSystem.Application.Services;

public class StudentService(IStudentRepository studentRepository) : IStudentService
{
    public async Task CreateAsync(Student student, CancellationToken cancellationToken = default)
    {
        await studentRepository.CreateAsync(student, cancellationToken);
    }

    public async Task<List<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await studentRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Student> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await studentRepository.GetByIdAsync(id, cancellationToken);
    }
}