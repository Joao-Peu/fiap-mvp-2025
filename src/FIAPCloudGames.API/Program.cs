using FIAPCloudGames.API.Extensions;
using FIAPCloudGames.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();