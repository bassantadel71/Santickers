using Santickers.API.Middleware;
using Santickers.Infrastructure.Persistence.Data;
using Santickers.Infrastructure.Persistence.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


// Add global exception handling middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// Initialize database and seed data
using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider
		.GetRequiredService<ApplicationDbContext>();

	await DbInitializer.InitializeAsync(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();