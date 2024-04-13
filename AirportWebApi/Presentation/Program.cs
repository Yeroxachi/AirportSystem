using System.Reflection;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseCustomSerilog();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(Assembly.Load("Application"));
});
builder.Services.AddAirportDbContext(builder.Configuration);
builder.Services.AddServicesOptions(builder.Configuration);
builder.Services.AddCustomServices();
builder.Services.AddSwaggerSecurity();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();