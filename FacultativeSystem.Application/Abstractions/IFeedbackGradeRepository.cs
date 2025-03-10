using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Application.Abstractions;

public interface IFeedbackGradeRepository
{
    Task CreateAsync(FeedbackGrade feedbackGrade, CancellationToken cancellationToken);
    Task<List<FeedbackGrade>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken);

    Task UpdateAsync(Guid id, int grade, string feedback, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}