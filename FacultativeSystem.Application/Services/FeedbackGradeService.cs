using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Domain.Repositories;
using MapsterMapper;

public class FeedbackGradeService(IMapper mapper, IFeedbackGradeRepository repository) : IFeedbackGradeService
{

    public async Task CreateAsync(FeedbackGrade feedbackGrade)
    {
        var feedbackGradeEntity = mapper.Map<FeedbackGradeEntity>(feedbackGrade);

        await repository.AddAsync(feedbackGradeEntity);
    }

    public async Task<FeedbackGrade?> GetByIdAsync(Guid id)
    {
        var feedbackGrade = await repository.GetByIdAsync(id);
        
        return mapper.Map<FeedbackGrade>(feedbackGrade);
    }

    public async Task<List<FeedbackGrade>> GetByStudentIdAsync(Guid studentId)
    {
        var feedbackGrades = await repository.GetByStudentIdAsync(studentId);
        return mapper.Map<List<FeedbackGrade>>(feedbackGrades);
    }

    public async Task<List<FeedbackGrade>> GetByCourseIdAsync(Guid courseId)
    {
        var feedbackGrades = await repository.GetByCourseIdAsync(courseId);
        return mapper.Map<List<FeedbackGrade>>(feedbackGrades);
    }
}