using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class StudentCourseGradeRepository(DataAccess context) : IStudentCourseGradeRepository
{
    public async Task CreateAsync(StudentCourseGradeEntity studentCourseGradeEntity, CancellationToken cancellationToken)
    {
        await context.StudentCourseGrades.AddAsync(studentCourseGradeEntity, cancellationToken);
    }

    public async Task<List<StudentCourseGradeEntity>> GetByStudentIdAsync(Guid studentId,
        CancellationToken cancellationToken)
    {
        var grades = await  context.StudentCourseGrades.Where(x => x.StudentId == studentId).ToListAsync(cancellationToken);
        return grades;
    }

    public async Task<List<StudentCourseGradeEntity>> GetByCourseIdAsync(Guid courseId,
        CancellationToken cancellationToken)
    {
        var grades = await context.StudentCourseGrades.Where(x => x.CourseId == courseId).ToListAsync(cancellationToken);
        return grades;
    }

    public async Task UpdateAsync(Guid id, int grade, string feedback,
        CancellationToken cancellationToken)
    {
        await context.StudentCourseGrades.Where(x => x.StudentId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Grade, x => grade)
                .SetProperty(x => x.Feedback, x => feedback), cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await context.StudentCourseGrades
            .Where(x => x.StudentId == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}