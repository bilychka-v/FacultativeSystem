using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class FeedbackGradeRepository : IFeedbackGradeRepository
{
    private readonly DataAccess _context;

    public FeedbackGradeRepository(DataAccess context)
    {
        _context = context;
    }

    public async Task<FeedbackGradeEntity?> GetByIdAsync(Guid id)
    {
        return await _context.FeedbackGradeEntities
            .Include(f=>f.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<List<FeedbackGradeEntity>> GetByStudentIdAsync(Guid studentId)
    {
        return (await _context.FeedbackGradeEntities
            .Include(f => f.Course)
            .Where(f => f.StudentId == studentId)
            .ToListAsync());
    }
    
    public async Task<List<FeedbackGradeEntity>> GetGradesByCourseId(Guid courseId, string? sortByGrade, string? sortDirection, CancellationToken cancellationToken = default)
    {
        var queryGrades = _context.FeedbackGradeEntities
            .Include(g => g.Student)
            .Where(g => g.CourseId == courseId);

        if (string.IsNullOrEmpty(sortByGrade) == false)
        {
            if (string.Equals(sortByGrade, "Grade", StringComparison.OrdinalIgnoreCase))
            {
                var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);
                queryGrades = isAsc ? queryGrades.OrderBy(c => c.Grade) : queryGrades.OrderByDescending(c => c.Grade);
            }
        }
        
        return await queryGrades.ToListAsync(cancellationToken);
    }
    public async Task<FeedbackGradeEntity> UpdateGrades(FeedbackGradeEntity feedbackGrades, CancellationToken cancellationToken = default)
    {
        var feedback = await _context.FeedbackGradeEntities.FindAsync(feedbackGrades.Id);

        if (feedback is null)
            throw new Exception("Feedback grade not found.");
        feedback.Id = feedbackGrades.Id;
        feedback.Grade = feedbackGrades.Grade;
        feedback.Feedback = feedbackGrades.Feedback;

        await _context.SaveChangesAsync(cancellationToken);

        return feedback;
    }


    
}