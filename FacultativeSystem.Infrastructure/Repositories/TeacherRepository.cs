using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FacultativeSystem.Infrastructure.Repositories;

public class TeacherRepository(DataAccess context) : ITeacherRepository
{
    public async Task CreateAsync(Teacher teacher, CancellationToken cancellationToken = default)
    {
        var teacherEntity = teacher.Adapt<TeacherEntity>();
        await context.Teachers.AddAsync(teacherEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var teachersEntities = await context.Teachers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        var teachers = teachersEntities.Adapt<List<Teacher>>();

        return teachers;
    }

    public async Task<Teacher> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var teacherEntity = await context.Teachers.FindAsync(id, cancellationToken);
        if(teacherEntity is null) throw new Exception("Teacher not found");
        
        var teacher = teacherEntity.Adapt<Teacher>();
        return teacher;
    }

    public async Task CreateTeacherAsync(Teacher teacher, Guid courseId, CancellationToken cancellationToken = default)
    {
        var courseEntity = await context.Courses
            .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken);

        if (courseEntity is null) throw new Exception("Course not found");

        var teacherEntity = teacher.Adapt<TeacherEntity>();
        
        courseEntity.TeacherId = teacherEntity.Id;
        
        context.Teachers.Add(teacherEntity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Guid> UpdateAsync(Guid id, string userName, List<Course>? listCourseNames, CancellationToken cancellationToken = default)
    {
        var teacherEntity = await context.Teachers.FindAsync(id, cancellationToken);
        if(teacherEntity is null) throw new Exception("Teacher not found");
        
        teacherEntity.UserName = userName;
        teacherEntity.CourseNames = listCourseNames.Adapt<List<CourseEntity>>();
        
        await context.SaveChangesAsync(cancellationToken);  
        return teacherEntity.Id;
    }
}