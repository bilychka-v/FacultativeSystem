using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class FeedbackGradeRepository(DataAccess context) : IFeedbackGradeRepository
{
    public async Task CreateAsync(FeedbackGrade feedbackGrade, CancellationToken cancellationToken)
    {
        var feedbackGradeEntity = feedbackGrade.Adapt<FeedbackGradeEntity>();
        await context.FeedbackGradeEntities.AddAsync(feedbackGradeEntity, cancellationToken);
    }

    public async Task<List<FeedbackGrade>> GetByStudentIdAsync(Guid studentId,
        CancellationToken cancellationToken)
    {
        var feedbackGradeEntities = await context.FeedbackGradeEntities.FindAsync(studentId, cancellationToken);
        if(feedbackGradeEntities is null) throw new Exception("No feedback grade found");
        
        var feedbackGrades = feedbackGradeEntities.Adapt<List<FeedbackGrade>>();
        return feedbackGrades;
    }
    

    public async Task UpdateAsync(Guid id, int grade, string feedback,
        CancellationToken cancellationToken)
    {
        await context.FeedbackGradeEntities.Where(x => x.StudentId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Grade, x => grade)
                .SetProperty(x => x.Feedback, x => feedback), cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await context.FeedbackGradeEntities
            .Where(x => x.StudentId == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}