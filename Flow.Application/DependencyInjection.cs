using Microsoft.Extensions.DependencyInjection;
using Flow.Application.Services;

namespace Flow.Application;

public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        services.AddScoped<IDemandRecordService, DemandRecordService>();
        
        return services;
    }
}