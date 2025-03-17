using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;

namespace FacultativeSystem.Application.Services;

public class CourseService(ICourseRepository courseRepository): ICourseService
{
    public async Task<List<Course>> GetAllCoursesAsync()
    {   
        var coursesEntities = await courseRepository.GetAllCoursesAsync();
        return coursesEntities.Adapt<List<Course>>();
    }

    public async Task CreateAsync(Course course, CancellationToken cancellationToken = default)
    {
        var courseEntity = course.Adapt<CourseEntity>();
        await courseRepository.CreateAsync(courseEntity, cancellationToken);
    }

    public async Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default)
    {
        var studentEntities = await courseRepository.GetAllStudentsAsync(cancellationToken);
        return studentEntities.Adapt<List<Student>>();
    }

    public async Task<Course> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var courseEntity = await courseRepository.GetByIdAsync(id, cancellationToken);
        return  courseEntity.Adapt<Course>();
    }

    public async Task<Guid> UpdateAsync(Guid id, Guid idTeacher, CancellationToken cancellationToken = default)
    {
        return await courseRepository.UpdateAsync(id, idTeacher, cancellationToken);
    }
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await courseRepository.DeleteAsync(id, cancellationToken);
    }
}