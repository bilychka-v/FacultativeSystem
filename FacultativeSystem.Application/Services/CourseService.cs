using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;

namespace FacultativeSystem.Application.Services;

public class CourseService(ICourseRepository courseRepository): ICourseService
{
    public async Task<List<Course>> GetAllCoursesAsync()
    {
        return await courseRepository.GetAllCoursesAsync();
    }

    public async Task CreateAsync(Course course, CancellationToken cancellationToken = default)
    {
        await courseRepository.CreateAsync(course, cancellationToken);
    }

    public async Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default)
    {
        return await courseRepository.GetAllStudentsAsync(cancellationToken);
    }

    public async Task<Course> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await courseRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await courseRepository.DeleteAsync(id, cancellationToken);
    }
}