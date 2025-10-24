using FIAPCloudGames.API.Extensions;
using FIAPCloudGames.API.Middleware;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        fileSizeLimitBytes: 10_485_760,
        rollOnFileSizeLimit: true,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Host.UseSerilog();

try
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddDatabaseConfiguration(builder.Configuration);
    builder.Services.AddDependencies();
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddSwaggerConfiguration();
    var app = builder.Build();
    app.UseMiddleware<ErrorHandlingMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} respondeu {StatusCode} em {Elapsed:0.0000} ms";

        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString());
            diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].ToString());
        };

        options.GetLevel = (httpContext, elapsed, ex) =>
        {
            if (ex != null) return LogEventLevel.Error;
            return httpContext.Response.StatusCode >= 500 ? LogEventLevel.Error : LogEventLevel.Information;
        };
    });


    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro ao inicializar a aplicação");
}
finally
{
    Log.CloseAndFlush();
}



