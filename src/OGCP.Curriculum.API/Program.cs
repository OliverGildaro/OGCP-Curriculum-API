using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.factories;
using OGCP.Curriculum.API.repositories;
using OGCP.Curriculum.API.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbProfileContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conectionDb")));
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigins", builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});
builder.Services.AddControllers();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IProfileFactory, ProfileFactory>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

var app = builder.Build();

// Configure the HttP request pipeline.

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
