using System.Collections;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using FacultativeSystem.Domain.Repositories;
using MapsterMapper;

namespace FacultativeSystem.Application.Services;

public class CourseService(ICourseRepository courseRepository, IFeedbackGradeRepository feedbackGradeRepository, IMapper mapper): ICourseService
{
    public async Task<IEnumerable<Course>> GetAllCoursesAsync(string? query = null, string? sortBy = null, string? sortDirection = null)
    {   
        var coursesEntities = await courseRepository.GetAllCoursesAsync(query, sortBy, sortDirection);
        return mapper.Map<IEnumerable<Course>>(coursesEntities);
    }

    public async Task CreateAsync(Course course, CancellationToken cancellationToken = default)
    {
        var courseEntity = mapper.Map<CourseEntity>(course);
        await courseRepository.CreateAsync(courseEntity, cancellationToken);
    }

    public async Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default)
    {
        var studentEntities = await courseRepository.GetAllStudentsAsync(cancellationToken);
        return mapper.Map<List<Student>>(studentEntities);
        
    }

    public async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var courseEntity = await courseRepository.GetByIdAsync(id, cancellationToken);
        return  mapper.Map<Course>(courseEntity);
    }

    public async Task<Course?> UpdateAsync(Course course, CancellationToken cancellationToken = default)
    {
        var courseEntity = mapper.Map<CourseEntity>(course);
        // var courseEntity = await courseRepository.GetByIdAsync(course.Id, cancellationToken);
        await courseRepository.UpdateAsync(courseEntity, cancellationToken);
        return mapper.Map<Course>(courseEntity);
    }
    public async Task<Course?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var courseEntity =  await courseRepository.DeleteAsync(id, cancellationToken);
        return mapper.Map<Course>(courseEntity);
    }

    public async Task<List<Course?>> GetCourseByTeacherId(Guid teacherId, CancellationToken cancellationToken = default)
    {
        var courseEntity = await courseRepository.GetCourseByTeacherId(teacherId, cancellationToken);
        return mapper.Map<List<Course>>(courseEntity)!;
    }

    public async Task<Course?> GetCourseByFeedbackId(Guid feedbackId, CancellationToken cancellationToken = default)
    {
        var courseEntity = await courseRepository.GetCourseByFeedbackId(feedbackId, cancellationToken);
        return mapper.Map<Course>(courseEntity);
    }
}