using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Database;
using DAL.DbConn;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using DAL.Seeder;
using DAL.Unit_of_work;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Connections;
using BLL.FluentValidation.Status;
using BLL.Mapper;
using Microsoft.OpenApi.Models;
using System.Reflection;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<IDbConnectionFactory>(new NpgsqlConnectionFactory(connectionString));

builder.Services.AddScoped<IWorkSubmissionStatusRepository, WorkSubmissionStatusRepository>();
builder.Services.AddScoped<ISubmissionDeliveryMethodRepository, SubmissionDeliveryMethodRepository>();
builder.Services.AddScoped<IWorkSubmissionFileRepository, WorkSubmissionFileRepository>();
builder.Services.AddScoped<IWorkSubmissionRepository, WorkSubmissionRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ISubmissionDeliveryMethodService, SubmissionDeliveryMethodService>();
builder.Services.AddScoped<IWorkSubmissionStatusService, WorkSubmissionStatusService>();
builder.Services.AddScoped<IWorkSubmissionService, WorkSubmissionService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<WorkSubmissionStatusCreateDtoValidator>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(WorkSubmissionProfile).Assembly);
});

builder.Services.AddControllers();


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
    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

    await uow.BeginTransactionAsync();

    try
    {
        await SubmissionDeliveryMethodSeeder.SeedAsync(uow);
        await WorkSubmissionStatusSeeder.SeedAsync(uow);
        await WorkSubmissionSeeder.SeedAsync(uow);
        await WorkSubmissionFileSeeder.SeedAsync(uow);
        await uow.CommitAsync();
    }
    catch
    {
        await uow.RollbackAsync();
        throw;
    }
}

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
