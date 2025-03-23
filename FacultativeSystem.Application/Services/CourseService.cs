using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;

namespace FacultativeSystem.Application.Services;

public class CourseService(ICourseRepository courseRepository): ICourseService
{
    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
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

    public async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var courseEntity = await courseRepository.GetByIdAsync(id, cancellationToken);
        return  courseEntity.Adapt<Course>();
    }

    public async Task<Course?> UpdateAsync(Course course, CancellationToken cancellationToken = default)
    {
        var courseEntity = course.Adapt<CourseEntity>();
        await courseRepository.UpdateAsync(courseEntity, cancellationToken);
        return courseEntity.Adapt<Course>();
    }
    public async Task<Course?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var courseEntity =  await courseRepository.DeleteAsync(id, cancellationToken);
        return courseEntity.Adapt<Course>();
    }
}