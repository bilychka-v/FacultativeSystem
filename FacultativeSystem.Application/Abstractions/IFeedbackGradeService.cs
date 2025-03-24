using FacultativeSystem.Application.Models;

public interface IFeedbackGradeService
{
    Task CreateAsync(FeedbackGrade feedbackGrade);
    Task<FeedbackGrade?> GetByIdAsync(Guid id);
    Task<List<FeedbackGrade>> GetByStudentIdAsync(Guid studentId);
    Task<List<FeedbackGrade>> GetByCourseIdAsync(Guid courseId);
    
}