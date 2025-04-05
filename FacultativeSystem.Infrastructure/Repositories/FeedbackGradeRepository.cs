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

    public async Task<IEnumerable<FeedbackGradeEntity>> GetAllAsync()
    {
        var feedBackEntities = await _context.FeedbackGradeEntities
            .AsNoTracking()
            .ToListAsync();
        return feedBackEntities;
        
    }

    public async Task<FeedbackGradeEntity?> GetByIdAsync(Guid id)
    {
        return await _context.FeedbackGradeEntities.FindAsync(id);
    }

    public async Task<List<FeedbackGradeEntity>> GetByStudentIdAsync(Guid studentId)
    {
        return (await _context.FeedbackGradeEntities
            .Include(f => f.Course)
            .Where(f => f.StudentId == studentId)
            .ToListAsync())!;
    }

    public async Task<IEnumerable<FeedbackGradeEntity>> GetByCourseIdAsync(Guid courseId)
    {
        return await _context.FeedbackGradeEntities
            .Where(f => f.CourseId == courseId)
            .ToListAsync();
    }

    public async Task AddAsync(FeedbackGradeEntity feedback)
    {
        await _context.FeedbackGradeEntities.AddAsync(feedback);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FeedbackGradeEntity feedback)
    {
        _context.FeedbackGradeEntities.Update(feedback);
        await _context.SaveChangesAsync();
    }
    
}