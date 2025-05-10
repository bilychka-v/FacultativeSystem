using FacultativeSystem.Api.Contracts;
using FacultativeSystem.Application.Models;
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
        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        
        
    }
}