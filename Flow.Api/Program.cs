using Flow.Application;
using Flow.Infrastructure;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try {
    Log.Information("Starting up");
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    builder.Services.AddMemoryCache();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped(typeof(CancellationToken),
        implementationFactory: serviceProvider => {
            IHttpContextAccessor httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            return httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
        });
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();

    WebApplication app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment()) {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
} catch (Exception ex) {
    Log.Fatal(ex, "Unhandled exception");
} finally {
    Log.CloseAndFlush();
}