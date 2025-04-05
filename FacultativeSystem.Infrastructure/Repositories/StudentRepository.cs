using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class StudentRepository(DataAccess context) : IStudentRepository
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
        // Перевірка, чи студент уже записаний на даний курс (якщо існує запис у фідбек-таблиці)
        var existingEnrollment = await context.FeedbackGradeEntities
            .FirstOrDefaultAsync(fg => fg.StudentId == studentId && fg.CourseId == courseId, cancellationToken);

        if (existingEnrollment != null)
        {
            // Студент уже записаний, можна вийти або оновити існуючий запис, якщо потрібно
            return;
        }

        // Створення нового запису фідбеку як запису про запис студента на курс
        var newEnrollment = new FeedbackGradeEntity
        {
            Id = Guid.NewGuid(),
            StudentId = studentId,
            CourseId = courseId,
            Grade = 0,                  // Початкове значення оцінки (може бути змінено при наданні фідбеку)
            Feedback = string.Empty,    // Порожній фідбек, який заповнюватиметься викладачем
            GradedAt = DateTime.UtcNow  // Дата запису або може бути іншою логікою
        };

        context.FeedbackGradeEntities.Add(newEnrollment);
        await context.SaveChangesAsync(cancellationToken);
    }

}