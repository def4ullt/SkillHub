using Aspire.Hosting;
using MongoDB.Driver;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgresPassword = builder.AddParameter("postgres-password", secret: true);

var postgres = builder.AddPostgres("SkillHubPostgres", password: postgresPassword)
    .WithImage("postgres:16")
    .WithDataVolume("SkillHubPostgresPgData")
    .WithBindMount("./Script", "/docker-entrypoint-initdb.d");

var taskServiceDB = postgres.AddDatabase("SkillHubTasks");
var workServiceDB = postgres.AddDatabase("TaskWorkService");

var mongo = builder.AddMongoDB("SkillHubMongo")
    .WithImage("mongo:7")
    .WithDataVolume("SkillHubMongoData");

var mongoDB = mongo.AddDatabase("SkillHubReviews");

var mlServicePath = Path.GetFullPath(Path.Combine(builder.AppHostDirectory, "..", "MicroServices", "MLService"));

var mlService = builder.AddExecutable("ml-service", "python", mlServicePath,
        "-m", "uvicorn", "app:app", "--host", "0.0.0.0", "--port", "5400");

var rabbit = builder.AddRabbitMQ("rabbitmq")
    .WithImage("rabbitmq", "3-management")
    .WithDataVolume("SkillHubRabbitMQData");

var taskService = builder.AddProject<TaskService_API>("task-service")
    .WithReference(taskServiceDB)
    .WithReference(rabbit)
    .WaitFor(taskServiceDB)
    .WaitFor(rabbit);

var workService = builder.AddProject<WorkService_API>("work-service")
    .WithReference(workServiceDB)
    .WithReference(rabbit)
    .WaitFor(workServiceDB)
    .WaitFor(rabbit);

var reviewService = builder.AddProject<ReviewService_API>("review-service")
    .WithReference(mongoDB)
    .WithEnvironment("MLService__BaseUrl", "http://localhost:5400")
    .WaitFor(mongoDB);

var aggregator = builder.AddProject<Aggregator_API>("aggregator-service")
    .WithReference(taskService)
    .WithReference(workService)
    .WithReference(reviewService)
    .WaitFor(taskService)
    .WaitFor(workService)
    .WaitFor(reviewService);

var gateway = builder.AddProject<Gateway>("gateway")
    .WithReference(aggregator)
    .WaitFor(aggregator);

var app = builder.Build();
await app.RunAsync();