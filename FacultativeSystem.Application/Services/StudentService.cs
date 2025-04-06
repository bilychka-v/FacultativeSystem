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
         return studentEntites.Select(student => new Student
         {
             Id = student.Id,
             UserName = student.UserName,
             Courses = student.FeedbackGradeEntities?
                 .Where(feedback => feedback.Course !=null)
                 .Select(feedback => feedback.Course!.Name)
                 .ToList() ?? new List<string>()

         }).ToList();
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

    public async Task<Student> UpdateStudentCourse(Guid studentId, Guid courseId, CancellationToken cancellationToken = default)
    {
        await studentRepository.UpdateStudentCourse(studentId, courseId, cancellationToken);
        var updatedStudentEntity = await studentRepository.GetByIdAsync(studentId, cancellationToken);
        return mapper.Map<Student>(updatedStudentEntity);
    }
}