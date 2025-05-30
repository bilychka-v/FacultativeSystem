using FacultativeSystem.Application.Models;

public interface IFeedbackGradeService
{
    Task<FeedbackGrade?> GetByIdAsync(Guid id);
    Task<List<FeedbackGrade>> GetByStudentIdAsync(Guid studentId);
    Task<List<FeedbackGrade>> GetGradesByCourseIdAsync(Guid courseId, string? sortByGrade, string? sortDirection);
    Task<FeedbackGrade> UpdateGrades(FeedbackGrade feedbackGrades, CancellationToken cancellationToken = default);

}