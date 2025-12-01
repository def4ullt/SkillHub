using BLL.Mapper;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.DB;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using DAL.SeedData;
using DAL.Unit_of_work;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BLL.FluentValidation.Task;
using Microsoft.OpenApi.Models;
using System.Reflection;
using CatalogService.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddServiceDefaults();

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITechnologyRepository, TechnologyRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ITechnologyService, TechnologyService>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(TagProfile).Assembly);
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TaskCreateDtoValidator>();

builder.AddNpgsqlDbContext<TaskServiceDbContext>("SkillHubTasks");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CatalogService", Version = "v1" });

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TaskServiceDbContext>();

    await context.Database.MigrateAsync();

    await TagSeeder.SeedAsync(context);
    await TechnologySeeder.SeedAsync(context);
    await TaskEntitySeeder.SeedAsync(context);
    await TaskTechnologySeeder.SeedAsync(context);
    await TaskTagSeeder.SeedAsync(context);

    Console.WriteLine("Seeding completed!");
}

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapDefaultEndpoints();

app.UseAuthorization();

app.MapControllers();

app.Run();
