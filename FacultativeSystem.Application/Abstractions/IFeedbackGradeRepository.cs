using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Domain.Repositories;

public interface IFeedbackGradeRepository
{
    Task<IEnumerable<FeedbackGradeEntity>> GetAllAsync();
    Task<FeedbackGradeEntity?> GetByIdAsync(Guid id);
    Task<List<FeedbackGradeEntity>> GetByStudentIdAsync(Guid studentId);
    Task<IEnumerable<FeedbackGradeEntity>> GetByCourseIdAsync(Guid courseId);
    Task AddAsync(FeedbackGradeEntity feedback);
    Task UpdateAsync(FeedbackGradeEntity feedback);
}