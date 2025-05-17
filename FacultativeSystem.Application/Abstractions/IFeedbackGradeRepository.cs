using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Domain.Repositories;

public interface IFeedbackGradeRepository
{
    Task<FeedbackGradeEntity?> GetByIdAsync(Guid id);
    Task<List<FeedbackGradeEntity>> GetByStudentIdAsync(Guid studentId);
    
    Task<List<FeedbackGradeEntity>> GetGradesByCourseId(Guid courseId, string? sortByGrade, string? sortDirection, CancellationToken cancellationToken = default);

    Task<FeedbackGradeEntity> UpdateGrades(FeedbackGradeEntity feedbackGrades, CancellationToken cancellationToken = default);
    
}