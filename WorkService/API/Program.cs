using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Database;
using DAL.DbConn;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using DAL.Seeder;
using DAL.Unit_of_work;
using Microsoft.AspNetCore.Connections;

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


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
