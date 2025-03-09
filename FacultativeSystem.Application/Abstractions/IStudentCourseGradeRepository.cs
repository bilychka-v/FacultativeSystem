using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Application.Abstractions;

public interface IStudentCourseGradeRepository
{
    Task CreateAsync(StudentCourseGradeEntity studentCourseGradeEntity, CancellationToken cancellationToken);
    Task<List<StudentCourseGradeEntity>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken);
    Task<List<StudentCourseGradeEntity>> GetByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);

    Task UpdateAsync(Guid id, int grade, string feedback, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}