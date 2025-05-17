using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Models;
using FacultativeSystem.Domain.Entities;
using Mapster;
using MapsterMapper;

namespace FacultativeSystem.Api;

public static class MapsterConfiguration
{
    public static void RegisterMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.NewConfig<CourseRequest, Course>()
            .Map(dest => dest.StartDate, src => src.StartDate.ToUniversalTime())
            .Map(dest => dest.EndDate, src => src.EndDate.ToUniversalTime());
        
        config.NewConfig<StudentEntity, Student>();

        config.NewConfig<FeedbackGradeEntity, FeedbackGrade>()
            .Map(dest => dest.Student, src => src.Student);

        config.NewConfig<CourseEntity, Course>();
        
        config.NewConfig<TeacherEntity, Teacher>()
            .Map(dest => dest.Courses, src => src.Courses.Select(c=>c.Name) ?? new List<string>());

        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        
        
    }
}