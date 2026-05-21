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
using MassTransit;
using SkillHub.Contracts;
using API.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<TaskCreatedConsumer>();
	x.AddConsumer<TaskUpdatedConsumer>();

	x.UsingRabbitMq((context, cfg) =>
	{
		var config = context.GetRequiredService<IConfiguration>();
		var connStr = config.GetConnectionString("rabbitmq");
		if (!string.IsNullOrEmpty(connStr))
			cfg.Host(new Uri(connStr));
		else
			cfg.Host("localhost", "/", h => { h.Username("guest"); h.Password("guest"); });

		cfg.ConfigureEndpoints(context);
	});
});

string? connectionString = builder.Configuration.GetConnectionString("TaskWorkService");
builder.Services.AddSingleton<IDbConnectionFactory>(new NpgsqlConnectionFactory(connectionString));

builder.Services.AddScoped<IWorkSubmissionStatusRepository, WorkSubmissionStatusRepository>();
builder.Services.AddScoped<ISubmissionDeliveryMethodRepository, SubmissionDeliveryMethodRepository>();
builder.Services.AddScoped<IWorkSubmissionFileRepository, WorkSubmissionFileRepository>();
builder.Services.AddScoped<IWorkSubmissionRepository, WorkSubmissionRepository>();
builder.Services.AddScoped<IUserXpRepository, UserXpRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ISubmissionDeliveryMethodService, SubmissionDeliveryMethodService>();
builder.Services.AddScoped<IWorkSubmissionStatusService, WorkSubmissionStatusService>();
builder.Services.AddScoped<IWorkSubmissionService, WorkSubmissionService>();
builder.Services.AddScoped<IUserXpService, UserXpService>();

string taskServiceUrl = builder.Configuration["TaskService:BaseUrl"] ?? "http://localhost:5015";
builder.Services.AddHttpClient<ITaskServiceClient, TaskServiceClient>(client =>
{
    client.BaseAddress = new Uri(taskServiceUrl);
});

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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkService", Version = "v1" });

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


app.MapDefaultEndpoints();

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
