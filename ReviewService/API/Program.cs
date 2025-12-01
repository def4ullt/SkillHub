using System.Reflection;
using API.Middleware;
using Application.Behaviours;
using Application.FluenValidation.QueryParams;
using Application.Question.Commands.UpdateAnswer;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Context;
using Infrastructure.Context.FakeData;
using Infrastructure.Context.Mapping;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ValueObjectMappings.Register();
builder.Services.AddMongoDb(builder.Configuration);

builder.Services.AddScoped<ITaskReviewRepository, TaskReviewRepository>();
builder.Services.AddScoped<ITaskQuestionRepository, TaskQuestionRepository>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UpdateTaskAnswerHandler>());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReviewService", Version = "v1" });

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(TaskReviewQueryParametersValidator).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ReviewExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    services.EnsureIndexes();
    List<IDataSeeder> seeders = new List<IDataSeeder>()
    {
        services.GetRequiredService<TaskReviewSeeder>(),
        services.GetRequiredService<TaskQuestionSeeder>()
    };
    foreach (var seeder in seeders)
    {
        await seeder.SeedAsync();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
