using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddCors();
builder.Services.AddHostedService<TimeService>();

var app = builder.Build();
app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(_ => true));
app.MapHub<ClockHub>("/clockHub");
app.Run();