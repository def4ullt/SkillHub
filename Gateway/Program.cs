using ServiceDefaults.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddReverseProxy()
	   .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.WithOrigins("http://localhost:5173")
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

var app = builder.Build();
app.MapDefaultEndpoints();
app.UseCors();
app.UseAuthorization();
app.UseMiddleware<GatewayLoggingMiddleware>();
app.MapControllers();
app.MapReverseProxy();
app.Run();