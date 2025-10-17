using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Flow.Infrastructure.DbContexts;

namespace Flow.Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContextFactory<FlowDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        
        return services;
    }
}
