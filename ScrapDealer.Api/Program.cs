using ScrapDealer.Api.Extensions;
using ScrapDealer.Infrastructure;
using ScrapDealer.Infrastructure.Logging;
using ScrapDealer.Shared;
using ScrapDealer.Shared.Helpers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalRConfig();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new PersianDateTimeConverter());
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJsFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

app.UseShared();
app.UseMiddleware<LoggingMiddleware>();
app.UseSignalR();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowNextJsFrontend");

app.MapControllers();

app.Run();
