using FacultativeSystem.Application.Models;

public interface IFeedbackGradeService
{
    Task CreateAsync(FeedbackGrade feedbackGrade);
    Task<List<FeedbackGrade?>> GetByIdAsync(Guid id);
    Task<List<FeedbackGrade>> GetByStudentIdAsync(Guid studentId);
    Task<List<FeedbackGrade>> GetByCourseIdAsync(Guid courseId);
    Task<List<FeedbackGrade>> GetGradesByCourseIdAsync(Guid courseId);

}