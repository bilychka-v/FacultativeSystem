using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Domain.Repositories;
using MapsterMapper;

public class FeedbackGradeService(IMapper mapper, IFeedbackGradeRepository repository) : IFeedbackGradeService
{
    public async Task<FeedbackGrade?> GetByIdAsync(Guid id)
    {
        var feedbackGrade = await repository.GetByIdAsync(id);
        
        return mapper.Map<FeedbackGrade>(feedbackGrade)!;
    }

    public async Task<List<FeedbackGrade>> GetByStudentIdAsync(Guid studentId)
    {
        var feedbackGrades = await repository.GetByStudentIdAsync(studentId);
        return mapper.Map<List<FeedbackGrade>>(feedbackGrades);
    }

    public async Task<List<FeedbackGrade>> GetGradesByCourseIdAsync(Guid courseId)
    {
        var grades = await repository.GetGradesByCourseId(courseId);
        return mapper.Map<List<FeedbackGrade>>(grades);
        
    }

    public async Task<FeedbackGrade> UpdateGrades(FeedbackGrade feedbackGrades, CancellationToken cancellationToken = default)
    {
        var entity = mapper.Map<FeedbackGradeEntity>(feedbackGrades);
        var updatedEntity = await repository.UpdateGrades(entity, cancellationToken);

        return mapper.Map<FeedbackGrade>(updatedEntity);
    }


}