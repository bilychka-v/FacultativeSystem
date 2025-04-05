using Mapster;
using MapsterMapper;

namespace FacultativeSystem.Api;

public static class MapsterConfiguration
{
    public static void RegisterMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}