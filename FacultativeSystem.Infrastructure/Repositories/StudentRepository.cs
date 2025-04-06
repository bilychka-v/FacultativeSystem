using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FacultativeSystem.Infrastructure.Repositories;

public class StudentRepository(DataAccess context, ILogger<StudentRepository> _logger) : IStudentRepository
{
    public async Task CreateAsync(StudentEntity studentEntity, CancellationToken cancellationToken = default)
    {
        await context.Students.AddAsync(studentEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<StudentEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var studentEntites = await context.Students
            .AsNoTracking()
            .Include(c=>c.FeedbackGradeEntities)!
            .ThenInclude(fg=>fg.Course)
            .ToListAsync(cancellationToken);

        return studentEntites;
    }

    public async Task<StudentEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
         var studentEntity = await context.Students.FindAsync( id, cancellationToken);
         if(studentEntity is null) throw new Exception("Student not found");
         
         return studentEntity;
    }

    public async Task<Guid> UpdateAsync(Guid id, string name, CancellationToken cancellationToken = default)
    {
        var studentEntity = await context.Students.FindAsync( id, cancellationToken);
        if(studentEntity is null) throw new Exception("Student not found");

        studentEntity.UserName = name;
        await context.SaveChangesAsync(cancellationToken);
        return studentEntity.Id;
        
    }

    public async Task UpdateStudentCourse(Guid studentId, Guid courseId, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"Updating enrollment: StudentId = {studentId}, CourseId = {courseId}");
            
            var existingEnrollment = await context.FeedbackGradeEntities
                .FirstOrDefaultAsync(fg => fg.StudentId == studentId && fg.CourseId == courseId, cancellationToken);

            if (existingEnrollment != null)
            {
                _logger.LogInformation("Student is already enrolled in the course.");
                return;
            }
            
            var newEnrollment = new FeedbackGradeEntity
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                CourseId = courseId,
                Grade = 0,
                Feedback = string.Empty,
                GradedAt = DateTime.UtcNow
            };

            // Додавання нового запису до контексту
            context.FeedbackGradeEntities.Add(newEnrollment);
            await context.SaveChangesAsync(cancellationToken);
        
            _logger.LogInformation("Enrollment added successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while updating student course: {ex.Message}", ex);
            throw;  // Перериваємо метод, щоб викликати глобальний обробник помилок
        }
    }


}